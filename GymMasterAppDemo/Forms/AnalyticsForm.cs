using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Presenters;

namespace GymMasterAppDemo.Views
{
    public partial class AnalyticsForm : Form, IAnalyticsView
    {
        private readonly AnalyticsTab _initialTab;
        private readonly AnalyticsPresenter _presenter;

        public event EventHandler ViewLoaded;
        public event EventHandler RefreshClicked;
        public event EventHandler ExportExcelClicked;
        public event EventHandler CloseClicked;
        public event EventHandler ReturnToHomeRequested;

        public int SelectedTabIndex => tabAnalytics.SelectedIndex;

        public AnalyticsForm() : this(AnalyticsTab.RecentCustomers)
        {
        }

        public AnalyticsForm(AnalyticsTab initialTab)
        {
            InitializeComponent();

            _initialTab = initialTab;

            ConfigureUi();

            _presenter = new AnalyticsPresenter(this, new AnalyticsRepository());
        }

        private void AnalyticsForm_Load(object sender, EventArgs e)
        {
            if (tabAnalytics.TabPages.Count > 0)
            {
                var index = (int)_initialTab;
                if (index < 0 || index >= tabAnalytics.TabPages.Count)
                {
                    index = 0;
                }

                tabAnalytics.SelectedIndex = index;
            }

            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigureUi()
        {
            SetupGrid(dgvRecentCustomers);
            SetupGrid(dgvExpiringToday);
            SetupGrid(dgvUnpaidMemberships);

            lblNumNewCustomers.Text = "Σύνολο Εγγραφών: 0";
            lblExpiredToday.Text = "Σύνολο Συνδρομών: 0";
            lblNumOfUnpaid.Text = "Σύνολο Ανεξόφλητων: 0";
        }

        private void SetupGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AutoGenerateColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        public void BindRecentCustomers(List<RecentCustomerReportItem> items)
        {
            dgvRecentCustomers.DataSource = null;
            dgvRecentCustomers.DataSource = items;

            if (dgvRecentCustomers.Columns["CustomerId"] != null)
                dgvRecentCustomers.Columns["CustomerId"].HeaderText = "Κωδ. Πελάτη";

            if (dgvRecentCustomers.Columns["FullName"] != null)
                dgvRecentCustomers.Columns["FullName"].HeaderText = "Ονοματεπώνυμο";

            if (dgvRecentCustomers.Columns["MobilePhone"] != null)
                dgvRecentCustomers.Columns["MobilePhone"].HeaderText = "Κινητό Τηλέφωνο";

            if (dgvRecentCustomers.Columns["RegistrationDate"] != null)
            {
                dgvRecentCustomers.Columns["RegistrationDate"].HeaderText = "Ημ. Εγγραφής";
                dgvRecentCustomers.Columns["RegistrationDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            dgvRecentCustomers.ClearSelection();
        }

        public void BindExpiringToday(List<ExpiringTodayReportItem> items)
        {
            dgvExpiringToday.DataSource = null;
            dgvExpiringToday.DataSource = items;

            if (dgvExpiringToday.Columns["MembershipId"] != null)
                dgvExpiringToday.Columns["MembershipId"].HeaderText = "Κωδ. Συνδρομής";

            if (dgvExpiringToday.Columns["CustomerId"] != null)
                dgvExpiringToday.Columns["CustomerId"].HeaderText = "Κωδ. Πελάτη";

            if (dgvExpiringToday.Columns["CustomerName"] != null)
                dgvExpiringToday.Columns["CustomerName"].HeaderText = "Ον/μο Πελάτη";

            if (dgvExpiringToday.Columns["MembershipType"] != null)
                dgvExpiringToday.Columns["MembershipType"].HeaderText = "Τύπος Συνδρομής";

            if (dgvExpiringToday.Columns["StartDate"] != null)
            {
                dgvExpiringToday.Columns["StartDate"].HeaderText = "Ημ. Έναρξης";
                dgvExpiringToday.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvExpiringToday.Columns["Price"] != null)
            {
                dgvExpiringToday.Columns["Price"].HeaderText = "Κόστος (€)";
                dgvExpiringToday.Columns["Price"].DefaultCellStyle.Format = "N2";
            }

            if (dgvExpiringToday.Columns["Status"] != null)
                dgvExpiringToday.Columns["Status"].HeaderText = "Κατάσταση";

            dgvExpiringToday.ClearSelection();
        }

        public void BindUnpaidMemberships(List<UnpaidMembershipReportItem> items)
        {
            dgvUnpaidMemberships.DataSource = null;
            dgvUnpaidMemberships.DataSource = items;

            if (dgvUnpaidMemberships.Columns["MembershipId"] != null)
                dgvUnpaidMemberships.Columns["MembershipId"].HeaderText = "Κωδ. Συνδρομής";

            if (dgvUnpaidMemberships.Columns["CustomerId"] != null)
                dgvUnpaidMemberships.Columns["CustomerId"].HeaderText = "Κωδ. Πελάτη";

            if (dgvUnpaidMemberships.Columns["CustomerName"] != null)
                dgvUnpaidMemberships.Columns["CustomerName"].HeaderText = "Ον/μο Πελάτη";

            if (dgvUnpaidMemberships.Columns["MembershipType"] != null)
                dgvUnpaidMemberships.Columns["MembershipType"].HeaderText = "Τύπος Συνδρομής";

            if (dgvUnpaidMemberships.Columns["EndDate"] != null)
            {
                dgvUnpaidMemberships.Columns["EndDate"].HeaderText = "Ημ. Λήξης";
                dgvUnpaidMemberships.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvUnpaidMemberships.Columns["Price"] != null)
            {
                dgvUnpaidMemberships.Columns["Price"].HeaderText = "Κόστος (€)";
                dgvUnpaidMemberships.Columns["Price"].DefaultCellStyle.Format = "N2";
            }

            if (dgvUnpaidMemberships.Columns["Balance"] != null)
            {
                dgvUnpaidMemberships.Columns["Balance"].HeaderText = "Υπόλοιπο (€)";
                dgvUnpaidMemberships.Columns["Balance"].DefaultCellStyle.Format = "N2";
            }

            if (dgvUnpaidMemberships.Columns["Status"] != null)
                dgvUnpaidMemberships.Columns["Status"].HeaderText = "Κατάσταση";

            dgvUnpaidMemberships.ClearSelection();
        }

        public void SetRecentCustomersCount(int count)
        {
            lblNumNewCustomers.Text = $"Σύνολο Εγγραφών: {count}";
        }

        public void SetExpiringTodayCount(int count)
        {
            lblExpiredToday.Text = $"Σύνολο Συνδρομών: {count}";
        }

        public void SetUnpaidCount(int count)
        {
            lblNumOfUnpaid.Text = $"Σύνολο Ανεξόφλητων: {count}";
        }

        public void ExportCurrentTabToExcel()
        {
            DataGridView activeGrid = GetActiveGrid();
            string activeTabTitle = GetActiveTabTitle();

            if (activeGrid == null)
            {
                ShowErrorMessage("Δεν βρέθηκε το ενεργό grid για εξαγωγή.");
                return;
            }

            var visibleColumns = activeGrid.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            if (visibleColumns.Count == 0)
            {
                ShowErrorMessage("Δεν υπάρχουν στήλες για εξαγωγή.");
                return;
            }

            string downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads");

            if (!Directory.Exists(downloadsPath))
            {
                Directory.CreateDirectory(downloadsPath);
            }

            string timeStamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string safeFileName = SanitizeFileName($"{activeTabTitle}_{timeStamp}.xlsx");
            string fullPath = Path.Combine(downloadsPath, safeFileName);

            using (var workbook = new XLWorkbook())
            {
                string worksheetName = SanitizeWorksheetName(activeTabTitle);
                var worksheet = workbook.Worksheets.Add(worksheetName);

                for (int col = 0; col < visibleColumns.Count; col++)
                {
                    worksheet.Cell(1, col + 1).Value = visibleColumns[col].HeaderText;
                    worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                    worksheet.Cell(1, col + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                int excelRow = 2;

                foreach (DataGridViewRow gridRow in activeGrid.Rows)
                {
                    if (gridRow.IsNewRow)
                        continue;

                    for (int col = 0; col < visibleColumns.Count; col++)
                    {
                        var gridColumn = visibleColumns[col];
                        object formattedValue = gridRow.Cells[gridColumn.Name].FormattedValue;
                        worksheet.Cell(excelRow, col + 1).Value = formattedValue?.ToString() ?? string.Empty;
                    }

                    excelRow++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(fullPath);
            }

            ShowInfoMessage(
                $"Η εξαγωγή ολοκληρώθηκε επιτυχώς.\n\nΑρχείο:\n{Path.GetFileName(fullPath)}\n\nΤοποθεσία:\n{downloadsPath}");
        }

        private DataGridView GetActiveGrid()
        {
            switch (tabAnalytics.SelectedIndex)
            {
                case 0:
                    return dgvRecentCustomers;
                case 1:
                    return dgvExpiringToday;
                case 2:
                    return dgvUnpaidMemberships;
                default:
                    return null;
            }
        }

        private string GetActiveTabTitle()
        {
            if (tabAnalytics.SelectedTab == null)
                return "Αναφορά";

            string title = tabAnalytics.SelectedTab.Text?.Trim();

            return string.IsNullOrWhiteSpace(title) ? "Αναφορά" : title;
        }

        private string SanitizeFileName(string fileName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName;
        }

        private string SanitizeWorksheetName(string worksheetName)
        {
            char[] invalidChars = { ':', '\\', '/', '?', '*', '[', ']' };

            foreach (char invalidChar in invalidChars)
            {
                worksheetName = worksheetName.Replace(invalidChar, '_');
            }

            worksheetName = worksheetName.Trim();

            if (string.IsNullOrWhiteSpace(worksheetName))
                worksheetName = "Αναφορά";

            if (worksheetName.Length > 31)
                worksheetName = worksheetName.Substring(0, 31);

            return worksheetName;
        }

        public void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Ενημέρωση", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseView()
        {
            ReturnToHomeRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}