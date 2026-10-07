using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Forms;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Presenters;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();

            this.Shown += MainForm_Shown;
        }

        public event EventHandler BackupDatabaseRequested;

        public IWin32Window OwnerWindow
        {
            get { return this; }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            FillStatusStrip();
            SetActiveMenuButton(btnHome);
            OpenChildForm(new DashboardForm(OpenChildForm));
        }

        private void ResetMenuButtonStyles()
        {
            Button[] menuButtons =
            {
                btnHome,
                btnCustomers,
                btnMemberships,
                btnAnalytics,
                btnWorksOuts
            };

            foreach (Button btn in menuButtons)
            {
                btn.BackColor = Color.LightSkyBlue;
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
                btn.FlatAppearance.BorderSize = 0;
            }
        }

        private void SetActiveMenuButton(Button activeButton)
        {
            ResetMenuButtonStyles();

            activeButton.BackColor = Color.SteelBlue;
            activeButton.ForeColor = Color.White;
            activeButton.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Regular);
            activeButton.FlatAppearance.BorderSize = 0;
        }

        private void OpenChildForm(Form childForm)
        {
            pnlContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;

            childForm.Show();
        }

        private void FillStatusStrip()
        {
            tslFormName.Text = "GymMaster";
            tslManager.Text = "Υπεύθυνος Γυμναστηρίου: Demo Manager";
            tslCopyright.Text = BuildProjectCopyright();

            try
            {
                GymMasterAppDemo.Data.DbDiagnostics.TestConnection();
                tslDb.Text = "DB: OK";
                tslDb.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                tslDb.Text = "DB: ERROR";
                tslDb.ForeColor = Color.DarkRed;
                tslDb.ToolTipText = ex.Message;
            }
        }

        private static string BuildProjectCopyright()
        {
            var asm = Assembly.GetExecutingAssembly();
            var attr = asm.GetCustomAttribute<AssemblyCopyrightAttribute>();

            string copy = (attr != null && !string.IsNullOrWhiteSpace(attr.Copyright))
                ? attr.Copyright
                : $"© {DateTime.Now.Year}";

            return $"{Application.ProductName} {copy}";
        }

        private bool _exitConfirmed = false;

        private bool ConfirmExit()
        {
            return MessageBox.Show(
                "Θέλετε σίγουρα να τερματίσετε την εφαρμογή;",
                "GymMaster - Τερματισμός",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            ) == DialogResult.Yes;
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            if (ConfirmExit())
            {
                _exitConfirmed = true;
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (_exitConfirmed) return;

            if (!ConfirmExit())
                e.Cancel = true;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnCustomers);
            ICustomerView view = new CustomerForm();
            ICustomerRepository repository = new CustomerRepository(DbConfig.ConnectionString);
            CustomerPresenter presenter = new CustomerPresenter(view, repository);

            Form customerForm = (Form)view;

            CustomerForm concreteForm = customerForm as CustomerForm;
            if (concreteForm != null)
            {
                concreteForm.ReturnToHomeRequested += (s, args) =>
                {
                    OpenChildForm(new DashboardForm(OpenChildForm));
                };
            }

            presenter.LoadCustomers();
            OpenChildForm(customerForm);
        }

        private void btnMemberships_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnMemberships);
            IMembershipView view = new MembershipForm(DbConfig.ConnectionString);

            ICustomerRepository customerRepository =
                new CustomerRepository(DbConfig.ConnectionString);

            IMembershipRepository membershipRepository =
                new MembershipRepository(DbConfig.ConnectionString);

            MembershipPresenter presenter = new MembershipPresenter(
                view,
                customerRepository,
                membershipRepository);

            Form membershipForm = (Form)view;

            MembershipForm concreteForm = membershipForm as MembershipForm;
            if (concreteForm != null)
            {
                concreteForm.ReturnToHomeRequested += delegate
                {
                    OpenChildForm(new DashboardForm(OpenChildForm));
                };
            }

            OpenChildForm(membershipForm);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnHome);
            OpenChildForm(new DashboardForm(OpenChildForm));
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnAnalytics);
            AnalyticsForm analyticsForm = new AnalyticsForm(AnalyticsTab.RecentCustomers);

            analyticsForm.ReturnToHomeRequested += (s, args) =>
            {
                OpenChildForm(new DashboardForm(OpenChildForm));
            };

            OpenChildForm(analyticsForm);
        }

        private void btnWorksOuts_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnWorksOuts);
            IWorkoutProgramView view = new WorkoutProgramForm(DbConfig.ConnectionString);

            ICustomerRepository customerRepository =
                new CustomerRepository(DbConfig.ConnectionString);

            IWorkoutProgramRepository workoutProgramRepository =
                new WorkoutProgramRepository(DbConfig.ConnectionString);

            WorkoutProgramPresenter presenter = new WorkoutProgramPresenter(
                view,
                customerRepository,
                workoutProgramRepository);

            Form workoutProgramForm = (Form)view;

            WorkoutProgramForm concreteForm = workoutProgramForm as WorkoutProgramForm;
            if (concreteForm != null)
            {
                concreteForm.ReturnToHomeRequested += delegate
                {
                    OpenChildForm(new DashboardForm(OpenChildForm));
                };
            }

            OpenChildForm(workoutProgramForm);
        }

        public bool ConfirmQuestion(string message, string title)
        {
            return MessageBox.Show(
                this,
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(
                this,
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void btnBackupDBAll_Click(object sender, EventArgs e)
        {
            if (BackupDatabaseRequested != null)
                BackupDatabaseRequested(this, EventArgs.Empty);
        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {
            AboutBoxForm aboutform = new AboutBoxForm();
            aboutform.ShowDialog(); 
        }
    }
}
