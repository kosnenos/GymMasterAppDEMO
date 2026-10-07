using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface IWorkoutProgramEditView
    {
        long CustomerId { get; set; }
        string CustomerFullname { get; set; }

        string ProgramIdText { get; set; }

        string SelectedGoalCode { get; set; }
        DateTime StartDate { get; set; }
        string DurationText { get; set; }
        string EndDateText { get; set; }
        string FrequencyText { get; set; }
        string Comments { get; set; }

        string FormTitle { set; }
        string SaveButtonText { set; }

        DataGridView DetailsGrid { get; }

        event EventHandler LoadFormEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler StartDateChangedEvent;
        event EventHandler DurationLeaveEvent;

        void SetGoalListBindingSource(BindingSource source);

        void SetProgramIdReadOnly(bool value);
        void SetCustomerFieldsReadOnly(bool value);
        void SetEndDateReadOnly(bool value);

        void ShowMessage(string message, string title = "Πληροφορία");
        void ShowWarning(string message, string title = "Προειδοποίηση");
        DialogResult ShowQuestion(string message, string title = "Επιβεβαίωση");

        void CloseView();

        void BindDetails(BindingSource source);

        void SetDayColumnOptions(List<string> days);
        void SetMuscleGroupColumnOptions(List<string> muscleGroups);

        void RefreshDetailsGrid();
    }
}