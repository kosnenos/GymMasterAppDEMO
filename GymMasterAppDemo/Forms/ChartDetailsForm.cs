using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GymMasterAppDemo.Forms
{
    public partial class ChartDetailsForm : Form
    {
        private readonly string _previewCode;
        private readonly string _previewTitle;
        private readonly DataTable _data;
        private readonly PrintDocument _printDocument;

        public ChartDetailsForm(string previewCode, string previewTitle, DataTable data)
        {
            InitializeComponent();

            _previewCode = previewCode;
            _previewTitle = previewTitle;
            _data = data ?? new DataTable();

            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Landscape = true;
            _printDocument.PrintPage += PrintDocument_PrintPage;

            Load += ChartDetailsForm_Load;
            btnClose.Click += btnClose_Click;
            btnPrint.Click += btnPrint_Click;
            btnExportJPG.Click += btnExportJPG_Click;
            btnExcelExport.Click += btnExcelExport_Click;
        }

        private void ChartDetailsForm_Load(object sender, EventArgs e)
        {
            Text = _previewTitle;
            lblTitle.Text = _previewTitle;

            ConfigureGrid();
            dgvChartData.DataSource = _data;

            ConfigurePreviewChart();
            LoadPreviewChartData();
        }

        private void ConfigureGrid()
        {
            dgvChartData.ReadOnly = true;
            dgvChartData.AllowUserToAddRows = false;
            dgvChartData.AllowUserToDeleteRows = false;
            dgvChartData.AllowUserToResizeRows = false;
            dgvChartData.MultiSelect = false;
            dgvChartData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChartData.RowHeadersVisible = false;
            dgvChartData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChartData.BackgroundColor = Color.White;
            dgvChartData.BorderStyle = BorderStyle.None;
            dgvChartData.EnableHeadersVisualStyles = false;

            dgvChartData.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvChartData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChartData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            dgvChartData.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvChartData.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvChartData.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void ConfigurePreviewChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();

            switch (_previewCode)
            {
                case "ANNUAL":
                    ConfigureAnnualChart();
                    break;

                case "REVENUE":
                    ConfigureRevenueChart();
                    break;

                case "MEMBERSHIP_TYPES":
                    ConfigureMembershipTypesChart();
                    break;

                case "GENDER":
                    ConfigureGenderChart();
                    break;

                case "SERVICES":
                    ConfigureServicesChart();
                    break;

                case "OCCUPATIONS":
                    ConfigureOccupationsChart();
                    break;
            }
        }

        private void LoadPreviewChartData()
        {
            switch (_previewCode)
            {
                case "ANNUAL":
                    foreach (DataRow row in _data.Rows)
                    {
                        chart1.Series["AnnualMemberships"].Points.AddXY(
                            row["Έτος"].ToString(),
                            Convert.ToInt32(row["Συνδρομές"]));
                    }
                    break;

                case "REVENUE":
                    foreach (DataRow row in _data.Rows)
                    {
                        chart1.Series["Τρέχον Έτος"].Points.AddXY(
                            row["Μήνας"].ToString(),
                            Convert.ToDecimal(row["Τρέχον Έτος"]));

                        chart1.Series["Προηγούμενο Έτος"].Points.AddXY(
                            row["Μήνας"].ToString(),
                            Convert.ToDecimal(row["Προηγούμενο Έτος"]));
                    }
                    break;

                case "MEMBERSHIP_TYPES":
                    foreach (DataRow row in _data.Rows)
                    {
                        string type = row["Τύπος Συνδρομής"].ToString();
                        int total = Convert.ToInt32(row["Πλήθος"]);

                        int pointIndex = chart1.Series["MembershipTypes"].Points.AddXY(type, total);

                        switch (type)
                        {
                            case "Ημερήσια":
                                chart1.Series["MembershipTypes"].Points[pointIndex].Color = Color.SteelBlue;
                                break;
                            case "Μηνιαία":
                                chart1.Series["MembershipTypes"].Points[pointIndex].Color = Color.MediumSeaGreen;
                                break;
                            case "Τρίμηνη":
                                chart1.Series["MembershipTypes"].Points[pointIndex].Color = Color.Goldenrod;
                                break;
                            case "Ετήσια":
                                chart1.Series["MembershipTypes"].Points[pointIndex].Color = Color.IndianRed;
                                break;
                            default:
                                chart1.Series["MembershipTypes"].Points[pointIndex].Color = Color.MediumPurple;
                                break;
                        }
                    }
                    break;

                case "GENDER":
                    foreach (DataRow row in _data.Rows)
                    {
                        chart1.Series["Gender"].Points.AddXY(
                            row["Φύλο"].ToString(),
                            Convert.ToInt32(row["Πλήθος"]));
                    }
                    break;

                case "SERVICES":
                    Color[] barColors = new Color[]
                    {
                        Color.SteelBlue,
                        Color.MediumSeaGreen,
                        Color.Goldenrod,
                        Color.IndianRed,
                        Color.MediumPurple,
                        Color.CadetBlue,
                        Color.DarkOrange,
                        Color.Teal
                    };

                    int colorIndex = 0;

                    foreach (DataRow row in _data.Rows)
                    {
                        int pointIndex = chart1.Series["Services"].Points.AddXY(
                            row["Υπηρεσία"].ToString(),
                            Convert.ToInt32(row["Πελάτες"]));

                        chart1.Series["Services"].Points[pointIndex].Color = barColors[colorIndex % barColors.Length];
                        colorIndex++;
                    }
                    break;

                case "OCCUPATIONS":
                    int grandTotal = 0;

                    foreach (DataRow row in _data.Rows)
                        grandTotal += Convert.ToInt32(row["Πελάτες"]);

                    foreach (DataRow row in _data.Rows)
                    {
                        string occupation = row["Επάγγελμα"].ToString();
                        int total = Convert.ToInt32(row["Πελάτες"]);

                        int pointIndex = chart1.Series["Occupations"].Points.AddXY(occupation, total);

                        double percent = grandTotal == 0 ? 0 : (total * 100.0 / grandTotal);

                        chart1.Series["Occupations"].Points[pointIndex].LegendText =
                            GetShortOccupationText(occupation) + ": " + total + " (" + percent.ToString("0") + "%)";
                    }
                    break;
            }
        }

        private void ConfigureAnnualChart()
        {
            ChartArea area = new ChartArea("AnnualMemberships");
            area.BackColor = Color.Transparent;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("AnnualMemberships") { Enabled = false };
            chart1.Legends.Add(legend);

            Series series = new Series("AnnualMemberships");
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "AnnualMemberships";
            series.Legend = "AnnualMemberships";
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;
            series.Color = Color.MediumPurple;
            series.IsValueShownAsLabel = true;

            chart1.Series.Add(series);
        }

        private void ConfigureRevenueChart()
        {
            ChartArea area = new ChartArea("RevenueArea");
            area.BackColor = Color.Transparent;
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.Minimum = 0;
            area.AxisY.IsStartedFromZero = true;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.Title = "Έσοδα (€)";
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("RevenueLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            chart1.Legends.Add(legend);

            Series currentYear = new Series("Τρέχον Έτος");
            currentYear.ChartType = SeriesChartType.Line;
            currentYear.ChartArea = "RevenueArea";
            currentYear.Legend = "RevenueLegend";
            currentYear.BorderWidth = 3;
            currentYear.Color = Color.Green;
            currentYear.MarkerStyle = MarkerStyle.Circle;
            currentYear.MarkerSize = 7;
            currentYear.IsValueShownAsLabel = true;
            currentYear.LabelFormat = "0.##";

            Series previousYear = new Series("Προηγούμενο Έτος");
            previousYear.ChartType = SeriesChartType.Line;
            previousYear.ChartArea = "RevenueArea";
            previousYear.Legend = "RevenueLegend";
            previousYear.BorderWidth = 2;
            previousYear.Color = Color.IndianRed;
            previousYear.MarkerStyle = MarkerStyle.Circle;
            previousYear.MarkerSize = 6;

            chart1.Series.Add(currentYear);
            chart1.Series.Add(previousYear);
        }

        private void ConfigureMembershipTypesChart()
        {
            ChartArea area = new ChartArea("MembershipTypesArea");
            area.BackColor = Color.Transparent;
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.Minimum = 0;
            area.AxisY.IsStartedFromZero = true;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("MembershipTypesLegend") { Enabled = false };
            chart1.Legends.Add(legend);

            Series series = new Series("MembershipTypes");
            series.ChartType = SeriesChartType.Column;
            series.ChartArea = "MembershipTypesArea";
            series.Legend = "MembershipTypesLegend";
            series.IsValueShownAsLabel = true;
            series["PointWidth"] = "0.6";

            chart1.Series.Add(series);
        }

        private void ConfigureGenderChart()
        {
            ChartArea area = new ChartArea("MainArea");
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("MainLegend") { Docking = Docking.Bottom };
            chart1.Legends.Add(legend);

            Series series = new Series("Gender");
            series.ChartType = SeriesChartType.Pie;
            series.ChartArea = "MainArea";
            series.Legend = "MainLegend";
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #VAL (#PERCENT{P0})";
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";

            chart1.Series.Add(series);
        }

        private void ConfigureServicesChart()
        {
            ChartArea area = new ChartArea("ServicesArea");
            area.BackColor = Color.Transparent;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.Interval = 1;
            area.AxisX.Minimum = 0;
            area.AxisX.IsStartedFromZero = true;
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("ServicesLegend") { Enabled = false };
            chart1.Legends.Add(legend);

            Series series = new Series("Services");
            series.ChartType = SeriesChartType.Bar;
            series.ChartArea = "ServicesArea";
            series.Legend = "ServicesLegend";
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            series["PointWidth"] = "0.6";

            chart1.Series.Add(series);
        }

        private void ConfigureOccupationsChart()
        {
            chart1.Palette = ChartColorPalette.BrightPastel;

            ChartArea area = new ChartArea("OccupationArea");
            area.BackColor = Color.Transparent;
            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;
            area.Position.Auto = false;
            area.Position.X = 2;
            area.Position.Y = 8;
            area.Position.Width = 96;
            area.Position.Height = 76;
            area.InnerPlotPosition.Auto = false;
            area.InnerPlotPosition.X = 10;
            area.InnerPlotPosition.Y = 8;
            area.InnerPlotPosition.Width = 80;
            area.InnerPlotPosition.Height = 80;
            chart1.ChartAreas.Add(area);

            chart1.Titles.Add(new Title(_previewTitle, Docking.Top,
                new Font("Segoe UI", 13, FontStyle.Bold), Color.Black)
            { Alignment = ContentAlignment.TopLeft });

            Legend legend = new Legend("OccupationLegend");
            legend.Enabled = true;
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.PowderBlue;
            legend.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            legend.IsTextAutoFit = false;
            chart1.Legends.Add(legend);

            Series series = new Series("Occupations");
            series.ChartType = SeriesChartType.Doughnut;
            series.ChartArea = "OccupationArea";
            series.Legend = "OccupationLegend";
            series.IsValueShownAsLabel = false;
            series["DoughnutRadius"] = "68";
            series.BorderColor = Color.White;
            series.BorderWidth = 2;

            chart1.Series.Add(series);
        }

        private string GetShortOccupationText(string text)
        {
            switch (text)
            {
                case "Ελεύθερος Επαγγελματίας":
                    return "Ελεύθερος επαγγ.";
                case "Ιδιωτικός Υπάλληλος":
                    return "Ιδιωτικός υπ.";
                case "Δημόσιος Υπάλληλος":
                    return "Δημόσιος υπ.";
                case "Φοιτητής/Μαθητής":
                    return "Φοιτητής/Μαθ.";
                default:
                    return text;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExportJPG_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JPEG Image (*.jpg)|*.jpg";
                sfd.FileName = MakeSafeFileName(_previewTitle) + ".jpg";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    chart1.SaveImage(sfd.FileName, ChartImageFormat.Jpeg);
                    MessageBox.Show("Η εξαγωγή εικόνας ολοκληρώθηκε επιτυχώς.",
                        "Εξαγωγή", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = MakeSafeFileName(_previewTitle) + ".csv";

                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;

                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                {
                    for (int i = 0; i < _data.Columns.Count; i++)
                    {
                        sw.Write(EscapeCsv(_data.Columns[i].ColumnName));
                        if (i < _data.Columns.Count - 1)
                            sw.Write(";");
                    }
                    sw.WriteLine();

                    foreach (DataRow row in _data.Rows)
                    {
                        for (int i = 0; i < _data.Columns.Count; i++)
                        {
                            sw.Write(EscapeCsv(row[i]?.ToString() ?? ""));
                            if (i < _data.Columns.Count - 1)
                                sw.Write(";");
                        }
                        sw.WriteLine();
                    }
                }

                MessageBox.Show("Η εξαγωγή δεδομένων ολοκληρώθηκε επιτυχώς.",
                    "Εξαγωγή", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dlg = new PrintDialog())
            {
                dlg.Document = _printDocument;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _printDocument.Print();
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold))
            {
                e.Graphics.DrawString(_previewTitle, titleFont, Brushes.Black, 40, 30);
            }

            using (Bitmap bmp = new Bitmap(chart1.Width, chart1.Height))
            {
                chart1.DrawToBitmap(bmp, new Rectangle(0, 0, chart1.Width, chart1.Height));

                Rectangle target = new Rectangle(40, 80,
                    e.PageBounds.Width - 80,
                    e.PageBounds.Height - 140);

                e.Graphics.DrawImage(bmp, target);
            }

            e.HasMorePages = false;
        }

        private string MakeSafeFileName(string text)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                text = text.Replace(c.ToString(), "");

            return text.Replace("|", "-").Trim();
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains("\""))
                value = value.Replace("\"", "\"\"");

            if (value.Contains(";") || value.Contains("\"") || value.Contains("\n"))
                value = "\"" + value + "\"";

            return value;
        }
    }
}