using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface IWorkoutProgramView
    {
        string SearchValue { get; }

        long SelectedCustomerId { get; }
        string SelectedCustomerFullname { get; }
        long SelectedWorkoutProgramId { get; }

        void SetCustomerListBindingSource(BindingSource customerList);
        void SetWorkoutProgramListBindingSource(BindingSource workoutProgramList);

        void ShowMessage(string message, string title = "Πληροφορία");
        void ClearWorkoutPrograms();

        void OpenAddWorkoutProgramView(long customerId, string customerFullname);
        void OpenEditWorkoutProgramView(long customerId, string customerFullname, long workoutProgramId);

        event EventHandler SearchEvent;
        event EventHandler RefreshEvent;
        event EventHandler CustomerSelectionChangedEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditUpdateEvent;
        event EventHandler DeleteEvent;
        event EventHandler ReturnToHomeRequested;
        event EventHandler PrintEvent;
    }
}