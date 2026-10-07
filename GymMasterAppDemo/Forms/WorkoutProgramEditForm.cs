using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Forms
{
    public partial class WorkoutProgramEditForm : Form, IWorkoutProgramEditView
    {
        public WorkoutProgramEditForm()
        {
            InitializeComponent();
            ConfigureControls();
            ConfigureGrid();
            WireEvents();
        }

        public long CustomerId
        {
            get
            {
                return long.TryParse(txtCustomerId.Text, out long id) ? id : 0;
            }
            set
            {
                txtCustomerId.Text = value <= 0 ? string.Empty : value.ToString();
            }
        }

        public string CustomerFullname
        {
            get => txtCustomerFullname.Text;
            set => txtCustomerFullname.Text = value ?? string.Empty;
        }

        public string ProgramIdText
        {
            get => txtProgramId.Text;
            set => txtProgramId.Text = value ?? string.Empty;
        }

        public string SelectedGoalCode
        {
            get
            {
                return cboGoal.SelectedValue == null
                    ? null
                    : cboGoal.SelectedValue.ToString();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    cboGoal.SelectedIndex = -1;
                else
                    cboGoal.SelectedValue = value;
            }
        }

        public DateTime StartDate
        {
            get => dtpStartDate.Value.Date;
            set => dtpStartDate.Value = value == DateTime.MinValue ? DateTime.Today : value.Date;
        }

        public string DurationText
        {
            get => txtDuration.Text;
            set => txtDuration.Text = value ?? string.Empty;
        }

        public string EndDateText
        {
            get => txtEndDate.Text;
            set => txtEndDate.Text = value ?? string.Empty;
        }

        public string FrequencyText
        {
            get => txtFrequency.Text;
            set => txtFrequency.Text = value ?? string.Empty;
        }

        public string Comments
        {
            get => txtComments.Text;
            set => txtComments.Text = value ?? string.Empty;
        }

        public string FormTitle
        {
            set => this.Text = value;
        }

        public string SaveButtonText
        {
            set => btnSave.Text = value;
        }

        public DataGridView DetailsGrid => dgvWorkoutProgramDetails;

        public event EventHandler LoadFormEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler StartDateChangedEvent;
        public event EventHandler DurationLeaveEvent;

        private void ConfigureControls()
        {
            txtProgramId.ReadOnly = true;
            txtCustomerId.ReadOnly = true;
            txtCustomerFullname.ReadOnly = true;
            txtEndDate.ReadOnly = true;

            cboGoal.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpStartDate.Format = DateTimePickerFormat.Short;
        }

        private void ConfigureGrid()
        {
            dgvWorkoutProgramDetails.AutoGenerateColumns = false;
            dgvWorkoutProgramDetails.AllowUserToAddRows = true;
            dgvWorkoutProgramDetails.AllowUserToDeleteRows = true;
            dgvWorkoutProgramDetails.MultiSelect = false;
            dgvWorkoutProgramDetails.RowHeadersVisible = false;
            dgvWorkoutProgramDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvWorkoutProgramDetails.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvWorkoutProgramDetails.Columns.Clear();

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id";
            colId.DataPropertyName = "Id";
            colId.Visible = false;

            DataGridViewTextBoxColumn colProgramId = new DataGridViewTextBoxColumn();
            colProgramId.Name = "ProgramId";
            colProgramId.DataPropertyName = "ProgramId";
            colProgramId.Visible = false;

            DataGridViewComboBoxColumn colDay = new DataGridViewComboBoxColumn();
            colDay.Name = "DayOfWeek";
            colDay.DataPropertyName = "DayOfWeek";
            colDay.HeaderText = "Ημέρα Προπόνησης";
            colDay.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            colDay.FlatStyle = FlatStyle.Flat;

            DataGridViewComboBoxColumn colMuscleGroup = new DataGridViewComboBoxColumn();
            colMuscleGroup.Name = "MuscleGroup";
            colMuscleGroup.DataPropertyName = "MuscleGroup";
            colMuscleGroup.HeaderText = "Μυϊκή Ομάδα";
            colMuscleGroup.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            colMuscleGroup.FlatStyle = FlatStyle.Flat;

            DataGridViewComboBoxColumn colExerciseName = new DataGridViewComboBoxColumn();
            colExerciseName.Name = "ExerciseName";
            colExerciseName.DataPropertyName = "ExerciseName";
            colExerciseName.HeaderText = "Άσκηση";
            colExerciseName.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            colExerciseName.FlatStyle = FlatStyle.Flat;

            DataGridViewTextBoxColumn colExerciseCode = new DataGridViewTextBoxColumn();
            colExerciseCode.Name = "ExerciseCode";
            colExerciseCode.DataPropertyName = "ExerciseCode";
            colExerciseCode.Visible = false;

            DataGridViewTextBoxColumn colSets = new DataGridViewTextBoxColumn();
            colSets.Name = "Sets";
            colSets.DataPropertyName = "Sets";
            colSets.HeaderText = "Σετ";

            DataGridViewTextBoxColumn colReps = new DataGridViewTextBoxColumn();
            colReps.Name = "Reps";
            colReps.DataPropertyName = "Reps";
            colReps.HeaderText = "Επαναλήψεις";
            colReps.MaxInputLength = 3;

            DataGridViewTextBoxColumn colRestTime = new DataGridViewTextBoxColumn();
            colRestTime.Name = "RestTime";
            colRestTime.DataPropertyName = "RestTime";
            colRestTime.HeaderText = "Ξεκούραση";

            dgvWorkoutProgramDetails.Columns.AddRange(
                colId,
                colProgramId,
                colDay,
                colMuscleGroup,
                colExerciseName,
                colExerciseCode,
                colSets,
                colReps,
                colRestTime);
        }

        private void WireEvents()
        {
            this.Load += (s, e) => LoadFormEvent?.Invoke(this, EventArgs.Empty);
            btnSave.Click += (s, e) => SaveEvent?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (s, e) => CancelEvent?.Invoke(this, EventArgs.Empty);
            dtpStartDate.ValueChanged += (s, e) => StartDateChangedEvent?.Invoke(this, EventArgs.Empty);
            txtDuration.Leave += (s, e) => DurationLeaveEvent?.Invoke(this, EventArgs.Empty);
        }

        public void SetGoalListBindingSource(BindingSource source)
        {
            cboGoal.DataSource = source;
            cboGoal.DisplayMember = "Goal";
            cboGoal.ValueMember = "GoalCode";
            cboGoal.SelectedIndex = -1;
        }

        public void SetProgramIdReadOnly(bool value)
        {
            txtProgramId.ReadOnly = value;
        }

        public void SetCustomerFieldsReadOnly(bool value)
        {
            txtCustomerId.ReadOnly = value;
            txtCustomerFullname.ReadOnly = value;
        }

        public void SetEndDateReadOnly(bool value)
        {
            txtEndDate.ReadOnly = value;
        }

        public void BindDetails(BindingSource source)
        {
            dgvWorkoutProgramDetails.DataSource = source;
        }

        public void SetDayColumnOptions(List<string> days)
        {
            DataGridViewComboBoxColumn column = dgvWorkoutProgramDetails.Columns["DayOfWeek"] as DataGridViewComboBoxColumn;
            if (column == null)
                return;

            column.DataSource = null;
            column.Items.Clear();

            if (days != null)
                column.Items.AddRange(days.ToArray());
        }

        public void SetMuscleGroupColumnOptions(List<string> muscleGroups)
        {
            DataGridViewComboBoxColumn column = dgvWorkoutProgramDetails.Columns["MuscleGroup"] as DataGridViewComboBoxColumn;
            if (column == null)
                return;

            column.DataSource = null;
            column.Items.Clear();

            if (muscleGroups != null)
                column.Items.AddRange(muscleGroups.ToArray());
        }

        public void RefreshDetailsGrid()
        {
            dgvWorkoutProgramDetails.Refresh();
        }

        public void ShowMessage(string message, string title = "Πληροφορία")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowWarning(string message, string title = "Προειδοποίηση")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public DialogResult ShowQuestion(string message, string title = "Επιβεβαίωση")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void CloseView()
        {
            this.Close();
        }
    }
}