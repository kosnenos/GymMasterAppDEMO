using System;
using System.Windows.Forms;
using System.Drawing;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;
using GymMasterAppDemo.Data;

namespace GymMasterAppDemo.Forms
{
    public partial class WorkoutProgramForm : Form, IWorkoutProgramView
    {
        private readonly string _connectionString;

        private BindingSource _customerBindingSource;
        private BindingSource _workoutProgramBindingSource;

        public WorkoutProgramForm(string connectionString)
        {
            InitializeComponent();

            _connectionString = connectionString;

            AssociateAndRaiseViewEvents();
            ConfigureDataGridViews();
            DisableDeferredButtons();
        }

        public string SearchValue
        {
            get { return txtSearch.Text; }
        }

        public long SelectedCustomerId
        {
            get
            {
                if (dgvCustomers.CurrentRow == null)
                    return 0;

                CustomerListModel customer = dgvCustomers.CurrentRow.DataBoundItem as CustomerListModel;
                if (customer == null)
                    return 0;

                return customer.Id;
            }
        }

        public string SelectedCustomerFullname
        {
            get
            {
                if (dgvCustomers.CurrentRow == null)
                    return string.Empty;

                CustomerListModel customer = dgvCustomers.CurrentRow.DataBoundItem as CustomerListModel;
                if (customer == null)
                    return string.Empty;

                return (customer.LastName + " " + customer.FirstName).Trim();
            }
        }

        public long SelectedWorkoutProgramId
        {
            get
            {
                if (dgvWorkoutPrograms.CurrentRow == null)
                    return 0;

                WorkoutProgramModel program = dgvWorkoutPrograms.CurrentRow.DataBoundItem as WorkoutProgramModel;
                if (program == null)
                    return 0;

                return program.Id;
            }
        }

        public event EventHandler SearchEvent;
        public event EventHandler RefreshEvent;
        public event EventHandler CustomerSelectionChangedEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditUpdateEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler PrintEvent;
        public event EventHandler ReturnToHomeRequested;

        public void SetCustomerListBindingSource(BindingSource customerList)
        {
            _customerBindingSource = customerList;
            dgvCustomers.DataSource = _customerBindingSource;
        }

        public void SetWorkoutProgramListBindingSource(BindingSource workoutProgramList)
        {
            _workoutProgramBindingSource = workoutProgramList;
            dgvWorkoutPrograms.DataSource = _workoutProgramBindingSource;
        }

        public void ShowMessage(string message, string title = "Πληροφορία")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearWorkoutPrograms()
        {
            dgvWorkoutPrograms.DataSource = null;
        }

        public void OpenAddWorkoutProgramView(long customerId, string customerFullname)
        {
            using (WorkoutProgramEditForm form = new WorkoutProgramEditForm())
            {
                IWorkoutProgramRepository workoutProgramRepository = new WorkoutProgramRepository(_connectionString);
                ILookupRepository lookupRepository = new LookupRepository(_connectionString);

                new WorkoutProgramEditPresenter(
                    form,
                    workoutProgramRepository,
                    lookupRepository,
                    WorkoutProgramEditMode.Add,
                    customerId,
                    customerFullname);

                form.ShowDialog(this);
            }
        }

        public void OpenEditWorkoutProgramView(long customerId, string customerFullname, long workoutProgramId)
        {
            using (WorkoutProgramEditForm form = new WorkoutProgramEditForm())
            {
                IWorkoutProgramRepository workoutProgramRepository = new WorkoutProgramRepository(_connectionString);
                ILookupRepository lookupRepository = new LookupRepository(_connectionString);

                new WorkoutProgramEditPresenter(
                    form,
                    workoutProgramRepository,
                    lookupRepository,
                    WorkoutProgramEditMode.Edit,
                    customerId,
                    customerFullname,
                    workoutProgramId);

                form.ShowDialog(this);
            }
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate
            {
                if (SearchEvent != null)
                    SearchEvent(this, EventArgs.Empty);
            };

            txtSearch.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (SearchEvent != null)
                        SearchEvent(this, EventArgs.Empty);

                    e.SuppressKeyPress = true;
                }
            };

