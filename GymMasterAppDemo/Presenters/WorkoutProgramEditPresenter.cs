using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class WorkoutProgramEditPresenter
    {
        private readonly IWorkoutProgramEditView _view;
        private readonly IWorkoutProgramRepository _workoutProgramRepository;
        private readonly ILookupRepository _lookupRepository;
        private readonly WorkoutProgramEditMode _mode;
        private readonly long _customerId;
        private readonly string _customerFullname;
        private readonly long? _workoutProgramId;

        private readonly BindingSource _goalBindingSource;
        private readonly BindingSource _detailsBindingSource;

        private BindingList<WorkoutProgramDetailModel> _details;

        private readonly List<string> _days = new List<string>
        {
            "1η Μέρα",
            "2η Μέρα",
            "3η Μέρα",
            "4η Μέρα",
            "5η Μέρα",
            "6η Μέρα"
        };

        public WorkoutProgramEditPresenter(
            IWorkoutProgramEditView view,
            IWorkoutProgramRepository workoutProgramRepository,
            ILookupRepository lookupRepository,
            WorkoutProgramEditMode mode,
            long customerId,
            string customerFullname,
            long? workoutProgramId = null)
        {
            _view = view;
            _workoutProgramRepository = workoutProgramRepository;
            _lookupRepository = lookupRepository;
            _mode = mode;
            _customerId = customerId;
            _customerFullname = customerFullname;
            _workoutProgramId = workoutProgramId;

            _goalBindingSource = new BindingSource();
            _detailsBindingSource = new BindingSource();
            _details = new BindingList<WorkoutProgramDetailModel>();

            _view.LoadFormEvent += OnLoadForm;
            _view.SaveEvent += OnSave;
            _view.CancelEvent += OnCancel;
            _view.StartDateChangedEvent += OnStartDateOrDurationChanged;
            _view.DurationLeaveEvent += OnStartDateOrDurationChanged;

            _view.DetailsGrid.CurrentCellDirtyStateChanged += OnGridCurrentCellDirtyStateChanged;
            _view.DetailsGrid.CellValueChanged += OnDetailsGridCellValueChanged;
            _view.DetailsGrid.DefaultValuesNeeded += OnDetailsGridDefaultValuesNeeded;
            _view.DetailsGrid.DataError += OnDetailsGridDataError;
        }

        private void OnLoadForm(object sender, EventArgs e)
        {
            LoadGoals();
            LoadStaticGridOptions();

            _view.CustomerId = _customerId;
            _view.CustomerFullname = _customerFullname;

            _view.SetProgramIdReadOnly(true);
            _view.SetCustomerFieldsReadOnly(true);
            _view.SetEndDateReadOnly(true);

            if (_mode == WorkoutProgramEditMode.Add)
            {
                LoadForAdd();
            }
            else
            {
                LoadForEdit();
            }
        }

        private void LoadGoals()
        {
            var goals = _lookupRepository.GetWorkoutGoals();
            _goalBindingSource.DataSource = goals;
            _view.SetGoalListBindingSource(_goalBindingSource);
        }

        private void LoadStaticGridOptions()
        {
            _view.SetDayColumnOptions(_days);
            _view.SetMuscleGroupColumnOptions(_lookupRepository.GetMuscleGroups());
        }

        private void LoadForAdd()
        {
            _view.FormTitle = "Νέο Ατομικό Πρόγραμμα | Πελάτης: " + _customerFullname;
            _view.SaveButtonText = "    Αποθήκευση";
            _view.ProgramIdText = string.Empty;
            _view.SelectedGoalCode = null;
            _view.StartDate = DateTime.Today;
            _view.DurationText = string.Empty;
            _view.EndDateText = string.Empty;
            _view.FrequencyText = string.Empty;
            _view.Comments = string.Empty;

            _details = new BindingList<WorkoutProgramDetailModel>();
            _detailsBindingSource.DataSource = _details;
            _view.BindDetails(_detailsBindingSource);

            UpdateEndDate();
        }

        private void LoadForEdit()
        {
            if (!_workoutProgramId.HasValue)
            {
                _view.ShowWarning("Δεν δόθηκε κωδικός προγράμματος.");
                _view.CloseView();
                return;
            }

            var program = _workoutProgramRepository.GetById(_workoutProgramId.Value);
            if (program == null)
            {
                _view.ShowWarning("Το πρόγραμμα δεν βρέθηκε.");
                _view.CloseView();
                return;
            }

            _view.FormTitle = "Αλλαγή Ατομικού Προγράμματος | Αριθμ. Προγρ/τος: " + program.Id.ToString("D5");
            _view.SaveButtonText = "    Ενημέρωση";
            _view.ProgramIdText = program.Id.ToString("D5");
            _view.SelectedGoalCode = program.GoalCode;
            _view.StartDate = program.StartDate;
            _view.DurationText = program.Duration.ToString();
            _view.EndDateText = program.EndDate.ToString("dd/MM/yyyy");
            _view.FrequencyText = program.Frequency.ToString();
            _view.Comments = program.Comments;

            var detailList = _workoutProgramRepository.GetDetailsByProgramId(program.Id);
            _details = new BindingList<WorkoutProgramDetailModel>(detailList);
            _detailsBindingSource.DataSource = _details;
            _view.BindDetails(_detailsBindingSource);

            PrepareExerciseCombosForExistingRows();
            _view.RefreshDetailsGrid();
        }

        private void OnStartDateOrDurationChanged(object sender, EventArgs e)
        {
            UpdateEndDate();
        }

        private void UpdateEndDate()
        {
            if (!int.TryParse(_view.DurationText, out int duration) || duration < 1 || duration > 8)
            {
                _view.EndDateText = string.Empty;
                return;
            }

            DateTime endDate = _view.StartDate.Date.AddDays(duration * 7);
            _view.EndDateText = endDate.ToString("dd/MM/yyyy");
        }

        private void OnGridCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!_view.DetailsGrid.IsCurrentCellDirty)
                return;

            DataGridViewCell currentCell = _view.DetailsGrid.CurrentCell;
            if (currentCell == null)
                return;

            if (currentCell is DataGridViewComboBoxCell)
            {
                _view.DetailsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void OnDetailsGridDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["RestTime"].Value = "1";
        }

        private void OnDetailsGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _view.DetailsGrid.Rows.Count)
                return;

            DataGridViewRow row = _view.DetailsGrid.Rows[e.RowIndex];
            if (row.IsNewRow)
                return;

            string columnName = _view.DetailsGrid.Columns[e.ColumnIndex].Name;

            if (columnName == "MuscleGroup")
            {
                row.Cells["ExerciseName"].Value = null;
                row.Cells["ExerciseCode"].Value = null;

                string muscleGroup = Convert.ToString(row.Cells["MuscleGroup"].Value);
                ApplyExerciseOptionsToRow(row, muscleGroup, null);
            }
            else if (columnName == "ExerciseName")
            {
                UpdateExerciseCodeForRow(row);
            }
        }

        private void OnDetailsGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void PrepareExerciseCombosForExistingRows()
        {
            foreach (DataGridViewRow row in _view.DetailsGrid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string muscleGroup = Convert.ToString(row.Cells["MuscleGroup"].Value);
                string exerciseName = Convert.ToString(row.Cells["ExerciseName"].Value);

                ApplyExerciseOptionsToRow(row, muscleGroup, exerciseName);
                UpdateExerciseCodeForRow(row);
            }
        }

        private void ApplyExerciseOptionsToRow(DataGridViewRow row, string muscleGroup, string selectedExerciseName)
        {
            var comboCell = row.Cells["ExerciseName"] as DataGridViewComboBoxCell;
            if (comboCell == null)
                return;

            comboCell.DataSource = null;
            comboCell.Items.Clear();

            if (string.IsNullOrWhiteSpace(muscleGroup))
            {
                comboCell.ReadOnly = true;
                comboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                return;
            }

            var exercises = _lookupRepository
                .GetExercisesByMuscleGroup(muscleGroup)
                .Select(x => x.ExerciseName)
                .ToList();

            comboCell.DataSource = exercises;
            comboCell.ReadOnly = false;
            comboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

            if (!string.IsNullOrWhiteSpace(selectedExerciseName) && exercises.Contains(selectedExerciseName))
            {
                row.Cells["ExerciseName"].Value = selectedExerciseName;
            }
        }

        private void UpdateExerciseCodeForRow(DataGridViewRow row)
        {
            string muscleGroup = Convert.ToString(row.Cells["MuscleGroup"].Value);
            string exerciseName = Convert.ToString(row.Cells["ExerciseName"].Value);

            if (string.IsNullOrWhiteSpace(muscleGroup) || string.IsNullOrWhiteSpace(exerciseName))
            {
                row.Cells["ExerciseCode"].Value = null;
                return;
            }

            var exercise = _lookupRepository
                .GetExercisesByMuscleGroup(muscleGroup)
                .FirstOrDefault(x => string.Equals(x.ExerciseName, exerciseName, StringComparison.OrdinalIgnoreCase));

            row.Cells["ExerciseCode"].Value = exercise == null ? null : exercise.ExerciseCode;
        }

        private void OnSave(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateHeader(out string goalCode, out int duration, out int frequency, out DateTime endDate))
                    return;

                List<WorkoutProgramDetailModel> details = BuildAndValidateDetails();
                if (details == null)
                    return;

                WorkoutProgramModel model = new WorkoutProgramModel
                {
                    CustomerId = _customerId,
                    GoalCode = goalCode,
                    Duration = duration,
                    Frequency = frequency,
                    StartDate = _view.StartDate.Date,
                    EndDate = endDate.Date,
                    Comments = NullIfEmpty(_view.Comments)
                };

                if (_mode == WorkoutProgramEditMode.Add)
                {
                    long newId = _workoutProgramRepository.Add(model, details);
                    _view.ShowMessage("Το πρόγραμμα καταχωρήθηκε επιτυχώς με κωδικό " + newId.ToString("D5") + ".");
                }
                else
                {
                    model.Id = _workoutProgramId ?? 0;
                    _workoutProgramRepository.Update(model, details);
                    _view.ShowMessage("Το πρόγραμμα ενημερώθηκε επιτυχώς.");
                }

                _view.CloseView();
            }
            catch (Exception ex)
            {
                _view.ShowWarning("Παρουσιάστηκε σφάλμα κατά την αποθήκευση: " + ex.Message);
            }
        }

        private bool ValidateHeader(out string goalCode, out int duration, out int frequency, out DateTime endDate)
        {
            goalCode = NullIfEmpty(_view.SelectedGoalCode);
            duration = 0;
            frequency = 0;
            endDate = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(goalCode))
            {
                _view.ShowWarning("Ο Στόχος είναι υποχρεωτικό πεδίο.");
                return false;
            }

            if (!int.TryParse(_view.DurationText, out duration) || duration < 1 || duration > 8)
            {
                _view.ShowWarning("Η Διάρκεια πρέπει να είναι αριθμός από 1 έως 8 εβδομάδες.");
                return false;
            }

            if (!int.TryParse(_view.FrequencyText, out frequency) || frequency < 1 || frequency > 6)
            {
                _view.ShowWarning("Η Συχνότητα πρέπει να είναι αριθμός από 1 έως 6 ημέρες.");
                return false;
            }

            endDate = _view.StartDate.Date.AddDays(duration * 7);
            _view.EndDateText = endDate.ToString("dd/MM/yyyy");

            return true;
        }

        private List<WorkoutProgramDetailModel> BuildAndValidateDetails()
        {
            List<WorkoutProgramDetailModel> result = new List<WorkoutProgramDetailModel>();
            HashSet<string> usedExerciseCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataGridViewRow row in _view.DetailsGrid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string dayOfWeek = NullIfEmpty(Convert.ToString(row.Cells["DayOfWeek"].Value));
                string muscleGroup = NullIfEmpty(Convert.ToString(row.Cells["MuscleGroup"].Value));
                string exerciseName = NullIfEmpty(Convert.ToString(row.Cells["ExerciseName"].Value));
                string exerciseCode = NullIfEmpty(Convert.ToString(row.Cells["ExerciseCode"].Value));
                string setsText = NullIfEmpty(Convert.ToString(row.Cells["Sets"].Value));
                string repsText = NullIfEmpty(Convert.ToString(row.Cells["Reps"].Value));
                string restTime = NullIfEmpty(Convert.ToString(row.Cells["RestTime"].Value));

                if (IsDetailRowEmpty(dayOfWeek, muscleGroup, exerciseName, setsText, repsText, restTime))
                    continue;

                if (string.IsNullOrWhiteSpace(dayOfWeek))
                {
                    _view.ShowWarning("Κάθε γραμμή ασκήσεων πρέπει να έχει Ημέρα Προπόνησης.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(muscleGroup))
                {
                    _view.ShowWarning("Κάθε γραμμή ασκήσεων πρέπει να έχει Μυϊκή Ομάδα.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(exerciseName))
                {
                    _view.ShowWarning("Κάθε γραμμή ασκήσεων πρέπει να έχει Άσκηση.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(exerciseCode))
                {
                    var exercise = _lookupRepository
                        .GetExercisesByMuscleGroup(muscleGroup)
                        .FirstOrDefault(x => string.Equals(x.ExerciseName, exerciseName, StringComparison.OrdinalIgnoreCase));

                    exerciseCode = exercise == null ? null : exercise.ExerciseCode;
                }

                if (string.IsNullOrWhiteSpace(exerciseCode))
                {
                    _view.ShowWarning("Δεν βρέθηκε έγκυρος κωδικός άσκησης για την άσκηση \"" + exerciseName + "\".");
                    return null;
                }

                if (usedExerciseCodes.Contains(exerciseCode))
                {
                    _view.ShowWarning("Η ίδια άσκηση δεν μπορεί να υπάρχει περισσότερες από μία φορές στο ίδιο πρόγραμμα.");
                    return null;
                }

                if (!int.TryParse(setsText, out int sets) || sets < 1 || sets > 10)
                {
                    _view.ShowWarning("Το πεδίο Σετ πρέπει να έχει τιμή από 1 έως 10.");
                    return null;
                }

                if (!int.TryParse(repsText, out int reps) || reps < 1 || reps > 50)
                {
                    _view.ShowWarning("Το πεδίο Επαναλήψεις πρέπει να έχει τιμή από 1 έως 50.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(restTime))
                    restTime = "1";

                usedExerciseCodes.Add(exerciseCode);

                result.Add(new WorkoutProgramDetailModel
                {
                    DayOfWeek = dayOfWeek,
                    MuscleGroup = muscleGroup,
                    ExerciseCode = exerciseCode,
                    ExerciseName = exerciseName,
                    Sets = sets,
                    Reps = reps,
                    RestTime = restTime
                });
            }

            if (result.Count == 0)
            {
                _view.ShowWarning("Πρέπει να καταχωρηθεί τουλάχιστον μία άσκηση.");
                return null;
            }

            return result;
        }

        private bool IsDetailRowEmpty(
            string dayOfWeek,
            string muscleGroup,
            string exerciseName,
            string setsText,
            string repsText,
            string restTime)
        {
            return string.IsNullOrWhiteSpace(dayOfWeek)
                && string.IsNullOrWhiteSpace(muscleGroup)
                && string.IsNullOrWhiteSpace(exerciseName)
                && string.IsNullOrWhiteSpace(setsText)
                && string.IsNullOrWhiteSpace(repsText)
                && string.IsNullOrWhiteSpace(restTime);
        }

        private int CalculateDurationWeeks(DateTime startDate, DateTime endDate)
        {
            double days = (endDate.Date - startDate.Date).TotalDays;
            if (days <= 0)
                return 1;

            int weeks = (int)Math.Round(days / 7.0, MidpointRounding.AwayFromZero);
            if (weeks < 1)
                weeks = 1;
            if (weeks > 8)
                weeks = 8;

            return weeks;
        }

        private string NullIfEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}