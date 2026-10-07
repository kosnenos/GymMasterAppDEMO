using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface INewMembershipView
    {
        long CustomerId { get; set; }
        string CustomerFullname { get; set; }

        string SelectedMembershipType { get; }
        string SelectedServiceCode { get; }
        string SelectedPaymentMethod { get; }

        DateTime StartDate { get; }
        decimal Price { get; }
        decimal FirstPaymentAmount { get; }
        string Comment { get; }

        void SetMembershipTypeListBindingSource(BindingSource source);
        void SetServiceListBindingSource(BindingSource source);
        void SetPaymentMethodListBindingSource(BindingSource source);

        void SetDuration(int duration);
        void SetEndDate(string endDateText);

        void ShowMessage(string message, string title = "Πληροφορία");
        void CloseView();

        event EventHandler LoadEvent;
        event EventHandler MembershipTypeChangedEvent;
        event EventHandler StartDateChangedEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
    }
}