            btnRefresh.Click += delegate
            {
                if (RefreshEvent != null)
                    RefreshEvent(this, EventArgs.Empty);
            };

            btnAddNew.Click += delegate
            {
                if (AddNewEvent != null)
                    AddNewEvent(this, EventArgs.Empty);
            };

            btnEditUpdate.Click += delegate
            {
                if (EditUpdateEvent != null)
                    EditUpdateEvent(this, EventArgs.Empty);
            };

            btnDelete.Click += delegate
            {
                if (DeleteEvent != null)
                    DeleteEvent(this, EventArgs.Empty);
            };

            btnPrint.Click += delegate
            {
                if (PrintEvent != null)
                    PrintEvent(this, EventArgs.Empty);
            };

            btnClose.Click += delegate
            {
                if (ReturnToHomeRequested != null)
                    ReturnToHomeRequested(this, EventArgs.Empty);
                else
                    this.Close();
            };

            dgvCustomers.SelectionChanged += delegate
            {
                if (CustomerSelectionChangedEvent != null)
                    CustomerSelectionChangedEvent(this, EventArgs.Empty);
            };

            dgvWorkoutPrograms.CellDoubleClick += delegate
            {
                if (EditUpdateEvent != null)
                    EditUpdateEvent(this, EventArgs.Empty);
            };
        }

        private void DisableDeferredButtons()
        {
            btnPrint.Enabled = true;
        }

        private void ConfigureCustomersGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);

            dgvCustomers.ReadOnly = true;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Κωδ. Πελάτη",
                Width = 90
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastName",
                DataPropertyName = "LastName",
                HeaderText = "Επώνυμο",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FirstName",
                DataPropertyName = "FirstName",
                HeaderText = "Όνομα",
                Width = 110
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FatherName",
                DataPropertyName = "FatherName",
                HeaderText = "Πατρώνυμο",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreationDate",
                DataPropertyName = "CreationDate",
                HeaderText = "Ημ. Εγγραφής",
                Width = 110,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Mobile",
                DataPropertyName = "Mobile",
                HeaderText = "Κινητό",
                Width = 110
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "E-mail",
                Width = 160
            });
        }

        private void ConfigureWorkoutProgramsGrid()
        {
            dgvWorkoutPrograms.AutoGenerateColumns = false;
            dgvWorkoutPrograms.Columns.Clear();
            dgvWorkoutPrograms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);

            dgvWorkoutPrograms.ReadOnly = true;
            dgvWorkoutPrograms.MultiSelect = false;
            dgvWorkoutPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWorkoutPrograms.AllowUserToAddRows = false;
            dgvWorkoutPrograms.AllowUserToDeleteRows = false;

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Αριθμ. Προγρ/τος",
                Width = 120,
                DefaultCellStyle = { Format = "D5" }
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GoalDescription",
                DataPropertyName = "GoalDescription",
                HeaderText = "Στόχος",
                Width = 160
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Frequency",
                DataPropertyName = "Frequency",
                HeaderText = "Συχνότητα (Ημέρες)",
                Width = 140
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                DataPropertyName = "StartDate",
                HeaderText = "Έναρξη",
                Width = 110,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EndDate",
                DataPropertyName = "EndDate",
                HeaderText = "Λήξη",
                Width = 110,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comments",
                DataPropertyName = "Comments",
                HeaderText = "Παρατηρήσεις",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerId",
                DataPropertyName = "CustomerId",
                Visible = false
            });

            dgvWorkoutPrograms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GoalCode",
                DataPropertyName = "GoalCode",
                Visible = false
            });
        }

        private void ConfigureDataGridViews()
        {
            ConfigureCustomersGrid();
            ConfigureWorkoutProgramsGrid();
        }
    }
}