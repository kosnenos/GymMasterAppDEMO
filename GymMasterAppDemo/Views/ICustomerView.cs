using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface ICustomerView
    {
        string SearchValue { get; }

        void SetCustomerListBindingSource(BindingSource source);
        long GetSelectedCustomerId();

        void ShowMessage(string message);
        void ShowWarning(string message);
        void OpenNewCustomerView();
        void OpenEditCustomerView(long customerId);
        void CloseView();

        event EventHandler SearchEvent;
        event EventHandler RefreshEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler CloseEvent;
        event EventHandler LoadCustomersEvent;
        event EventHandler DoubleClickEditEvent;
    }
}