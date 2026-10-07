using System;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;
using System.Drawing;
using System.Drawing.Printing;

namespace GymMasterAppDemo.Forms
{
    public partial class MembershipForm : Form, IMembershipView
    {
        private readonly string _connectionString;

        private BindingSource _customerBindingSource;
        private BindingSource _membershipBindingSource;

        public MembershipForm(string connectionString)
        {
            InitializeComponent();

            _connectionString = connectionString;

            AssociateAndRaiseViewEvents();
            ConfigureDataGridViews();
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

        public long SelectedMembershipId
        {
            get
            {
                if (dgvMemberships.CurrentRow == null)
                    return 0;

                MembershipModel membership = dgvMemberships.CurrentRow.DataBoundItem as MembershipModel;
                if (membership == null)
                    return 0;

                return membership.Id;
            }
        }

        public event EventHandler SearchEvent;
        public event EventHandler RefreshEvent;
        public event EventHandler CustomerSelectionChangedEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditPaymentsEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ReturnToHomeRequested;
        public event EventHandler PrintEvent;

        public void SetCustomerListBindingSource(BindingSource customerList)
        {
            _customerBindingSource = customerList;
            dgvCustomers.DataSource = _customerBindingSource;
        }

        public void SetMembershipListBindingSource(BindingSource membershipList)
        {
            _membershipBindingSource = membershipList;
            dgvMemberships.DataSource = _membershipBindingSource;
        }

        public void ShowMessage(string message, string title = "Πληροφορία")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowPrintPreview(PrintDocument printDocument)
        {
            using (PrintPreviewDialog previewDialog = new PrintPreviewDialog())
            {
                previewDialog.Document = printDocument;
                previewDialog.Width = 1200;
                previewDialog.Height = 800;
                previewDialog.StartPosition = FormStartPosition.CenterParent;
                previewDialog.ShowDialog(this);
            }
        }

        public void ClearMemberships()
        {
            dgvMemberships.DataSource = null;
        }

        public void OpenNewMembershipView(long customerId, string customerFullname)
        {
            using (NewMembershipForm form = new NewMembershipForm(_connectionString, customerId, customerFullname))
            {
                form.ShowDialog();
            }
        }

        public void OpenPaymentsMembershipView(long membershipId)
        {
            using (PaymentsMembershipForm form = new PaymentsMembershipForm(_connectionString, membershipId))
            {
                form.ShowDialog();
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

            btnPayments.Click += delegate
            {
                if (EditPaymentsEvent != null)
                    EditPaymentsEvent(this, EventArgs.Empty);
            };

            btnDelete.Click += delegate
            {
                if (DeleteEvent != null)
                    DeleteEvent(this, EventArgs.Empty);
            };

            btnClose.Click += delegate
            {
                //this.Close();
                if (ReturnToHomeRequested != null)
                    ReturnToHomeRequested(this, EventArgs.Empty); if (ReturnToHomeRequested != null)
                    ReturnToHomeRequested(this, EventArgs.Empty);
            };

            btnPrint.Click += delegate
            {
                if (PrintEvent != null)
                    PrintEvent(this, EventArgs.Empty);
            };

            dgvCustomers.SelectionChanged += delegate
            {
                if (CustomerSelectionChangedEvent != null)
                    CustomerSelectionChangedEvent(this, EventArgs.Empty);
            };

            dgvMemberships.CellDoubleClick += delegate
            {
                if (EditPaymentsEvent != null)
                    EditPaymentsEvent(this, EventArgs.Empty);
            };
        }
        private void ConfigureCustomersGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);
            dgvCustomers.Columns.Clear();

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

        private void ConfigureMembershipsGrid()
        {
            dgvMemberships.AutoGenerateColumns = false;
            dgvMemberships.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);
            dgvMemberships.Columns.Clear();

            dgvMemberships.ReadOnly = true;
            dgvMemberships.MultiSelect = false;
            dgvMemberships.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMemberships.AllowUserToAddRows = false;
            dgvMemberships.AllowUserToDeleteRows = false;

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Αριθμ. Συνδρομής",
                Width = 120
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MembershipTypeDescription",
                DataPropertyName = "MembershipTypeDescription",
                HeaderText = "Τύπος Συνδρομής",
                Width = 150
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                DataPropertyName = "StartDate",
                HeaderText = "Έναρξη",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EndDate",
                DataPropertyName = "EndDate",
                HeaderText = "Λήξη",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ServiceDescription",
                DataPropertyName = "ServiceDescription",
                HeaderText = "Πρόγραμμα/Υπηρεσία",
                Width = 170
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                DataPropertyName = "Price",
                HeaderText = "Κόστος (€)",
                Width = 90,
                DefaultCellStyle = { Format = "N2" }
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusDescriptionText",
                DataPropertyName = "StatusDescriptionText",
                HeaderText = "Κατάσταση",
                Width = 120
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comment",
                DataPropertyName = "Comment",
                HeaderText = "Παρατηρήσεις",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerId",
                DataPropertyName = "CustomerId",
                Visible = false
            });
        }

        private void ConfigureDataGridViews()
        {
            ConfigureCustomersGrid();
            ConfigureMembershipsGrid();
        }
    }
}