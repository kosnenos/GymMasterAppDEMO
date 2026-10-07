using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Printing;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class WorkoutProgramPresenter
    {
        private readonly IWorkoutProgramView _view;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkoutProgramRepository _workoutProgramRepository;

        private readonly BindingSource _customerBindingSource;
        private readonly BindingSource _workoutProgramBindingSource;

        public WorkoutProgramPresenter(
            IWorkoutProgramView view,
            ICustomerRepository customerRepository,
            IWorkoutProgramRepository workoutProgramRepository)
        {
            _view = view;
            _customerRepository = customerRepository;
            _workoutProgramRepository = workoutProgramRepository;

            _customerBindingSource = new BindingSource();
            _workoutProgramBindingSource = new BindingSource();

            _view.SetCustomerListBindingSource(_customerBindingSource);
            _view.SetWorkoutProgramListBindingSource(_workoutProgramBindingSource);

            _view.SearchEvent += SearchCustomers;
            _view.RefreshEvent += RefreshAll;
            _view.CustomerSelectionChangedEvent += LoadProgramsForSelectedCustomer;
            _view.AddNewEvent += AddNewWorkoutProgram;
            _view.EditUpdateEvent += EditWorkoutProgram;
            _view.DeleteEvent += DeleteWorkoutProgram;
            _view.PrintEvent += PrintWorkoutProgram;

            LoadActiveCustomers();
        }

        public void LoadWorkoutPrograms()
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
                    _view.ClearWorkoutPrograms();
                    return;
                }

                LoadProgramsForSelectedCustomer();
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
                    _view.ClearWorkoutPrograms();
                    return;
                }

                LoadProgramsForSelectedCustomer();
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

        private void LoadProgramsForSelectedCustomer(object sender = null, EventArgs e = null)
        {
            try
            {
                if (_view.SelectedCustomerId <= 0)
                {
                    _view.ClearWorkoutPrograms();
                    return;
                }

                List<WorkoutProgramModel> programs =
                    _workoutProgramRepository.GetByCustomerId(_view.SelectedCustomerId);

                _workoutProgramBindingSource.DataSource = programs;
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη φόρτωση προγραμμάτων: " + ex.Message, "Σφάλμα");
            }
        }

        private void AddNewWorkoutProgram(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedCustomerId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα έναν πελάτη.", "Προσοχή");
                    return;
                }

                _view.OpenAddWorkoutProgramView(_view.SelectedCustomerId, _view.SelectedCustomerFullname);
                LoadProgramsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά το άνοιγμα της φόρμας νέου προγράμματος: " + ex.Message, "Σφάλμα");
            }
        }

        private void EditWorkoutProgram(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedCustomerId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα έναν πελάτη.", "Προσοχή");
                    return;
                }

                if (_view.SelectedWorkoutProgramId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα ένα πρόγραμμα.", "Προσοχή");
                    return;
                }

                _view.OpenEditWorkoutProgramView(
                    _view.SelectedCustomerId,
                    _view.SelectedCustomerFullname,
                    _view.SelectedWorkoutProgramId);

                LoadProgramsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά το άνοιγμα της φόρμας αλλαγής προγράμματος: " + ex.Message, "Σφάλμα");
            }
        }

        private void DeleteWorkoutProgram(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedWorkoutProgramId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα ένα πρόγραμμα.", "Προσοχή");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Θέλετε σίγουρα να διαγράψετε το επιλεγμένο πρόγραμμα και τις λεπτομέρειές του;",
                    "Επιβεβαίωση διαγραφής",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                _workoutProgramRepository.Delete(_view.SelectedWorkoutProgramId);

                _view.ShowMessage("Το πρόγραμμα διαγράφηκε επιτυχώς.", "Επιτυχία");
                LoadProgramsForSelectedCustomer();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά τη διαγραφή προγράμματος: " + ex.Message, "Σφάλμα");
            }
        }

        private void PrintWorkoutProgram(object sender, EventArgs e)
        {
            try
            {
                if (_view.SelectedWorkoutProgramId <= 0)
                {
                    _view.ShowMessage("Παρακαλώ επιλέξτε πρώτα ένα πρόγραμμα.", "Προσοχή");
                    return;
                }

                WorkoutProgramPrintHeaderModel header =
                    _workoutProgramRepository.GetPrintHeader(_view.SelectedWorkoutProgramId);

                List<WorkoutProgramPrintDetailModel> details =
                    _workoutProgramRepository.GetPrintDetails(_view.SelectedWorkoutProgramId);

                if (header == null)
                {
                    _view.ShowMessage("Δεν βρέθηκαν στοιχεία για εκτύπωση.", "Προσοχή");
                    return;
                }

                if (details == null || details.Count == 0)
                {
                    _view.ShowMessage("Το πρόγραμμα δεν περιέχει ασκήσεις για εκτύπωση.", "Προσοχή");
                    return;
                }

                Image logo = GymMasterAppDemo.Properties.Resources.GymMasterLogo;

                WorkoutProgramPrintDocument printer =
                    new WorkoutProgramPrintDocument(header, details, logo);

                printer.ShowPreview();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Σφάλμα κατά την εκτύπωση προγράμματος: " + ex.Message, "Σφάλμα");
            }
        }
    }
}