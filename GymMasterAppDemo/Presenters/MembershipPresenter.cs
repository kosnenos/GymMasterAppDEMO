using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Printing;
using GymMasterAppDemo.Views;


namespace GymMasterAppDemo.Presenters
{
    public class MembershipPresenter
    {
        private readonly IMembershipView _view;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipRepository _membershipRepository;

        private readonly BindingSource _customerBindingSource;
        private readonly BindingSource _membershipBindingSource;

        public MembershipPresenter(
            IMembershipView view,
            ICustomerRepository customerRepository,
            IMembershipRepository membershipRepository)
        {
            _view = view;
            _customerRepository = customerRepository;
            _membershipRepository = membershipRepository;

            _customerBindingSource = new BindingSource();
            _membershipBindingSource = new BindingSource();

            _view.SetCustomerListBindingSource(_customerBindingSource);
            _view.SetMembershipListBindingSource(_membershipBindingSource);

            _view.SearchEvent += SearchCustomers;
            _view.RefreshEvent += RefreshAll;
            _view.CustomerSelectionChangedEvent += LoadMembershipsForSelectedCustomer;
            _view.AddNewEvent += OpenNewMembership;
            _view.EditPaymentsEvent += OpenPaymentsMembership;
            _view.DeleteEvent += DeleteMembership;
            _view.PrintEvent += PrepareMembershipPrint;

            LoadActiveCustomers();
        }

        public void LoadMemberships()
        {
            LoadActiveCustomers();
        }

        private void LoadActiveCustomers(object sender = null, EventArgs e = null)
        {
            try
            {
                List<CustomerListModel> customers = _customerRepository.GetActiveCustomers();
                _customerBindingSource.DataSource = customers;

                if (customers == null || customers.Count == 0)
                {
                    _view.ClearMemberships();
                    return;
                }

                LoadMembershipsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη φόρτωση πελατών: " + ex.Message, "Σφάλμα");
            }
        }

        private void SearchCustomers(object sender, EventArgs e)
        {
            try
            {
                List<CustomerListModel> customers;

                if (string.IsNullOrWhiteSpace(_view.SearchValue))
                    customers = _customerRepository.GetActiveCustomers();
                else
                    customers = _customerRepository.SearchActiveCustomers(_view.SearchValue.Trim());

                _customerBindingSource.DataSource = customers;

                if (customers == null || customers.Count == 0)
                {
                    _view.ClearMemberships();
                    return;
                }

                LoadMembershipsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά την αναζήτηση πελατών: " + ex.Message, "Σφάλμα");
            }
        }

        private void RefreshAll(object sender, EventArgs e)
        {
            LoadActiveCustomers();
        }

        private void LoadMembershipsForSelectedCustomer(object sender = null, EventArgs e = null)
        {
            try
            {
                if (_view.SelectedCustomerId <= 0)
                {
                    _view.ClearMemberships();
                    return;
                }

                List<MembershipModel> memberships = _membershipRepository.GetByCustomerId(_view.SelectedCustomerId);
                _membershipBindingSource.DataSource = memberships;
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη φόρτωση συνδρομών: " + ex.Message, "Σφάλμα");
            }
        }

        private void OpenNewMembership(object sender, EventArgs e)
        {
            if (_view.SelectedCustomerId <= 0)
            {
                _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα έναν πελάτη.", "Προσοχή");
                return;
            }

            _view.OpenNewMembershipView(_view.SelectedCustomerId, _view.SelectedCustomerFullname);
            LoadMembershipsForSelectedCustomer();
        }

        private void OpenPaymentsMembership(object sender, EventArgs e)
        {
            if (_view.SelectedMembershipId <= 0)
            {
                _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα μία συνδρομή.", "Προσοχή");
                return;
            }

            _view.OpenPaymentsMembershipView(_view.SelectedMembershipId);
            LoadMembershipsForSelectedCustomer();
        }

        private void DeleteMembership(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedMembershipId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα μία συνδρομή.", "Προσοχή");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Θέλετε σίγουρα να διαγράψετε την επιλεγμένη συνδρομή και τις πληρωμές της;",
                    "Επιβεβαίωση διαγραφής",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                _membershipRepository.Delete(_view.SelectedMembershipId);

                _view.ShowMessage("Η συνδρομή διαγράφηκε επιτυχώς.", "Επιτυχία");
                LoadMembershipsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη διαγραφή συνδρομής: " + ex.Message, "Σφάλμα");
            }
        }

        private void PrepareMembershipPrint(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedCustomerId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα έναν πελάτη.", "Προσοχή");
                    return;
                }

                CustomerModel customer = _customerRepository.GetById(_view.SelectedCustomerId);
                if (customer == null)
                {
                    _view.ShowMessage("Δεν βρέθηκαν τα στοιχεία του επιλεγμένου πελάτη.", "Σφάλμα");
                    return;
                }

                List<MembershipModel> memberships = _membershipRepository.GetByCustomerId(_view.SelectedCustomerId);

                CustomerMembershipPrintModel report = new CustomerMembershipPrintModel
                {
                    CustomerId = customer.Id,
                    FullName = (customer.LastName + " " + customer.FirstName).Trim(),
                    CreationDate = customer.CreationDate,
                    Mobile = customer.Mobile ?? string.Empty,
                    Email = customer.Email ?? string.Empty,
                    Memberships = memberships.Select(m => new CustomerMembershipPrintRowModel
                    {
                        MembershipId = m.Id,
                        MembershipTypeDescription = m.MembershipTypeDescription ?? string.Empty,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        ServiceDescription = m.ServiceDescription ?? string.Empty,
                        Price = m.Price,
                        StatusDescriptionText = m.StatusDescriptionText ?? string.Empty
                    }).ToList()
                };

                /*
                CustomerMembershipPrintService printService = new CustomerMembershipPrintService();
                var printDocument = printService.CreatePrintDocument(report);
                */

                CustomerMembershipPrintService printService = new CustomerMembershipPrintService();
                var printDocument = printService.CreatePrintDocument(
                    report,
                    GymMasterAppDemo.Properties.Resources.GymMasterLogo);

                _view.ShowPrintPreview(printDocument);
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά την προετοιμασία εκτύπωσης: " + ex.Message, "Σφάλμα");
            }
        }
    }
}