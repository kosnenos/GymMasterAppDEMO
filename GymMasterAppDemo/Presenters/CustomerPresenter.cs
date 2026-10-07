using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;


namespace GymMasterAppDemo.Presenters
{
    public class CustomerPresenter
    {
        private readonly ICustomerView _view;
        private readonly ICustomerRepository _repository;
        private readonly BindingSource _bindingSource;
        private List<CustomerListModel> _customerList;

        public CustomerPresenter(ICustomerView view, ICustomerRepository repository)
        {
            _view = view;
            _repository = repository;
            _bindingSource = new BindingSource();

            _view.SetCustomerListBindingSource(_bindingSource);

            _view.LoadCustomersEvent += LoadAllCustomers;
            _view.SearchEvent += SearchCustomer;
            _view.RefreshEvent += RefreshCustomers;
            _view.AddNewEvent += AddNewCustomer;
            _view.EditEvent += EditCustomer;
            _view.DoubleClickEditEvent += EditCustomer;
            _view.CloseEvent += CloseView;
        }

        public void LoadCustomers()
        {
            LoadAllCustomers(this, EventArgs.Empty);
        }

        private void LoadAllCustomers(object sender, EventArgs e)
        {
            _customerList = _repository.GetActiveCustomers();
            _bindingSource.DataSource = _customerList;
        }

        private void SearchCustomer(object sender, EventArgs e)
        {
            string value = _view.SearchValue?.Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                _customerList = _repository.GetActiveCustomers();
            }
            else
            {
                _customerList = _repository.SearchActiveCustomers(value);

                if (_customerList.Count == 0)
                {
                    _view.ShowMessage("Δεν βρέθηκε πελάτης με τα στοιχεία που δώσατε.");
                }
            }

            _bindingSource.DataSource = _customerList;
        }

        private void RefreshCustomers(object sender, EventArgs e)
        {
            _customerList = _repository.GetActiveCustomers();
            _bindingSource.DataSource = _customerList;
        }

        private void AddNewCustomer(object sender, EventArgs e)
        {
            _view.OpenNewCustomerView();
        }

        private void EditCustomer(object sender, EventArgs e)
        {
            long id = _view.GetSelectedCustomerId();

            if (id <= 0)
            {
                _view.ShowWarning("Παρακαλώ επιλέξτε έναν πελάτη από τη λίστα.");
                return;
            }

            _view.OpenEditCustomerView(id);
        }

        private void CloseView(object sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}