using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GymMasterAppDemo.Views;


namespace GymMasterAppDemo.Forms
{
    public partial class CustomerEditForm : Form, ICustomerEditView
    {
        private class YesNoOption
        {
            public bool? Value { get; set; }
            public string Text { get; set; }
        }
        public CustomerEditForm()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            ConfigureControls();
        }

        public string CustomerIdText
        {
            get => txtCustomerId.Text.Trim();
            set => txtCustomerId.Text = value;
        }

        public string LastName
        {
            get => txtLastName.Text.Trim();
            set => txtLastName.Text = value;
        }

        public string FirstName
        {
            get => txtFirstName.Text.Trim();
            set => txtFirstName.Text = value;
        }

        public string FatherName
        {
            get => txtFatherName.Text.Trim();
            set => txtFatherName.Text = value;
        }

        public string Gender
        {
            get => cmbGender.SelectedValue?.ToString();
            set => cmbGender.SelectedValue = value;
        }

        public string OccupationId
        {
            get => cmbOccupation.SelectedValue?.ToString();
            set => cmbOccupation.SelectedValue = value;
        }

        public DateTime? Birthday
        {
            get => dtpBirthday.Checked ? dtpBirthday.Value.Date : (DateTime?)null;
            set
            {
                if (value.HasValue)
                {
                    dtpBirthday.Checked = true;
                    dtpBirthday.Value = value.Value;
                }
                else
                {
                    dtpBirthday.Checked = false;
                }
            }
        }

        public string Address
        {
            get => txtAddress.Text.Trim();
            set => txtAddress.Text = value;
        }

        public string City
        {
            get => txtCity.Text.Trim();
            set => txtCity.Text = value;
        }

        public string Mobile
        {
            get => txtMobile.Text.Trim();
            set => txtMobile.Text = value;
        }

        public string Home
        {
            get => txtHome.Text.Trim();
            set => txtHome.Text = value;
        }

        public string Email
        {
            get => txtEmail.Text.Trim();
            set => txtEmail.Text = value;
        }

        public DateTime CreationDate
        {
            get => dtpCreationDate.Value.Date;
            set => dtpCreationDate.Value = value;
        }

        public string Comments
        {
            get => txtComments.Text.Trim();
            set => txtComments.Text = value;
        }

        public string FormTitle
        {
            set => this.Text = value;
        }

        public string SaveButtonText
        {
            set => btnSave.Text = value;
        }

        // Health properties
        public bool? HasBodyPain
        {
            get => GetComboBoolValue(cmbHasBodyPain);
            set => SetComboBoolValue(cmbHasBodyPain, value);
        }

        public string BodyPainDesc
        {
            get => txtBodyPainDesc.Text.Trim();
            set => txtBodyPainDesc.Text = value;
        }

        public bool? IsObest
        {
            get => GetComboBoolValue(cmbIsObest);
            set => SetComboBoolValue(cmbIsObest, value);
        }

        public bool? IsSmoker
        {
            get => GetComboBoolValue(cmbIsSmoker);
            set => SetComboBoolValue(cmbIsSmoker, value);
        }

        public bool? FamilyHeartHistory
        {
            get => GetComboBoolValue(cmbFamilyHeartHistory);
            set => SetComboBoolValue(cmbFamilyHeartHistory, value);
        }

        public bool? HasHypertasis
        {
            get => GetComboBoolValue(cmbHasHypertasis);
            set => SetComboBoolValue(cmbHasHypertasis, value);
        }

        public bool? HasDiabetes
        {
            get => GetComboBoolValue(cmbHasDiabetes);
            set => SetComboBoolValue(cmbHasDiabetes, value);
        }

        public bool? HasHeartIssues
        {
            get => GetComboBoolValue(cmbHasHeartIssues);
            set => SetComboBoolValue(cmbHasHeartIssues, value);
        }

        public bool? HasAsthma
        {
            get => GetComboBoolValue(cmbHasAsthma);
            set => SetComboBoolValue(cmbHasAsthma, value);
        }

        public bool? HasThyroedes
        {
            get => GetComboBoolValue(cmbHasThyroedes);
            set => SetComboBoolValue(cmbHasThyroedes, value);
        }

        public bool? HasArthritis
        {
            get => GetComboBoolValue(cmbHasArthritis);
            set => SetComboBoolValue(cmbHasArthritis, value);
        }

        public bool? HasOsteoporosis
        {
            get => GetComboBoolValue(cmbHasOsteoporosis);
            set => SetComboBoolValue(cmbHasOsteoporosis, value);
        }

        public bool? HasAllergies
        {
            get => GetComboBoolValue(cmbHasAllergies);
            set => SetComboBoolValue(cmbHasAllergies, value);
        }

        public bool? HasMyosceletic
        {
            get => GetComboBoolValue(cmbHasMyosceletic);
            set => SetComboBoolValue(cmbHasMyosceletic, value);
        }

        public string MyoskeleticDesc
        {
            get => txtMyoskeleticDesc.Text.Trim();
            set => txtMyoskeleticDesc.Text = value;
        }

        public bool? HasOther
        {
            get => GetComboBoolValue(cmbHasOther);
            set => SetComboBoolValue(cmbHasOther, value);
        }

        public string OtherDesc
        {
            get => txtOtherDesc.Text.Trim();
            set => txtOtherDesc.Text = value;
        }

