using System;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class NewMembershipPresenter
    {
        private readonly INewMembershipView _view;
        private readonly IMembershipRepository _membershipRepository;
        private readonly ILookupRepository _lookupRepository;
        private readonly MembershipPaymentService _membershipPaymentService;

        private readonly BindingSource _membershipTypeBindingSource;
        private readonly BindingSource _serviceBindingSource;
        private readonly BindingSource _paymentMethodBindingSource;

        public NewMembershipPresenter(
            INewMembershipView view,
            IMembershipRepository membershipRepository,
            ILookupRepository lookupRepository,
            MembershipPaymentService membershipPaymentService)
        {
            _view = view;
            _membershipRepository = membershipRepository;
            _lookupRepository = lookupRepository;
            _membershipPaymentService = membershipPaymentService;

            _membershipTypeBindingSource = new BindingSource();
            _serviceBindingSource = new BindingSource();
            _paymentMethodBindingSource = new BindingSource();

            _view.SetMembershipTypeListBindingSource(_membershipTypeBindingSource);
            _view.SetServiceListBindingSource(_serviceBindingSource);
            _view.SetPaymentMethodListBindingSource(_paymentMethodBindingSource);

            _view.LoadEvent += OnLoadView;
            _view.MembershipTypeChangedEvent += OnMembershipTypeChanged;
            _view.StartDateChangedEvent += OnStartDateChanged;
            _view.SaveEvent += OnSave;
            _view.CancelEvent += OnCancel;
        }

        private void OnLoadView(object sender, EventArgs e)
        {
            try
            {
                _membershipTypeBindingSource.DataSource = _lookupRepository.GetMembershipTypes();
                _serviceBindingSource.DataSource = _lookupRepository.GetServices();
                _paymentMethodBindingSource.DataSource = _lookupRepository.GetPaymentMethods();

                RecalculateMembershipFields();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη φόρτωση των λιστών: " + ex.Message, "Σφάλμα");
            }
        }

        private void OnMembershipTypeChanged(object sender, EventArgs e)
        {
            RecalculateMembershipFields();
        }

        private void OnStartDateChanged(object sender, EventArgs e)
        {
            RecalculateMembershipFields();
        }

        private void RecalculateMembershipFields()
        {
            int duration = MembershipCalculationHelper.GetDurationDays(_view.SelectedMembershipType);
            DateTime endDate = MembershipCalculationHelper.CalculateEndDate(_view.StartDate, duration);

            _view.SetDuration(duration);
            _view.SetEndDate(endDate.ToString("dd/MM/yyyy"));
        }

        private void OnSave(object sender, EventArgs e)
        {
            try
            {
                if (_view.CustomerId <= 0)
                {
                    _view.ShowMessage("Δεν βρέθηκε έγκυρος πελάτης.", "Προσοχή");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_view.SelectedMembershipType))
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε τύπο συνδρομής.", "Προσοχή");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_view.SelectedServiceCode))
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε υπηρεσία.", "Προσοχή");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_view.SelectedPaymentMethod))
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε τρόπο πληρωμής.", "Προσοχή");
                    return;
                }

                if (_view.Price <= 0)
                {
                    _view.ShowMessage("Το κόστος της συνδρομής πρέπει να είναι μεγαλύτερο από 0.", "Προσοχή");
                    return;
                }

                if (_view.FirstPaymentAmount <= 0)
                {
                    _view.ShowMessage("Το ποσό της πρώτης πληρωμής πρέπει να είναι μεγαλύτερο από 0.", "Προσοχή");
                    return;
                }

                if (_view.FirstPaymentAmount > _view.Price)
                {
                    _view.ShowMessage("Το ποσό της πρώτης πληρωμής δεν μπορεί να είναι μεγαλύτερο από το κόστος της συνδρομής.", "Προσοχή");
                    return;
                }

                int duration = MembershipCalculationHelper.GetDurationDays(_view.SelectedMembershipType);

                if (duration <= 0)
                {
                    _view.ShowMessage("Μη έγκυρος τύπος συνδρομής.", "Προσοχή");
                    return;
                }

                DateTime endDate = MembershipCalculationHelper.CalculateEndDate(_view.StartDate, duration);

                bool hasOverlap = _membershipRepository.HasOverlappingMembership(
                    _view.CustomerId,
                    _view.StartDate,
                    endDate);

                if (hasOverlap)
                {
                    _view.ShowMessage("Υπάρχει ήδη συνδρομή που επικαλύπτεται με το επιλεγμένο χρονικό διάστημα.", "Προσοχή");
                    return;
                }

                string statusCode = MembershipCalculationHelper.GetStatusForNewMembership(
                    _view.Price,
                    _view.FirstPaymentAmount);

                MembershipModel membership = new MembershipModel
                {
                    CustomerId = _view.CustomerId,
                    MembershipType = _view.SelectedMembershipType,
                    Duration = duration,
                    StartDate = _view.StartDate,
                    EndDate = endDate,
                    ServiceCode = _view.SelectedServiceCode,
                    Price = _view.Price,
                    StatusCode = statusCode,
                    Comment = _view.Comment
                };

                PaymentModel payment = new PaymentModel
                {
                    MembershipId = 0,
                    PaymentDate = DateTime.Today,
                    PaymentTime = DateTime.Now.TimeOfDay,
                    MethodType = _view.SelectedPaymentMethod,
                    Amount = _view.FirstPaymentAmount,
                    CreationDate = DateTime.Now,
                    Comment = null
                };

                _membershipPaymentService.CreateMembershipWithFirstPayment(membership, payment);

                _view.ShowMessage("Η νέα συνδρομή αποθηκεύτηκε επιτυχώς.", "Επιτυχία");
                _view.CloseView();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά την αποθήκευση της συνδρομής: " + ex.Message, "Σφάλμα");
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}