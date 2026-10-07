using System;
using System.Windows.Forms;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Forms
{
    public partial class BackupForm : Form, IBackupView
    {
        private bool _isBusy;

        public event EventHandler CreateBackupRequested;
        public event EventHandler CancelRequested;

        public BackupForm()
        {
            InitializeComponent();
            ConfigureForm();
            WireUpEvents();
        }

        public string BackupTargetPath
        {
            set
            {
                RunOnUiThread(delegate
                {
                    txtDestinationBackup.Text = value;
                });
            }
        }

        public void SetProgress(int percent)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;

            RunOnUiThread(delegate
            {
                pgbProgressBar.Minimum = 0;
                pgbProgressBar.Maximum = 100;
                pgbProgressBar.Value = percent;
                lblProgress.Text = "Πρόοδος: " + percent + "%";
            });
        }

        public void SetBusyState(bool isBusy)
        {
            _isBusy = isBusy;

            RunOnUiThread(delegate
            {
                btnCreateBackup.Enabled = !isBusy;
                btnCancel.Enabled = !isBusy;
                ControlBox = !isBusy;
                UseWaitCursor = isBusy;
            });
        }

        public void ShowSuccessMessage(string message)
        {
            RunOnUiThread(delegate
            {
                MessageBox.Show(
                    this,
                    message,
                    "Backup Βάσης Δεδομένων",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            });
        }

        public void ShowErrorMessage(string message)
        {
            RunOnUiThread(delegate
            {
                MessageBox.Show(
                    this,
                    message,
                    "Σφάλμα Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            });
        }

        public void CloseView()
        {
            RunOnUiThread(delegate
            {
                Close();
            });
        }

        public void ShowDialogView(IWin32Window owner)
        {
            ShowDialog(owner);
        }

        private void ConfigureForm()
        {
            StartPosition = FormStartPosition.CenterParent;
            Text = "Δημιουργία Αντιγράφου Ασφαλείας";

            txtDestinationBackup.ReadOnly = true;
            pgbProgressBar.Minimum = 0;
            pgbProgressBar.Maximum = 100;
            pgbProgressBar.Value = 0;
            lblProgress.Text = "Πρόοδος: 0%";
        }

        private void WireUpEvents()
        {
            btnCreateBackup.Click += delegate
            {
                if (CreateBackupRequested != null)
                    CreateBackupRequested(this, EventArgs.Empty);
            };

            btnCancel.Click += delegate
            {
                if (CancelRequested != null)
                    CancelRequested(this, EventArgs.Empty);
            };
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isBusy && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }

        private void RunOnUiThread(MethodInvoker action)
        {
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action();
        }
    }
}