        public string EmergencyContact
        {
            get => txtEmergencyContact.Text.Trim();
            set => txtEmergencyContact.Text = value;
        }

        public string EmergencyPhone
        {
            get => txtEmergencyPhone.Text.Trim();
            set => txtEmergencyPhone.Text = value;
        }

        public string HealthComment
        {
            get => txtHealthComment.Text.Trim();
            set => txtHealthComment.Text = value;
        }

        public event EventHandler LoadFormEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler CustomerIdLeaveEvent;

        private void ConfigureControls()
        {
            cmbGender.DataSource = new[]
            {
                new { Value = "", Text = "-- Επιλογή --" },
                new { Value = "Α", Text = "Άνδρας" },
                new { Value = "Γ", Text = "Γυναίκα" }
            };
            cmbGender.DisplayMember = "Text";
            cmbGender.ValueMember = "Value";

            BindYesNoCombo(cmbHasBodyPain);
            BindYesNoCombo(cmbIsObest);
            BindYesNoCombo(cmbIsSmoker);
            BindYesNoCombo(cmbFamilyHeartHistory);
            BindYesNoCombo(cmbHasHypertasis);
            BindYesNoCombo(cmbHasDiabetes);
            BindYesNoCombo(cmbHasHeartIssues);
            BindYesNoCombo(cmbHasAsthma);
            BindYesNoCombo(cmbHasThyroedes);
            BindYesNoCombo(cmbHasArthritis);
            BindYesNoCombo(cmbHasOsteoporosis);
            BindYesNoCombo(cmbHasAllergies);
            BindYesNoCombo(cmbHasMyosceletic);
            BindYesNoCombo(cmbHasOther);

            tabHealthRecord.Enabled = false;
            dtpBirthday.ShowCheckBox = true;

            UpdateHealthFieldStates();
        }
        private void AssociateAndRaiseViewEvents()
        {
            this.Load += (s, e) => LoadFormEvent?.Invoke(this, EventArgs.Empty);
            btnSave.Click += (s, e) => SaveEvent?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (s, e) => CancelEvent?.Invoke(this, EventArgs.Empty);
            txtCustomerId.Leave += (s, e) => CustomerIdLeaveEvent?.Invoke(this, EventArgs.Empty);

            txtCustomerId.KeyPress += NumericOnly_KeyPress;
            txtMobile.KeyPress += NumericOnly_KeyPress;
            txtHome.KeyPress += NumericOnly_KeyPress;
            txtEmergencyPhone.KeyPress += NumericOnly_KeyPress;

            cmbHasBodyPain.SelectedIndexChanged += (s, e) => UpdateHealthFieldStates();
            cmbHasMyosceletic.SelectedIndexChanged += (s, e) => UpdateHealthFieldStates();
            cmbHasOther.SelectedIndexChanged += (s, e) => UpdateHealthFieldStates();
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void BindYesNoCombo(ComboBox combo)
        {
            combo.DataSource = null;

            combo.DataSource = new List<YesNoOption>
    {
        new YesNoOption { Value = null, Text = "-- Επιλογή --" },
        new YesNoOption { Value = true, Text = "ΝΑΙ" },
        new YesNoOption { Value = false, Text = "ΟΧΙ" }
    };

            combo.DisplayMember = "Text";
            combo.ValueMember = "Value";
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.SelectedIndex = 0;
        }

        private bool? GetComboBoolValue(ComboBox combo)
        {
            if (combo.SelectedItem == null)
                return null;

            if (combo.SelectedItem is YesNoOption option)
                return option.Value;

            return null;
        }

        private void SetComboBoolValue(ComboBox combo, bool? value)
        {
            if (combo.Items.Count == 0)
                return;

            foreach (var item in combo.Items)
            {
                if (item is YesNoOption option)
                {
                    if (option.Value == value)
                    {
                        combo.SelectedItem = item;
                        return;
                    }
                }
            }

            combo.SelectedIndex = 0;
        }

        private void UpdateHealthFieldStates()
        {
            txtBodyPainDesc.Enabled = HasBodyPain == true;
            txtMyoskeleticDesc.Enabled = HasMyosceletic == true;
            txtOtherDesc.Enabled = HasOther == true;

            if (!txtBodyPainDesc.Enabled) txtBodyPainDesc.Text = string.Empty;
            if (!txtMyoskeleticDesc.Enabled) txtMyoskeleticDesc.Text = string.Empty;
            if (!txtOtherDesc.Enabled) txtOtherDesc.Text = string.Empty;
        }

        public void SetOccupationBindingSource(BindingSource source)
        {
            cmbOccupation.DataSource = null;
            cmbOccupation.DisplayMember = "OccupationDesc";
            cmbOccupation.ValueMember = "OccupationId";
            cmbOccupation.DataSource = source;
            cmbOccupation.SelectedIndex = -1;
        }

        public void SetCustomerIdReadOnly(bool readOnly)
        {
            txtCustomerId.ReadOnly = readOnly;
        }

        public void EnableHealthTab(bool enabled)
        {
            tabHealthRecord.Enabled = enabled;
        }

        public void SelectHealthTab()
        {
            tabMain.SelectedTab = tabHealthRecord;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Προειδοποίηση", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public DialogResult ShowQuestion(string message)
        {
            return MessageBox.Show(message, "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void CloseView()
        {
            this.Close();
        }

    }
}