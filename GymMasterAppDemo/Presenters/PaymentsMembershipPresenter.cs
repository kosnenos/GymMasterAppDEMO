using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class PaymentsMembershipPresenter
    {
        private readonly IPaymentsMembershipView _view;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILookupRepository _lookupRepository;
        private readonly MembershipPaymentService _membershipPaymentService;

        private readonly BindingSource _membershipTypeBindingSource;
        private readonly BindingSource _serviceBindingSource;
        private readonly BindingSource _paymentMethodBindingSource;
        private readonly BindingSource _paymentsBindingSource;

        private MembershipModel _currentMembership;

        public PaymentsMembershipPresenter(
            IPaymentsMembershipView view,
            IMembershipRepository membershipRepository,
            IPaymentRepository paymentRepository,
            ICustomerRepository customerRepository,
            ILookupRepository lookupRepository,
            MembershipPaymentService membershipPaymentService)
        {
            _view = view;
            _membershipRepository = membershipRepository;
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;
            _lookupRepository = lookupRepository;
            _membershipPaymentService = membershipPaymentService;

            _membershipTypeBindingSource = new BindingSource();
            _serviceBindingSource = new BindingSource();
            _paymentMethodBindingSource = new BindingSource();
            _paymentsBindingSource = new BindingSource();

            _view.SetMembershipTypeListBindingSource(_membershipTypeBindingSource);
            _view.SetServiceListBindingSource(_serviceBindingSource);
            _view.SetPaymentMethodListBindingSource(_paymentMethodBindingSource);
            _view.SetPaymentsListBindingSource(_paymentsBindingSource);

            _view.LoadEvent += OnLoadView;
            _view.SaveEvent += OnSave;
            _view.PaymentEvent += OnPayment;
            _view.CancelEvent += OnCancel;
            _view.DeleteEvent += OnDelete;
        }

        private void RefreshMembershipStatus()
        {
            decimal totalPaid = _paymentRepository.GetTotalPaidByMembershipId(_currentMembership.Id);
            string newStatusCode = MembershipCalculationHelper.GetStatusAfterPayment(_currentMembership.Price, totalPaid);

            _membershipRepository.UpdateStatus(_currentMembership.Id, newStatusCode);
        }

        private void OnLoadView(object sender, EventArgs e)
        {
            try
            {
                _membershipTypeBindingSource.DataSource = _lookupRepository.GetMembershipTypes();
                _serviceBindingSource.DataSource = _lookupRepository.GetServices();
                _paymentMethodBindingSource.DataSource = _lookupRepository.GetPaymentMethods();

                LoadMembershipData();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη φόρτωση της φόρμας: " + ex.Message, "Σφάλμα");
            }
        }

        private void LoadMembershipData()
        {
            _currentMembership = _membershipRepository.GetById(_view.MembershipId);

            if (_currentMembership == null)
            {
                _view.ShowMessage("Η συνδρομή δεν βρέθηκε.", "Σφάλμα");
                _view.CloseView();
                return;
            }

            CustomerModel customer = _customerRepository.GetById(_currentMembership.CustomerId);

            string fullname = string.Empty;
            if (customer != null)
                fullname = (customer.LastName + " " + customer.FirstName).Trim();

            decimal totalPaid = _paymentRepository.GetTotalPaidByMembershipId(_currentMembership.Id);
            decimal remainder = MembershipCalculationHelper.CalculateBalance(_currentMembership.Price, totalPaid);

            string statusDesc = _currentMembership.StatusDescriptionText;
            if (string.IsNullOrWhiteSpace(statusDesc))
                statusDesc = _currentMembership.StatusDescription;

            _view.SetCustomerInfo(_currentMembership.CustomerId, fullname);
            _view.SetMembershipInfo(_currentMembership.Id, statusDesc, remainder);

            _view.SetMembershipDetails(
                _currentMembership.MembershipType,
                _currentMembership.ServiceCode,
                _currentMembership.StartDate,
                _currentMembership.EndDate,
                _currentMembership.Comment);

            _paymentsBindingSource.DataSource = _paymentRepository.GetByMembershipId(_currentMembership.Id);

            bool isReadOnly = _currentMembership.StatusCode == "1";
            _view.SetReadOnlyMode(isReadOnly);
        }

        private void OnPayment(object sender, EventArgs e)
        {
            decimal totalPaid = _paymentRepository.GetTotalPaidByMembershipId(_currentMembership.Id);
            decimal remainder = MembershipCalculationHelper.CalculateBalance(_currentMembership.Price, totalPaid);
            _view.SetPaymentAmount(remainder);
        }

        private void OnSave(object sender, EventArgs e)
        {
            try
            {
                if (_currentMembership == null)
                    return;

                _currentMembership.ServiceCode = _view.ServiceCode;
                _currentMembership.Comment = _view.CommentText;

                _membershipRepository.Update(_currentMembership);

                List<PaymentModel> editedPayments = _view.GetEditedPaymentsFromGrid();
                for (int i = 0; i < editedPayments.Count; i++)
                {
                    PaymentModel payment = editedPayments[i];

                    if (payment.Id > 0)
                        _paymentRepository.Update(payment);
                }

                if (_view.PaymentAmount > 0)
                {
                    if (string.IsNullOrWhiteSpace(_view.PaymentMethodType))
                    {
                        _view.ShowMessage("Παρακαλώ επιλέξτε τρόπο πληρωμής.", "Προσοχή");
                        return;
                    }

                    PaymentModel payment = new PaymentModel();
                    payment.MembershipId = _currentMembership.Id;
                    payment.PaymentDate = DateTime.Today;
                    payment.PaymentTime = DateTime.Now.TimeOfDay;
                    payment.MethodType = _view.PaymentMethodType;
                    payment.Amount = _view.PaymentAmount;
                    payment.CreationDate = DateTime.Now;
                    payment.Comment = null;

                    _membershipPaymentService.AddPaymentAndRefreshStatus(_currentMembership.Id, _currentMembership.Price, payment);
                }
                else
                {
                    RefreshMembershipStatus();
                }

                LoadMembershipData();
                _view.SetPaymentAmount(0);

                _view.ShowMessage("Οι αλλαγές αποθηκεύτηκαν επιτυχώς.", "Επιτυχία");
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά την αποθήκευση: " + ex.Message, "Σφάλμα");
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _view.CloseView();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedPaymentId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα μία πληρωμή.", "Προσοχή");
                    return;
                }

                DialogResult result = _view.ShowQuestion(
                    "Θέλετε σίγουρα να διαγράψετε την επιλεγμένη πληρωμή;",
                    "Επιβεβαίωση διαγραφής");

                if (result != DialogResult.Yes)
                    return;

                _paymentRepository.Delete(_view.SelectedPaymentId);

                RefreshMembershipStatus();
                LoadMembershipData();
                _view.SetPaymentAmount(0);

                _view.ShowMessage("Η πληρωμή διαγράφηκε επιτυχώς.", "Επιτυχία");
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη διαγραφή της πληρωμής: " + ex.Message, "Σφάλμα");
            }
        }
    }
}