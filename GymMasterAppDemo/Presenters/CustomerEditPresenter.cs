using System;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class CustomerEditPresenter
    {
        private readonly ICustomerEditView _view;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOccupationRepository _occupationRepository;
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly CustomerEditMode _mode;
        private readonly long? _customerId;
        private readonly BindingSource _occupationBindingSource;

        private long? _currentHealthId;
        private bool _newCustomerInserted;

        public CustomerEditPresenter(
            ICustomerEditView view,
            ICustomerRepository customerRepository,
            IOccupationRepository occupationRepository,
            IHealthRecordRepository healthRecordRepository,
            CustomerEditMode mode,
            long? customerId = null)
        {
            _view = view;
            _customerRepository = customerRepository;
            _occupationRepository = occupationRepository;
            _healthRecordRepository = healthRecordRepository;
            _mode = mode;
            _customerId = customerId;
            _occupationBindingSource = new BindingSource();

            _view.LoadFormEvent += OnLoadForm;
            _view.SaveEvent += OnSave;
            _view.CancelEvent += OnCancel;
            _view.CustomerIdLeaveEvent += OnCustomerIdLeave;
        }

        private void OnLoadForm(object sender, EventArgs e)
        {
            LoadOccupations();

            if (_mode == CustomerEditMode.Add)
            {
                _view.FormTitle = "Νέος Πελάτης | GymMaster";
                _view.SaveButtonText = "   Αποθήκευση";
                _view.SetCustomerIdReadOnly(false);
                _view.EnableHealthTab(false);
                _view.City = string.IsNullOrWhiteSpace(_view.City) ? "Θεσσαλονίκη" : _view.City;
                _view.CreationDate = DateTime.Today;

                _currentHealthId = null;
                _newCustomerInserted = false;
            }
            else
            {
                _view.FormTitle = "Επεξεργασία Στοιχείων Πελάτη";
                _view.SaveButtonText = "   Ενημέρωση";
                _view.SetCustomerIdReadOnly(true);
                LoadCustomerForEdit();
            }
        }

        private void LoadOccupations()
        {
            var occupations = _occupationRepository.GetAll();
            _occupationBindingSource.DataSource = occupations;
            _view.SetOccupationBindingSource(_occupationBindingSource);
        }

        private void LoadCustomerForEdit()
        {
            if (!_customerId.HasValue)
                return;

            var customer = _customerRepository.GetById(_customerId.Value);
            if (customer == null)
            {
                _view.ShowWarning("Ο πελάτης δεν βρέθηκε.");
                _view.CloseView();
                return;
            }

            _currentHealthId = customer.HealthId;

            _view.CustomerIdText = customer.Id.ToString();
            _view.LastName = customer.LastName;
            _view.FirstName = customer.FirstName;
            _view.FatherName = customer.FatherName;
            _view.Gender = customer.Gender;
            _view.OccupationId = customer.OccupationId;
            _view.Birthday = customer.Birthday;
            _view.Address = customer.Address;
            _view.City = customer.City;
            _view.Mobile = customer.Mobile;
            _view.Home = customer.Home;
            _view.Email = customer.Email;
            _view.CreationDate = customer.CreationDate;
            _view.Comments = customer.Comments;

            string lastName = string.IsNullOrWhiteSpace(customer.LastName) ? string.Empty : customer.LastName.Trim();
            string firstName = string.IsNullOrWhiteSpace(customer.FirstName) ? string.Empty : customer.FirstName.Trim();

            _view.FormTitle = "Επεξεργασία Πελάτη | Κωδ: "
                + customer.Id.ToString("D4")
                + " | "
                + (lastName + " " + firstName).Trim();

            if (_currentHealthId.HasValue)
            {
                _view.EnableHealthTab(true);
                LoadHealthRecord(_currentHealthId.Value);
            }
            else
            {
                _view.EnableHealthTab(false);

                var result = _view.ShowQuestion("Ο πελάτης δεν έχει συμπληρωμένο Ιατρικό Φάκελο. Θέλετε να τον συμπληρώσετε τώρα;");
                if (result == DialogResult.Yes)
                {
                    _view.EnableHealthTab(true);
                    _view.SelectHealthTab();
                }
            }
        }

        private void LoadHealthRecord(long healthId)
        {
            var health = _healthRecordRepository.GetById(healthId);
            if (health == null)
                return;

            _view.HasBodyPain = health.HasBodyPain;
            _view.BodyPainDesc = health.BodyPainDesc;

            _view.IsObest = health.IsObest;
            _view.IsSmoker = health.IsSmoker;
            _view.FamilyHeartHistory = health.FamilyHeartHistory;
            _view.HasHypertasis = health.HasHypertasis;
            _view.HasDiabetes = health.HasDiabetes;
            _view.HasHeartIssues = health.HasHeartIssues;
            _view.HasAsthma = health.HasAsthma;
            _view.HasThyroedes = health.HasThyroedes;
            _view.HasArthritis = health.HasArthritis;
            _view.HasOsteoporosis = health.HasOsteoporosis;
            _view.HasAllergies = health.HasAllergies;
            _view.HasMyosceletic = health.HasMyosceletic;
            _view.MyoskeleticDesc = health.MyoskeleticDesc;

            _view.HasOther = health.HasOther;
            _view.OtherDesc = health.OtherDesc;

            _view.EmergencyContact = health.EmergencyContact;
            _view.EmergencyPhone = health.EmergencyPhone;
            _view.HealthComment = health.Comment;
        }

        private void OnCustomerIdLeave(object sender, EventArgs e)
        {
            if (_mode != CustomerEditMode.Add || _newCustomerInserted)
                return;

            if (string.IsNullOrWhiteSpace(_view.CustomerIdText))
                return;

            if (!long.TryParse(_view.CustomerIdText, out long id))
            {
                _view.ShowWarning("Ο Κωδικός Πελάτη πρέπει να περιέχει μόνο ψηφία.");
                return;
            }

            if (_customerRepository.CustomerIdExists(id))
            {
                _view.ShowWarning("Ο Κωδικός Πελάτη υπάρχει ήδη.");
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            var customer = BuildCustomerModel();

            // 1η αποθήκευση σε Add mode: δημιουργούμε πρώτα τον πελάτη
            if (_mode == CustomerEditMode.Add && !_newCustomerInserted)
            {
                _customerRepository.Add(customer);
                _newCustomerInserted = true;

                _view.SetCustomerIdReadOnly(true);

                var result = _view.ShowQuestion("Ο πελάτης καταχωρήθηκε. Θέλετε να συμπληρώσετε τώρα τον Ιατρικό Φάκελο;");
                if (result == DialogResult.Yes)
                {
                    _view.EnableHealthTab(true);
                    _view.SelectHealthTab();
                    _view.FormTitle = "Επεξεργασία Στοιχείων Πελάτη";
                    _view.SaveButtonText = "Ενημέρωση";
                    _view.ShowMessage("Συμπληρώστε τον Ιατρικό Φάκελο και πατήστε Ενημέρωση.");
                    return;
                }

                _view.ShowMessage("Ο πελάτης καταχωρήθηκε επιτυχώς.");
                _view.CloseView();
                return;
            }

            bool hasHealthData = HasAnyHealthData();

            if (hasHealthData)
            {
                if (!ValidateHealthRecord())
                    return;

                var health = BuildHealthRecordModel();

                if (_currentHealthId.HasValue)
                {
                    health.Id = _currentHealthId.Value;
                    _healthRecordRepository.Edit(health);
                }
                else
                {
                    _currentHealthId = _healthRecordRepository.Add(health);
                }
            }

            customer.HealthId = _currentHealthId;
            _customerRepository.Edit(customer);

            if (hasHealthData || _currentHealthId.HasValue)
                _view.ShowMessage("Τα στοιχεία του πελάτη και ο Ιατρικός Φάκελος αποθηκεύτηκαν επιτυχώς.");
            else
                _view.ShowMessage("Τα στοιχεία του πελάτη ενημερώθηκαν επιτυχώς.");

            _view.CloseView();
        }

        private CustomerModel BuildCustomerModel()
        {
            return new CustomerModel
            {
                Id = long.Parse(_view.CustomerIdText),
                LastName = _view.LastName.Trim(),
                FirstName = _view.FirstName.Trim(),
                FatherName = string.IsNullOrWhiteSpace(_view.FatherName) ? null : _view.FatherName.Trim(),
                Gender = string.IsNullOrWhiteSpace(_view.Gender) ? null : _view.Gender,
                OccupationId = string.IsNullOrWhiteSpace(_view.OccupationId) ? null : _view.OccupationId,
                Birthday = _view.Birthday,
                Address = string.IsNullOrWhiteSpace(_view.Address) ? null : _view.Address.Trim(),
                City = string.IsNullOrWhiteSpace(_view.City) ? "Θεσσαλονίκη" : _view.City.Trim(),
                Mobile = _view.Mobile.Trim(),
                Home = string.IsNullOrWhiteSpace(_view.Home) ? null : _view.Home.Trim(),
                Email = _view.Email.Trim(),
                Status = true,
                CreationDate = _view.CreationDate,
                Comments = string.IsNullOrWhiteSpace(_view.Comments) ? null : _view.Comments.Trim(),
                HealthId = _currentHealthId
            };
        }

        private HealthRecordModel BuildHealthRecordModel()
        {
            return new HealthRecordModel
            {
                HasBodyPain = _view.HasBodyPain,
                BodyPainDesc = _view.HasBodyPain == true ? NullIfEmpty(_view.BodyPainDesc) : null,

                IsObest = _view.IsObest,
                IsSmoker = _view.IsSmoker,
                FamilyHeartHistory = _view.FamilyHeartHistory,
                HasHypertasis = _view.HasHypertasis,
                HasDiabetes = _view.HasDiabetes,
                HasHeartIssues = _view.HasHeartIssues,
                HasAsthma = _view.HasAsthma,
                HasThyroedes = _view.HasThyroedes,
                HasArthritis = _view.HasArthritis,
                HasOsteoporosis = _view.HasOsteoporosis,
                HasAllergies = _view.HasAllergies,
                HasMyosceletic = _view.HasMyosceletic,
                MyoskeleticDesc = _view.HasMyosceletic == true ? NullIfEmpty(_view.MyoskeleticDesc) : null,

                HasOther = _view.HasOther,
                OtherDesc = _view.HasOther == true ? NullIfEmpty(_view.OtherDesc) : null,

                EmergencyContact = NullIfEmpty(_view.EmergencyContact),
                EmergencyPhone = NullIfEmpty(_view.EmergencyPhone),
                Comment = NullIfEmpty(_view.HealthComment)
            };
        }

        private bool HasAnyHealthData()
        {
            return _view.HasBodyPain.HasValue ||
                   !string.IsNullOrWhiteSpace(_view.BodyPainDesc) ||
                   _view.IsObest.HasValue ||
                   _view.IsSmoker.HasValue ||
                   _view.FamilyHeartHistory.HasValue ||
                   _view.HasHypertasis.HasValue ||
                   _view.HasDiabetes.HasValue ||
                   _view.HasHeartIssues.HasValue ||
                   _view.HasAsthma.HasValue ||
                   _view.HasThyroedes.HasValue ||
                   _view.HasArthritis.HasValue ||
                   _view.HasOsteoporosis.HasValue ||
                   _view.HasAllergies.HasValue ||
                   _view.HasMyosceletic.HasValue ||
                   !string.IsNullOrWhiteSpace(_view.MyoskeleticDesc) ||
                   _view.HasOther.HasValue ||
                   !string.IsNullOrWhiteSpace(_view.OtherDesc) ||
                   !string.IsNullOrWhiteSpace(_view.EmergencyContact) ||
                   !string.IsNullOrWhiteSpace(_view.EmergencyPhone) ||
                   !string.IsNullOrWhiteSpace(_view.HealthComment);
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(_view.CustomerIdText) ||
                string.IsNullOrWhiteSpace(_view.LastName) ||
                string.IsNullOrWhiteSpace(_view.FirstName) ||
                string.IsNullOrWhiteSpace(_view.Mobile) ||
                string.IsNullOrWhiteSpace(_view.Email))
            {
                _view.ShowWarning("Τα πεδία Κωδικός Πελάτη, Επώνυμο, Όνομα, Τηλέφωνο και Email είναι υποχρεωτικά.");
                return false;
            }

            if (!long.TryParse(_view.CustomerIdText, out long customerId))
            {
                _view.ShowWarning("Ο Κωδικός Πελάτη πρέπει να περιέχει μόνο ψηφία.");
                return false;
            }

            if (_mode == CustomerEditMode.Add && !_newCustomerInserted && _customerRepository.CustomerIdExists(customerId))
            {
                _view.ShowWarning("Ο Κωδικός Πελάτη υπάρχει ήδη.");
                return false;
            }

            if (_view.Mobile.Length != 10 || !_view.Mobile.All(char.IsDigit))
            {
                _view.ShowWarning("Το τηλέφωνο πρέπει να έχει ακριβώς 10 ψηφία.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(_view.Home))
            {
                if (_view.Home.Length != 10 || !_view.Home.All(char.IsDigit))
                {
                    _view.ShowWarning("Το σταθερό τηλέφωνο πρέπει να έχει ακριβώς 10 ψηφία.");
                    return false;
                }
            }

            if (!IsValidEmail(_view.Email))
            {
                _view.ShowWarning("Το Email δεν έχει σωστή μορφή.");
                return false;
            }

            if (_view.CreationDate == DateTime.MinValue)
            {
                _view.ShowWarning("Η Ημερομηνία Εγγραφής είναι υποχρεωτική.");
                return false;
            }

            return true;
        }

        private bool ValidateHealthRecord()
        {
            if (_view.HasBodyPain == true && string.IsNullOrWhiteSpace(_view.BodyPainDesc))
            {
                _view.ShowWarning("Συμπληρώστε περιγραφή για τον σωματικό πόνο.");
                return false;
            }

            if (_view.HasMyosceletic == true && string.IsNullOrWhiteSpace(_view.MyoskeleticDesc))
            {
                _view.ShowWarning("Συμπληρώστε περιγραφή για τα μυοσκελετικά.");
                return false;
            }

            if (_view.HasOther == true && string.IsNullOrWhiteSpace(_view.OtherDesc))
            {
                _view.ShowWarning("Συμπληρώστε περιγραφή για το άλλο θέμα.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(_view.EmergencyPhone) && !_view.EmergencyPhone.All(char.IsDigit))
            {
                _view.ShowWarning("Το τηλέφωνο έκτακτης ανάγκης πρέπει να περιέχει μόνο ψηφία.");
                return false;
            }

            return true;
        }

        private string NullIfEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}