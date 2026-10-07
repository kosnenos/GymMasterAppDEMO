using System;
using System.IO;
using System.Threading.Tasks;
using GymMasterAppDemo.Services;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class BackupPresenter
    {
        private readonly IBackupView _view;
        private readonly DatabaseBackupService _backupService;
        private readonly string _backupFullPath;

        private bool _isRunning;

        public BackupPresenter(IBackupView view, DatabaseBackupService backupService, string backupFullPath)
        {
            _view = view;
            _backupService = backupService;
            _backupFullPath = backupFullPath;

            _view.CreateBackupRequested += OnCreateBackupRequested;
            _view.CancelRequested += OnCancelRequested;

            _view.BackupTargetPath = _backupFullPath;
            _view.SetProgress(0);
            _view.SetBusyState(false);
        }

        private void OnCancelRequested(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _view.CloseView();
        }

        private async void OnCreateBackupRequested(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;
            _view.SetBusyState(true);
            _view.SetProgress(0);

            try
            {
                string createdFile = await Task.Run(delegate
                {
                    return _backupService.CreateBackup(_backupFullPath, UpdateProgress);
                });

                _view.SetProgress(100);
                _view.SetBusyState(false);
                string successMessage =
                    "Το αντίγραφο ασφαλείας δημιουργήθηκε επιτυχώς." +
                    Environment.NewLine + Environment.NewLine +
                    "Τίτλος αρχείου: " + Path.GetFileName(createdFile) + Environment.NewLine +
                    "Τοποθεσία: " + createdFile;

                _view.ShowSuccessMessage(successMessage);
                _view.CloseView();
            }
            catch (Exception ex)
            {
                _isRunning = false;
                _view.SetBusyState(false);

                _view.ShowErrorMessage(
                    "Αποτυχία δημιουργίας αντιγράφου ασφαλείας." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message);
            }
        }

        private void UpdateProgress(int percent)
        {
            _view.SetProgress(percent);
        }
    }
}