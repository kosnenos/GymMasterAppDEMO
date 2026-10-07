using System;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace GymMasterAppDemo.Views
{
    public interface IMembershipView
    {
        string SearchValue { get; }

        long SelectedCustomerId { get; }
        string SelectedCustomerFullname { get; }
        long SelectedMembershipId { get; }

        void SetCustomerListBindingSource(BindingSource customerList);
        void SetMembershipListBindingSource(BindingSource membershipList);

        void ShowMessage(string message, string title = "Πληροφορία");
        void ClearMemberships();

        void OpenNewMembershipView(long customerId, string customerFullname);
        void OpenPaymentsMembershipView(long membershipId);
        void ShowPrintPreview(PrintDocument printDocument);

        event EventHandler SearchEvent;
        event EventHandler RefreshEvent;
        event EventHandler CustomerSelectionChangedEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditPaymentsEvent;
        event EventHandler DeleteEvent;
        event EventHandler PrintEvent;
    }
}