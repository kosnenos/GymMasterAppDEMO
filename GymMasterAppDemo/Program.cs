using GymMasterAppDemo.Forms;
using GymMasterAppDemo.Presenters;
using System;
using System.Windows.Forms;

namespace GymMasterAppDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            MainFormPresenter presenter = new MainFormPresenter(mainForm);

            Application.Run(mainForm);
        }
    }
}