using System;
using System.Windows.Forms;
using System.Drawing;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Forms
{
    public partial class CustomerForm : Form, ICustomerView
    {
        public CustomerForm()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            ConfigureDataGridView();
        }

        public string SearchValue => txtSearch.Text.Trim();

        public event EventHandler SearchEvent;
        public event EventHandler RefreshEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler CloseEvent;
        public event EventHandler LoadCustomersEvent;
        public event EventHandler DoubleClickEditEvent;
        public event EventHandler ReturnToHomeRequested;

        private void AssociateAndRaiseViewEvents()
        {
            this.Load += (s, e) => LoadCustomersEvent?.Invoke(this, EventArgs.Empty);

            btnSearch.Click += (s, e) => SearchEvent?.Invoke(this, EventArgs.Empty);

            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                    e.SuppressKeyPress = true;
                }
            };

            btnRefresh.Click += (s, e) => RefreshEvent?.Invoke(this, EventArgs.Empty);
            btnNewCustomer.Click += (s, e) => AddNewEvent?.Invoke(this, EventArgs.Empty);
            btnEditCustomer.Click += (s, e) => EditEvent?.Invoke(this, EventArgs.Empty);
            //btnClose.Click += (s, e) => CloseEvent?.Invoke(this, EventArgs.Empty);
            btnClose.Click += (s, e) => ReturnToHomeRequested?.Invoke(this, EventArgs.Empty);

            dgvCustomers.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    DoubleClickEditEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ConfigureDataGridView()
        {
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular); 
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Κωδ. Πελάτη",
                Name = "colId"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                HeaderText = "Επώνυμο",
                Name = "colLastName"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FirstName",
                HeaderText = "Όνομα",
                Name = "colFirstName"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FatherName",
                HeaderText = "Πατρώνυμο",
                Name = "colFatherName"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Gender",
                HeaderText = "Φύλο",
                Name = "colGender"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Birthday",
                HeaderText = "Ημ. Γέννησης",
                Name = "colBirthday",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OccupationDescription",
                HeaderText = "Επάγγελμα",
                Name = "colOccupation"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HealthRecordInfo",
                HeaderText = "Ιατρικός Φάκελος",
                Name = "colHealth"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Διεύθυνση",
                Name = "colAddress"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "City",
                HeaderText = "Πόλη",
                Name = "colCity"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Mobile",
                HeaderText = "Τηλέφωνο",
                Name = "colMobile"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Home",
                HeaderText = "Σταθερό Τηλέφωνο",
                Name = "colHome"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "colEmail"
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreationDate",
                HeaderText = "Ημ. Εγγραφής",
                Name = "colCreationDate",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Comments",
                HeaderText = "Παρατηρήσεις",
                Name = "colComments"
            });
        }

        public void SetCustomerListBindingSource(BindingSource source)
        {
            dgvCustomers.DataSource = source;
        }

        public long GetSelectedCustomerId()
        {
            if (dgvCustomers.CurrentRow == null)
                return 0;

            return Convert.ToInt64(dgvCustomers.CurrentRow.Cells["colId"].Value);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Προειδοποίηση", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void OpenNewCustomerView()
        {
            ICustomerEditView view = new CustomerEditForm();
            ICustomerRepository customerRepository = new CustomerRepository(DbConfig.ConnectionString);
            IOccupationRepository occupationRepository = new OccupationRepository(DbConfig.ConnectionString);
            IHealthRecordRepository healthRecordRepository = new HealthRecordRepository(DbConfig.ConnectionString);

            CustomerEditPresenter presenter = new CustomerEditPresenter(
                view,
                customerRepository,
                occupationRepository,
                healthRecordRepository,
                GymMasterAppDemo.Models.CustomerEditMode.Add);

            ((Form)view).StartPosition = FormStartPosition.CenterScreen;
            ((Form)view).ShowDialog();

            RefreshEvent?.Invoke(this, EventArgs.Empty);
        }

        public void OpenEditCustomerView(long customerId)
        {
            ICustomerEditView view = new CustomerEditForm();
            ICustomerRepository customerRepository = new CustomerRepository(DbConfig.ConnectionString);
            IOccupationRepository occupationRepository = new OccupationRepository(DbConfig.ConnectionString);
            IHealthRecordRepository healthRecordRepository = new HealthRecordRepository(DbConfig.ConnectionString);

            CustomerEditPresenter presenter = new CustomerEditPresenter(
                view,
                customerRepository,
                occupationRepository,
                healthRecordRepository,
                GymMasterAppDemo.Models.CustomerEditMode.Edit,
                customerId);

            ((Form)view).StartPosition = FormStartPosition.CenterScreen;
            ((Form)view).ShowDialog();

            RefreshEvent?.Invoke(this, EventArgs.Empty);
        }

        public void CloseView()
        {
            this.Close();
        }
    }
}