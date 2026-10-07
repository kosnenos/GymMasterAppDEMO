using System;
using System.IO;
using GymMasterAppDemo.Forms;
using GymMasterAppDemo.Services;
using GymMasterAppDemo.Views;
using GymMasterAppDemo.Data;

namespace GymMasterAppDemo.Presenters
{
    public class MainFormPresenter
    {
        private readonly IMainView _view;

        public MainFormPresenter(IMainView view)
        {
            _view = view;

            _view.BackupDatabaseRequested += OnBackupDatabaseRequested;
        }

        private void OnBackupDatabaseRequested(object sender, EventArgs e)
        {
            bool confirmed = _view.ConfirmQuestion(
                "Θέλετε να δημιουργηθεί αντίγραφο ασφαλείας της βάσης δεδομένων;",
                "Backup Βάσης Δεδομένων");

            if (!confirmed)
                return;

            try
            {
                string backupFolder = @"C:\GymMasterAppDemo_Backups";

                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                string fileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}.bak", DbConfig.DatabaseName, DateTime.Now);
                string fullPath = Path.Combine(backupFolder, fileName);

                IBackupView backupView = new BackupForm();

                string connectionString = DbConfig.ConnectionString;

                DatabaseBackupService backupService =
                    new DatabaseBackupService(connectionString, DbConfig.DatabaseName);

                BackupPresenter backupPresenter =
                    new BackupPresenter(backupView, backupService, fullPath);

                backupView.ShowDialogView(_view.OwnerWindow);
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage(
                    "Δεν ήταν δυνατή η προετοιμασία της διαδικασίας backup." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Σφάλμα");
            }
        }
    }
}