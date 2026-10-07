using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Forms
{
    public partial class PaymentsMembershipForm : Form, IPaymentsMembershipView
    {
        private readonly string _connectionString;
        private readonly long _membershipId;

        private BindingSource _membershipTypeBindingSource;
        private BindingSource _serviceBindingSource;
        private BindingSource _paymentMethodBindingSource;
        private BindingSource _paymentsBindingSource;

        public PaymentsMembershipForm(string connectionString, long membershipId)
        {
            InitializeComponent();

            _connectionString = connectionString;
            _membershipId = membershipId;

            this.Text = "Κινήσεις Πληρωμής | Αριθμ. Συνδρομής: " + _membershipId.ToString("D4");

            AssociateAndRaiseViewEvents();
            ConfigureControls();
            ConfigurePaymentsGrid();

            IMembershipRepository membershipRepository = new MembershipRepository(_connectionString);
            IPaymentRepository paymentRepository = new PaymentRepository(_connectionString);
            ICustomerRepository customerRepository = new CustomerRepository(_connectionString);
            ILookupRepository lookupRepository = new LookupRepository(_connectionString);
            MembershipPaymentService membershipPaymentService = new MembershipPaymentService(_connectionString);

            new PaymentsMembershipPresenter(
                this,
                membershipRepository,
                paymentRepository,
                customerRepository,
                lookupRepository,
                membershipPaymentService);
        }

        public List<PaymentModel> GetEditedPaymentsFromGrid()
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            dgvPayments.EndEdit();

            for (int i = 0; i < dgvPayments.Rows.Count; i++)
            {
                DataGridViewRow row = dgvPayments.Rows[i];
                if (row.IsNewRow)
                    continue;

                object idValue = row.Cells["Id"].Value;
                if (idValue == null || idValue == DBNull.Value)
                    continue;

                long id = Convert.ToInt64(idValue);

                DateTime paymentDate;
                TimeSpan paymentTime;
                decimal amount;

                string paymentDateText = row.Cells["PaymentDate"].Value == null ? string.Empty : row.Cells["PaymentDate"].Value.ToString();
                string paymentTimeText = row.Cells["PaymentTimeDisplay"].Value == null ? string.Empty : row.Cells["PaymentTimeDisplay"].Value.ToString();
                string methodType = row.Cells["MethodType"].Value == null ? null : row.Cells["MethodType"].Value.ToString();
                string amountText = row.Cells["Amount"].Value == null ? string.Empty : row.Cells["Amount"].Value.ToString();

                if (!DateTime.TryParse(paymentDateText, out paymentDate))
                    continue;

                if (!TimeSpan.TryParse(paymentTimeText, out paymentTime))
                    continue;

                if (!decimal.TryParse(amountText, out amount))
                    continue;

                PaymentModel payment = new PaymentModel();
                payment.Id = id;
                payment.MembershipId = _membershipId;
                payment.PaymentDate = paymentDate.Date;
                payment.PaymentTime = paymentTime;
                payment.MethodType = methodType;
                payment.Amount = amount;

                payments.Add(payment);
            }

            return payments;
        }

        public long MembershipId
        {
            get { return _membershipId; }
        }

        public string ServiceCode
        {
            get
            {
                return cmbService.SelectedValue == null ? null : cmbService.SelectedValue.ToString();
            }
        }

        public string PaymentMethodType
        {
            get
            {
                return cmbPaymentMethod.SelectedValue == null ? null : cmbPaymentMethod.SelectedValue.ToString();
            }
        }

        public decimal PaymentAmount
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtPaymentAmount.Text, out value) ? value : 0;
            }
        }

        public string CommentText
        {
            get { return txtComments.Text.Trim(); }
        }

        public event EventHandler LoadEvent;
        public event EventHandler SaveEvent;
        public event EventHandler PaymentEvent;
        public event EventHandler CancelEvent;
        public event EventHandler DeleteEvent;

        public void SetCustomerInfo(long customerId, string customerFullname)
        {
            txtCustomerId.Text = customerId.ToString();
            txtCustomerFullname.Text = customerFullname;
        }

        public void SetMembershipInfo(long membershipId, string statusDesc, decimal remainder)
        {
            txtMembershipId.Text = membershipId.ToString();
            txtStatusDesc.Text = statusDesc;
            txtRemainder.Text = remainder.ToString("N2");
        }

        public void SetMembershipDetails(
            string membershipType,
            string serviceCode,
            DateTime startDate,
            DateTime endDate,
            string comment)
        {
            cmbMembershipType.SelectedValue = membershipType;
            cmbService.SelectedValue = serviceCode;
            txtStartDate.Text = startDate.ToString("dd/MM/yyyy");
            txtEndDate.Text = endDate.ToString("dd/MM/yyyy");
            txtComments.Text = comment;
        }

        public void SetMembershipTypeListBindingSource(BindingSource source)
        {
            _membershipTypeBindingSource = source;
            cmbMembershipType.DataSource = null;
            cmbMembershipType.DisplayMember = "Description";
            cmbMembershipType.ValueMember = "MembershipType";
            cmbMembershipType.DataSource = _membershipTypeBindingSource;
        }

        public void SetServiceListBindingSource(BindingSource source)
        {
            _serviceBindingSource = source;
            cmbService.DataSource = null;
            cmbService.DisplayMember = "ServiceDescription";
            cmbService.ValueMember = "ServiceCode";
            cmbService.DataSource = _serviceBindingSource;
        }

        public void SetPaymentMethodListBindingSource(BindingSource source)
        {
            _paymentMethodBindingSource = source;
            cmbPaymentMethod.DataSource = null;
            cmbPaymentMethod.DisplayMember = "MethodDescription";
            cmbPaymentMethod.ValueMember = "MethodType";
            cmbPaymentMethod.DataSource = _paymentMethodBindingSource;
            
            DataGridViewComboBoxColumn methodColumn = dgvPayments.Columns["MethodType"] as DataGridViewComboBoxColumn;
            if (methodColumn != null)
            {
                methodColumn.DataSource = source;
                methodColumn.DisplayMember = "MethodDescription";
                methodColumn.ValueMember = "MethodType";
            }
        }

        public void SetPaymentsListBindingSource(BindingSource source)
        {
            _paymentsBindingSource = source;
            dgvPayments.DataSource = _paymentsBindingSource;
        }

        public void SetPaymentAmount(decimal amount)
        {
            txtPaymentAmount.Text = amount <= 0 ? string.Empty : amount.ToString("N2");
        }

        public void SetReadOnlyMode(bool isReadOnly)
        {
            // Στοιχεία συνδρομής
            cmbMembershipType.Enabled = false; // πάντα κλειδωμένο
            cmbService.Enabled = !isReadOnly;

            txtCustomerId.ReadOnly = true;
            txtCustomerFullname.ReadOnly = true;
            txtMembershipId.ReadOnly = true;
            txtStatusDesc.ReadOnly = true;
            txtRemainder.ReadOnly = true;
            txtStartDate.ReadOnly = true;
            txtEndDate.ReadOnly = true;

            // Παρατηρήσεις συνδρομής
            txtComments.ReadOnly = isReadOnly;

            // Νέα πληρωμή
            txtPaymentAmount.ReadOnly = isReadOnly;
            cmbPaymentMethod.Enabled = !isReadOnly;

            // Grid πληρωμών
            dgvPayments.Enabled = !isReadOnly;
            dgvPayments.ReadOnly = isReadOnly;

            // Κρατάμε πάντα readonly τη στήλη Id
            if (dgvPayments.Columns["Id"] != null)
                dgvPayments.Columns["Id"].ReadOnly = true;

            if (dgvPayments.Columns["MembershipId"] != null)
                dgvPayments.Columns["MembershipId"].ReadOnly = true;

            // Κουμπιά
            btnSave.Enabled = !isReadOnly;
            btnPayment.Enabled = !isReadOnly;
            btnDelete.Enabled = !isReadOnly;

            // Το btnCancel μένει πάντα ενεργό
            btnCancel.Enabled = true;
        }

        public long SelectedPaymentId
        {
            get
            {
                if (dgvPayments.CurrentRow == null)
                    return 0;

                object value = dgvPayments.CurrentRow.Cells["Id"].Value;
                if (value == null || value == DBNull.Value)
                    return 0;

                return Convert.ToInt64(value);
            }
        }

        public void RefreshPaymentsGrid()
        {
            dgvPayments.Refresh();
        }

        public void ShowMessage(string message, string title = "Πληροφορία")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult ShowQuestion(string message, string title = "Επιβεβαίωση")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void CloseView()
        {
            this.Close();
        }

        private void AssociateAndRaiseViewEvents()
        {
            this.Load += delegate
            {
                if (LoadEvent != null)
                    LoadEvent(this, EventArgs.Empty);
            };

            btnSave.Click += delegate
            {
                if (SaveEvent != null)
                    SaveEvent(this, EventArgs.Empty);
            };

            btnPayment.Click += delegate
            {
                if (PaymentEvent != null)
                    PaymentEvent(this, EventArgs.Empty);
            };

            btnCancel.Click += delegate
            {
                if (CancelEvent != null)
                    CancelEvent(this, EventArgs.Empty);
            };

            btnDelete.Click += delegate
            {
                if (DeleteEvent != null)
                    DeleteEvent(this, EventArgs.Empty);
            };
        }

        private void ConfigureControls()
        {
            txtCustomerId.ReadOnly = true;
            txtCustomerFullname.ReadOnly = true;
            txtMembershipId.ReadOnly = true;
            txtStatusDesc.ReadOnly = true;
            txtRemainder.ReadOnly = true;
            txtStartDate.ReadOnly = true;
            txtEndDate.ReadOnly = true;

            cmbMembershipType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMembershipType.Enabled = false;

            cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ConfigurePaymentsGrid()
        {
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.Columns.Clear();

            dgvPayments.ReadOnly = false;
            dgvPayments.MultiSelect = false;
            dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Αριθμ. Πληρωμής",
                Width = 120,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaymentDate",
                DataPropertyName = "PaymentDate",
                HeaderText = "Ημ. Πληρωμής",
                Width = 110,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaymentTimeDisplay",
                DataPropertyName = "PaymentTimeDisplay",
                HeaderText = "Ώρα Πληρωμής",
                Width = 100
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                DataPropertyName = "Amount",
                HeaderText = "Ποσό Πληρωμής",
                Width = 110,
                DefaultCellStyle = { Format = "N2" }
            });

            DataGridViewComboBoxColumn methodColumn = new DataGridViewComboBoxColumn();
            methodColumn.Name = "MethodType";
            methodColumn.DataPropertyName = "MethodType";
            methodColumn.HeaderText = "Τρόπος Πληρωμής";
            methodColumn.DisplayMember = "MethodDescription";
            methodColumn.ValueMember = "MethodType";
            methodColumn.Width = 160;
            dgvPayments.Columns.Add(methodColumn);

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MembershipId",
                DataPropertyName = "MembershipId",
                Visible = false
            });
        }
    }
}