using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Views
{
    public interface IPaymentsMembershipView
    {
        long MembershipId { get; }

        string ServiceCode { get; }
        string PaymentMethodType { get; }
        decimal PaymentAmount { get; }
        string CommentText { get; }

        long SelectedPaymentId { get; }

        void SetCustomerInfo(long customerId, string customerFullname);
        void SetMembershipInfo(long membershipId, string statusDesc, decimal remainder);
        void SetMembershipDetails(
            string membershipType,
            string serviceCode,
            DateTime startDate,
            DateTime endDate,
            string comment);

        void SetMembershipTypeListBindingSource(BindingSource source);
        void SetServiceListBindingSource(BindingSource source);
        void SetPaymentMethodListBindingSource(BindingSource source);
        void SetPaymentsListBindingSource(BindingSource source);

        List<PaymentModel> GetEditedPaymentsFromGrid();

        void SetPaymentAmount(decimal amount);
        void SetReadOnlyMode(bool isReadOnly);

        void ShowMessage(string message, string title = "Πληροφορία");
        DialogResult ShowQuestion(string message, string title = "Επιβεβαίωση");
        void CloseView();
        void RefreshPaymentsGrid();

        event EventHandler LoadEvent;
        event EventHandler SaveEvent;
        event EventHandler PaymentEvent;
        event EventHandler DeleteEvent;
        event EventHandler CancelEvent;
    }
}