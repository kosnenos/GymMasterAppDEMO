using System;
using System.Windows.Forms;

namespace GymMasterAppDemo.Views
{
    public interface IMainView
    {
        event EventHandler BackupDatabaseRequested;

        bool ConfirmQuestion(string message, string title);
        void ShowErrorMessage(string message, string title);

        IWin32Window OwnerWindow { get; }
    }
}