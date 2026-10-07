using System;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Forms
{
    public partial class NewMembershipForm : Form, INewMembershipView
    {
        private readonly string _connectionString;

        public NewMembershipForm(string connectionString, long customerId, string customerFullname)
        {
            InitializeComponent();

            _connectionString = connectionString;

            CustomerId = customerId;
            CustomerFullname = customerFullname;

            AssociateAndRaiseViewEvents();
            ConfigureControls();

            IMembershipRepository membershipRepository = new MembershipRepository(_connectionString);
            ILookupRepository lookupRepository = new LookupRepository(_connectionString);
            MembershipPaymentService membershipPaymentService = new MembershipPaymentService(_connectionString);

            new NewMembershipPresenter(this, membershipRepository, lookupRepository, membershipPaymentService);
        }

        public long CustomerId
        {
            get
            {
                long value;
                return long.TryParse(txtCustomerId.Text, out value) ? value : 0;
            }
            set
            {
                txtCustomerId.Text = value.ToString();
            }
        }

        public string CustomerFullname
        {
            get { return txtCustomerFullname.Text.Trim(); }
            set { txtCustomerFullname.Text = value; }
        }

        public string SelectedMembershipType
        {
            get
            {
                return cmbMembershipType.SelectedValue == null ? null : cmbMembershipType.SelectedValue.ToString();
            }
        }

        public string SelectedServiceCode
        {
            get
            {
                return cmbService.SelectedValue == null ? null : cmbService.SelectedValue.ToString();
            }
        }

        public string SelectedPaymentMethod
        {
            get
            {
                return cmbPaymentMethod.SelectedValue == null ? null : cmbPaymentMethod.SelectedValue.ToString();
            }
        }

        public DateTime StartDate
        {
            get { return dtpStartDate.Value.Date; }
        }

        public decimal Price
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtPrice.Text, out value) ? value : 0;
            }
        }

        public decimal FirstPaymentAmount
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtFirstPaymentAmount.Text, out value) ? value : 0;
            }
        }

        public string Comment
        {
            get { return txtComment.Text.Trim(); }
        }

        public event EventHandler LoadEvent;
        public event EventHandler MembershipTypeChangedEvent;
        public event EventHandler StartDateChangedEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetMembershipTypeListBindingSource(BindingSource source)
        {
            cmbMembershipType.DataSource = null;
            cmbMembershipType.DisplayMember = "Description";
            cmbMembershipType.ValueMember = "MembershipType";
            cmbMembershipType.DataSource = source;
        }

        public void SetServiceListBindingSource(BindingSource source)
        {
            cmbService.DataSource = null;
            cmbService.DisplayMember = "ServiceDescription";
            cmbService.ValueMember = "ServiceCode";
            cmbService.DataSource = source;
        }

        public void SetPaymentMethodListBindingSource(BindingSource source)
        {
            cmbPaymentMethod.DataSource = null;
            cmbPaymentMethod.DisplayMember = "MethodDescription";
            cmbPaymentMethod.ValueMember = "MethodType";
            cmbPaymentMethod.DataSource = source;
        }

        public void SetDuration(int duration)
        {
            txtDuration.Text = duration.ToString();
        }

        public void SetEndDate(string endDateText)
        {
            txtEndDate.Text = endDateText;
        }

        public void ShowMessage(string message, string title = "Πληροφορία")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            cmbMembershipType.SelectedIndexChanged += delegate
            {
                if (MembershipTypeChangedEvent != null)
                    MembershipTypeChangedEvent(this, EventArgs.Empty);
            };

            dtpStartDate.ValueChanged += delegate
            {
                if (StartDateChangedEvent != null)
                    StartDateChangedEvent(this, EventArgs.Empty);
            };

            btnSave.Click += delegate
            {
                if (SaveEvent != null)
                    SaveEvent(this, EventArgs.Empty);
            };

            btnCancel.Click += delegate
            {
                if (CancelEvent != null)
                    CancelEvent(this, EventArgs.Empty);
            };
        }

        private void ConfigureControls()
        {
            txtCustomerId.ReadOnly = true;
            txtCustomerFullname.ReadOnly = true;
            txtDuration.ReadOnly = true;
            txtEndDate.ReadOnly = true;

            cmbMembershipType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpStartDate.Value = DateTime.Today;
            txtDuration.Text = string.Empty;
            txtEndDate.Text = string.Empty;
        }
    }
}