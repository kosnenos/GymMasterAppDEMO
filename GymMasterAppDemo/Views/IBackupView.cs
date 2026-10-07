using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface IBackupView
    {
        event EventHandler CreateBackupRequested;
        event EventHandler CancelRequested;

        string BackupTargetPath { set; }

        void SetProgress(int percent);
        void SetBusyState(bool isBusy);

        void ShowSuccessMessage(string message);
        void ShowErrorMessage(string message);

        void CloseView();
        void ShowDialogView(IWin32Window owner);
    }
}