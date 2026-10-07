using DocumentFormat.OpenXml.Vml;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Models;
using GymMasterAppDemo.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GymMasterAppDemo.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly Action<Form> _openChildFormAction;

        public DashboardForm(Action<Form> openChildFormAction)
        {
            InitializeComponent();

            _openChildFormAction = openChildFormAction;

            ConfigureGenderChart();
            ConfigureServicesChart();
            ConfigureOccupationChart();
            ConfigureAnnualMembershipsChart();
            ConfigureRevenueChart();
            ConfigureMembershipTypesChart();
            BindChartPreviewEvents();
        }

        private void OpenAnalyticsTab(AnalyticsTab tab)
        {
            _openChildFormAction?.Invoke(new AnalyticsForm(tab));
        }

        public void RefreshDashboard()
        {
            LoadDashboardTiles();
            LoadGenderChart();
            LoadServicesChart();
            LoadOccupationChart();
            LoadAnnualMembershipsChart();
            LoadRevenueChart();
            LoadMembershipTypesChart();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void RecentCustomersTile_Click(object sender, EventArgs e)
        {
            OpenAnalyticsTab(AnalyticsTab.RecentCustomers);
        }

        private void ExpiringTodayTile_Click(object sender, EventArgs e)
        {
            OpenAnalyticsTab(AnalyticsTab.ExpiringToday);
        }

        private void UnpaidTile_Click(object sender, EventArgs e)
        {
            OpenAnalyticsTab(AnalyticsTab.UnpaidMemberships);
        }
// Πλακίδια
        private void LoadDashboardTiles()
        {
            string sql = @"
                SELECT
                    (SELECT COUNT(*)
                     FROM Customers c
                     WHERE CAST(c.CreationDate AS date) >= DATEADD(MONTH, -1, CAST(GETDATE() AS date))
                    ) AS RecentCustomersCount,

                    (SELECT COUNT(*)
                     FROM Membership m
                     WHERE CAST(m.EndDate AS date) = CAST(GETDATE() AS date)
                    ) AS ExpiringTodayCount,

                    (SELECT COUNT(*)
                     FROM
                     (
                         SELECT m.Id
                         FROM Membership m
                         LEFT JOIN Payments p ON p.MembershipId = m.Id
                         WHERE m.StatusCode IN ('0', '2')
                         GROUP BY m.Id, m.Price
                         HAVING (m.Price - ISNULL(SUM(p.Amount), 0)) > 0
                     ) x
                    ) AS UnpaidMembershipsCount;";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int recentCustomers = Convert.ToInt32(reader["RecentCustomersCount"]);
                        int expiringToday = Convert.ToInt32(reader["ExpiringTodayCount"]);
                        int unpaidMemberships = Convert.ToInt32(reader["UnpaidMembershipsCount"]);

                        lblNumNewCustomers.Text = recentCustomers.ToString();
                        lblExpiredToday.Text = expiringToday.ToString();
                        lblNumOfUnpaid.Text = unpaidMemberships.ToString();
                    }
                }
            }
        }

        // Συνδρομές τρέχον έτους
        private void LoadAnnualMembershipsChart()
        {
            string sql = @"
                        SELECT T.YEARRECORD AS YearOfMembership,
                               COUNT(T.ID) AS NumOfRecords
                        FROM
                        (
                            SELECT ID,
                                   YEAR(CREATIONDATE) AS YEARRECORD
                            FROM MEMBERSHIP
                        ) T
                        GROUP BY T.YEARRECORD
                        ORDER BY T.YEARRECORD";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series series = chartAnnualMemberships.Series["AnnualMemberships"];
                    series.Points.Clear();

                    while (reader.Read())
                    {
                        string yearofmemberships = reader["YearOfMembership"].ToString();
                        int memberships = Convert.ToInt32(reader["NumOfRecords"]);

                        series.Points.AddXY(yearofmemberships, memberships);
                    }
                }
            }
        }
        private void ConfigureAnnualMembershipsChart()
        {
            chartAnnualMemberships.Titles.Clear();
            chartAnnualMemberships.Series.Clear();
            chartAnnualMemberships.ChartAreas.Clear();
            chartAnnualMemberships.Legends.Clear();

            ChartArea area = new ChartArea("AnnualMemberships");

            // Φόντο της περιοχής δεδομένων (το άσπρο που βλέπεις μέσα)
            area.BackColor = Color.Transparent;

            // Μόνο οριζόντιες γραμμές πλέγματος
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;

            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MinorGrid.Enabled = false;

            // Αχνό στυλ για τις οριζόντιες γραμμές
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MajorGrid.LineWidth = 1;

            // Αφαίρεση περιγράμματος της περιοχής δεδομένων
            area.BorderWidth = 0;
            area.BorderColor = Color.Transparent;

            // Προαιρετικά: να μη φαίνονται οι γραμμές των αξόνων
            area.AxisX.LineColor = Color.Gainsboro;
            area.AxisY.LineColor = Color.Gainsboro;

            // Προαιρετικά: να μη φαίνονται τα tick marks
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisX.MinorTickMark.Enabled = false;
            area.AxisY.MinorTickMark.Enabled = false;

            // Για bar chart συνήθως αυτό είναι πιο χρήσιμο
            // area.AxisY.Interval = 100;

            chartAnnualMemberships.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Συνδρομές Πελατών ανά Έτος",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartAnnualMemberships.Titles.Add(title);

            Legend legend = new Legend("AnnualMemberships");
            legend.Enabled = false;
            chartAnnualMemberships.Legends.Add(legend);

            Series series = new Series("AnnualMemberships");
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "AnnualMemberships";
            series.IsValueShownAsLabel = true;
            series.Legend = "AnnualMemberships";
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 7;
            series.Color = Color.MediumPurple;

            chartAnnualMemberships.Series.Add(series);

        }

        // Έσοδα τρέχοντος έτους από εξοφλημένες
        private void LoadRevenueChart()
        {
            string sql = @"
                            WITH Months AS
                            (
                                SELECT 1 AS MonthNum, N'Ιαν' AS MonthName
                                UNION ALL SELECT 2, N'Φεβ'
                                UNION ALL SELECT 3, N'Μαρ'
                                UNION ALL SELECT 4, N'Απρ'
                                UNION ALL SELECT 5, N'Μάι'
                                UNION ALL SELECT 6, N'Ιον'
                                UNION ALL SELECT 7, N'Ιολ'
                                UNION ALL SELECT 8, N'Αυγ'
                                UNION ALL SELECT 9, N'Σεπ'
                                UNION ALL SELECT 10, N'Οκτ'
                                UNION ALL SELECT 11, N'Νοε'
                                UNION ALL SELECT 12, N'Δεκ'
                            )
                            SELECT
                                m.MonthNum,
                                m.MonthName,
                                ISNULL(SUM(CASE
                                    WHEN YEAR(p.PaymentDate) = YEAR(GETDATE())
                                     AND ms.StatusCode = '1'
                                    THEN p.Amount
                                    ELSE 0
                                END), 0) AS CurrentYearRevenue,
                                ISNULL(SUM(CASE
                                    WHEN YEAR(p.PaymentDate) = YEAR(GETDATE()) - 1
                                     AND ms.StatusCode = '1'
                                    THEN p.Amount
                                    ELSE 0
                                END), 0) AS PreviousYearRevenue
                            FROM Months m
                            LEFT JOIN Payments p
                                ON MONTH(p.PaymentDate) = m.MonthNum
                            LEFT JOIN Membership ms
                                ON ms.Id = p.MembershipId
                            GROUP BY
                                m.MonthNum,
                                m.MonthName
                            ORDER BY
                                m.MonthNum;";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series currentYearSeries = chartRevenue.Series["Τρέχον Έτος"];
                    Series previousYearSeries = chartRevenue.Series["Προηγούμενο Έτος"];

                    currentYearSeries.Points.Clear();
                    previousYearSeries.Points.Clear();

                    while (reader.Read())
                    {
                        string monthName = reader["MonthName"].ToString();
                        decimal currentYearRevenue = Convert.ToDecimal(reader["CurrentYearRevenue"]);
                        decimal previousYearRevenue = Convert.ToDecimal(reader["PreviousYearRevenue"]);

                        currentYearSeries.Points.AddXY(monthName, currentYearRevenue);
                        previousYearSeries.Points.AddXY(monthName, previousYearRevenue);
                    }
                }
            }
        }
        private void ConfigureRevenueChart()
        {
            chartRevenue.Titles.Clear();
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas.Clear();
            chartRevenue.Legends.Clear();

            chartRevenue.BackColor = Color.LightCyan;
            chartRevenue.BorderlineWidth = 0;

            ChartArea area = new ChartArea("RevenueArea");
            area.BackColor = Color.Transparent;

            area.AxisX.Title = "";
            area.AxisY.Title = "Έσοδα (€)";
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;

            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MinorGrid.Enabled = false;
            area.AxisY.Minimum = 0;
            area.AxisY.IsStartedFromZero = true;

            area.AxisX.LineColor = Color.Transparent;
            area.AxisY.LineColor = Color.Transparent;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;

            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            chartRevenue.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Έσοδα από Εξοφλημένες Συνδρομές",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartRevenue.Titles.Add(title);

            Legend legend = new Legend("RevenueLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            chartRevenue.Legends.Add(legend);

            Series currentYearSeries = new Series("Τρέχον Έτος");
            currentYearSeries.ChartType = SeriesChartType.Line;
            currentYearSeries.ChartArea = "RevenueArea";
            currentYearSeries.Legend = "RevenueLegend";
            currentYearSeries.BorderWidth = 3;
            currentYearSeries.Color = Color.Green;
            currentYearSeries.MarkerStyle = MarkerStyle.Circle;
            currentYearSeries.MarkerSize = 7;
            currentYearSeries.IsValueShownAsLabel = true;
            currentYearSeries.LabelFormat = "0.##";

            Series previousYearSeries = new Series("Προηγούμενο Έτος");
            previousYearSeries.ChartType = SeriesChartType.Line;
            previousYearSeries.ChartArea = "RevenueArea";
            previousYearSeries.Legend = "RevenueLegend";
            previousYearSeries.BorderWidth = 2;
            previousYearSeries.Color = Color.IndianRed;
            previousYearSeries.MarkerStyle = MarkerStyle.Circle;
            previousYearSeries.MarkerSize = 6;
            previousYearSeries.IsValueShownAsLabel = false;

            chartRevenue.Series.Add(currentYearSeries);
            chartRevenue.Series.Add(previousYearSeries);
        }

        // Προτιμήσεις συνδρομών
        private void LoadMembershipTypesChart()
        {
            string sql = @"
                            SELECT
                                mt.Description AS MembershipTypeDescription,
                                COUNT(m.Id) AS TotalMemberships
                            FROM MembershipTypeList mt
                            LEFT JOIN Membership m
                                ON m.MembershipType = mt.MembershipType
                               AND YEAR(m.CreationDate) = YEAR(GETDATE())
                            GROUP BY
                                mt.MembershipType,
                                mt.Description
                            ORDER BY
                                mt.MembershipType;";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series series = chartMembershipTypes.Series["MembershipTypes"];
                    series.Points.Clear();

                    while (reader.Read())
                    {
                        string membershipType = reader["MembershipTypeDescription"].ToString();
                        int total = Convert.ToInt32(reader["TotalMemberships"]);

                        int pointIndex = series.Points.AddXY(membershipType, total);

                        // διαφορετικό χρώμα για κάθε στήλη
                        switch (membershipType)
                        {
                            case "Ημερήσια":
                                series.Points[pointIndex].Color = Color.SteelBlue;
                                break;
                            case "Μηνιαία":
                                series.Points[pointIndex].Color = Color.MediumSeaGreen;
                                break;
                            case "Τρίμηνη":
                                series.Points[pointIndex].Color = Color.Goldenrod;
                                break;
                            case "Ετήσια":
                                series.Points[pointIndex].Color = Color.IndianRed;
                                break;
                            default:
                                series.Points[pointIndex].Color = Color.MediumPurple;
                                break;
                        }
                    }
                }
            }
        }
        private void ConfigureMembershipTypesChart()
        {
            chartMembershipTypes.Titles.Clear();
            chartMembershipTypes.Series.Clear();
            chartMembershipTypes.ChartAreas.Clear();
            chartMembershipTypes.Legends.Clear();

            chartMembershipTypes.BackColor = Color.Transparent;
            chartMembershipTypes.BorderlineWidth = 0;

            ChartArea area = new ChartArea("MembershipTypesArea");
            area.BackColor = Color.Transparent;

            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;

            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MinorGrid.Enabled = false;
            area.AxisY.Minimum = 0;
            area.AxisY.IsStartedFromZero = true;

            area.AxisX.LineColor = Color.Transparent;
            area.AxisY.LineColor = Color.Transparent;

            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisX.MinorTickMark.Enabled = false;
            area.AxisY.MinorTickMark.Enabled = false;

            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            chartMembershipTypes.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Προτιμήσεις Τύπων Συνδρομής | Τρέχον Έτος",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartMembershipTypes.Titles.Add(title);

            Legend legend = new Legend("MembershipTypesLegend");
            legend.Enabled = false;
            chartMembershipTypes.Legends.Add(legend);

            Series series = new Series("MembershipTypes");
            series.ChartType = SeriesChartType.Column;
            series.ChartArea = "MembershipTypesArea";
            series.Legend = "MembershipTypesLegend";
            series.IsValueShownAsLabel = true;
            series["PointWidth"] = "0.6";

            chartMembershipTypes.Series.Add(series);
        }

        // Κατανομή πελατών ανά φύλο
        private void LoadGenderChart()
        {
            string sql = @"
                        SELECT Gender, COUNT(*) AS Total
                        FROM Customers
                        WHERE Status = 1
                          AND Gender IS NOT NULL
                        GROUP BY Gender";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series series = chartGender.Series["Gender"];
                    series.Points.Clear();

                    while (reader.Read())
                    {
                        string gender = reader["Gender"].ToString();
                        int total = Convert.ToInt32(reader["Total"]);

                        string text;
                        if (gender == "Α")
                            text = "Άνδρες";
                        else if (gender == "Γ")
                            text = "Γυναίκες";
                        else
                            text = gender;

                        series.Points.AddXY(text, total);
                    }
                }
            }
        }
        private void ConfigureGenderChart()
        {
            chartGender.Series.Clear();
            chartGender.ChartAreas.Clear();
            chartGender.Legends.Clear();

            ChartArea area = new ChartArea("MainArea");
            chartGender.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Κατανομή Πελατών ανά Φύλο",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartGender.Titles.Add(title);

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Bottom;
            chartGender.Legends.Add(legend);

            Series series = new Series("Gender");
            series.ChartType = SeriesChartType.Pie;
            series.ChartArea = "MainArea";
            series.Legend = "MainLegend";
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #VAL (#PERCENT{P0})";
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";

            chartGender.Series.Add(series);
        }

        // Δημοφιλείς υπηρεσίες
        private void LoadServicesChart()
        {
            string sql = @"
                SELECT 
                    s.ServiceDescription,
                    COUNT(DISTINCT m.CustomerId) AS TotalCustomers
                FROM Membership m
                INNER JOIN ServicesList s ON s.ServiceCode = m.ServiceCode
                GROUP BY s.ServiceDescription
                ORDER BY COUNT(DISTINCT m.CustomerId) DESC, s.ServiceDescription";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series series = chartServices.Series["Services"];
                    series.Points.Clear();

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

                    while (reader.Read())
                    {
                        string service = reader["ServiceDescription"].ToString();
                        int total = Convert.ToInt32(reader["TotalCustomers"]);

                        int pointIndex = series.Points.AddXY(service, total);
                        series.Points[pointIndex].Color = barColors[colorIndex % barColors.Length];

                        colorIndex++;
                    }
                }
            }
        }
        private void ConfigureServicesChart()
        {
            chartServices.Series.Clear();
            chartServices.ChartAreas.Clear();
            chartServices.Legends.Clear();
            chartServices.Titles.Clear();

            chartServices.BackColor = Color.Transparent;
            chartServices.Palette = ChartColorPalette.None;

            ChartArea area = new ChartArea("ServicesArea");
            area.BackColor = Color.Transparent;

            // Χωρίς πλέγμα
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            // Χωρίς περίγραμμα chart area
            area.BorderWidth = 0;
            area.BorderColor = Color.Transparent;

            // Αχνός άξονας Υ
            area.AxisY.LineColor = Color.FromArgb(180, 180, 180);
            area.AxisY.LineWidth = 1;

            // Ο άξονας Χ να μη φαίνεται
            area.AxisX.LineColor = Color.Transparent;

            // Χωρίς tick marks
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisX.MinorTickMark.Enabled = false;
            area.AxisY.MinorTickMark.Enabled = false;

            area.AxisY.Interval = 1;
            area.AxisX.Minimum = 0;
            area.AxisX.IsStartedFromZero = true;

            //area.AxisX.LabelStyle.ForeColor = Color.DimGray;
            area.AxisY.LabelStyle.ForeColor = Color.Black;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);

            chartServices.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Δημοφιλείς Υπηρεσίες Γυμναστηρίου",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartServices.Titles.Add(title);

            Legend legend = new Legend("ServicesLegend");
            legend.Enabled = false;
            chartServices.Legends.Add(legend);

            Series series = new Series("Services");
            series.ChartType = SeriesChartType.Bar;
            series.ChartArea = "ServicesArea";
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Black;
            series.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            series["PointWidth"] = "0.6";

            chartServices.Series.Add(series);
        }

        // Επαγγελματικό προφίλ πελατών
        private void LoadOccupationChart()
        {
            string sql = @"
                SELECT 
                    ISNULL(o.OccupationDesc, N'Χωρίς καταχώριση') AS OccupationDescription,
                    COUNT(c.Id) AS TotalCustomers
                FROM Customers c
                    LEFT JOIN OccupationList o ON o.OccupationId = c.OccupationId
                WHERE c.Status = 1
                GROUP BY ISNULL(o.OccupationDesc, N'Χωρίς καταχώριση')
                ORDER BY OccupationDescription";

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Series series = chartOccupations.Series["Occupations"];
                    series.Points.Clear();

                    var items = new List<Tuple<string, int>>();
                    int grandTotal = 0;

                    while (reader.Read())
                    {
                        string occupation = reader["OccupationDescription"].ToString();
                        int total = Convert.ToInt32(reader["TotalCustomers"]);

                        items.Add(Tuple.Create(occupation, total));
                        grandTotal += total;
                    }

                    foreach (var item in items)
                    {
                        int pointIndex = series.Points.AddXY(item.Item1, item.Item2);

                        double percent = grandTotal == 0
                            ? 0
                            : (item.Item2 * 100.0 / grandTotal);

                        series.Points[pointIndex].LegendText =
                            GetShortOccupationText(item.Item1) +
                            ": " + item.Item2 +
                            " (" + percent.ToString("0") + "%)";
                    }
                }
            }
        }
        private void ConfigureOccupationChart()
        {
            chartOccupations.Titles.Clear();
            chartOccupations.Series.Clear();
            chartOccupations.ChartAreas.Clear();
            chartOccupations.Legends.Clear();

            chartOccupations.BackColor = Color.Transparent;
            chartOccupations.BorderlineWidth = 0;
            chartOccupations.Palette = ChartColorPalette.BrightPastel;

            ChartArea area = new ChartArea("OccupationArea");
            area.BackColor = Color.Transparent;

            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;
            area.BorderWidth = 0;
            area.BorderColor = Color.Transparent;

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

            chartOccupations.ChartAreas.Add(area);

            Title title = new Title
            {
                Text = "Επαγγελματικό Προφίλ Πελατών",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Alignment = ContentAlignment.TopLeft
            };
            chartOccupations.Titles.Add(title);

            Legend legend = new Legend("OccupationLegend");
            legend.Enabled = true;
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.PowderBlue;
            legend.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            legend.IsTextAutoFit = false;
            chartOccupations.Legends.Add(legend);

            Series series = new Series("Occupations");
            series.ChartType = SeriesChartType.Doughnut;
            series.ChartArea = "OccupationArea";
            series.Legend = "OccupationLegend";
            series.IsValueShownAsLabel = false;   // χωρίς labels έξω από το donut

            series["DoughnutRadius"] = "68";
            series.BorderColor = Color.White;
            series.BorderWidth = 2;

            chartOccupations.Series.Add(series);
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

        private void BindChartPreviewEvents()
        {
            BindChartPreview(chartAnnualMemberships, pnlAnnualMemberships, () =>
                OpenChartPreview("ANNUAL", "Συνδρομές Πελατών ανά Έτος", GetAnnualMembershipsPreviewData()));

            BindChartPreview(chartRevenue, pnlRevenue, () =>
                OpenChartPreview("REVENUE", "Έσοδα από Εξοφλημένες Συνδρομές", GetRevenuePreviewData()));

            BindChartPreview(chartMembershipTypes, pnlMembershipTypes, () =>
                OpenChartPreview("MEMBERSHIP_TYPES", "Προτιμήσεις Τύπων Συνδρομής | Τρέχον Έτος", GetMembershipTypesPreviewData()));

            BindChartPreview(chartGender, pnlGenderChart, () =>
                OpenChartPreview("GENDER", "Κατανομή Πελατών ανά Φύλο", GetGenderPreviewData()));

            BindChartPreview(chartServices, pnlServicesChart, () =>
                OpenChartPreview("SERVICES", "Δημοφιλείς Υπηρεσίες Γυμναστηρίου", GetServicesPreviewData()));

            BindChartPreview(chartOccupations, pnlOccupationChart, () =>
                OpenChartPreview("OCCUPATIONS", "Επαγγελματικό Προφίλ Πελατών", GetOccupationsPreviewData()));
        }

        private void BindChartPreview(Control chartControl, Control panelControl, Action openAction)
        {
            chartControl.Cursor = Cursors.Hand;
            panelControl.Cursor = Cursors.Hand;

            chartControl.Click += (s, e) => openAction();
            panelControl.Click += (s, e) => openAction();
        }

        private void OpenChartPreview(string previewCode, string title, DataTable data)
        {
            using (ChartDetailsForm frm = new ChartDetailsForm(previewCode, title, data))
            {
                frm.ShowDialog(this);
            }
        }

        private DataTable ExecuteDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Db.CreateConnection(Db.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
            {
                da.Fill(dt);
            }

            return dt;
        }

        private DataTable GetAnnualMembershipsPreviewData()
        {
            string sql = @"
                        SELECT 
                            YEAR(CreationDate) AS [Έτος],
                            COUNT(Id) AS [Συνδρομές]
                        FROM Membership
                        GROUP BY YEAR(CreationDate)
                        ORDER BY YEAR(CreationDate);";

            return ExecuteDataTable(sql);
        }

        private DataTable GetRevenuePreviewData()
        {
            string sql = @"
                        WITH Months AS
                        (
                            SELECT 1 AS MonthNum, N'Ιαν' AS MonthName
                            UNION ALL SELECT 2, N'Φεβ'
                            UNION ALL SELECT 3, N'Μαρ'
                            UNION ALL SELECT 4, N'Απρ'
                            UNION ALL SELECT 5, N'Μάι'
                            UNION ALL SELECT 6, N'Ιον'
                            UNION ALL SELECT 7, N'Ιολ'
                            UNION ALL SELECT 8, N'Αυγ'
                            UNION ALL SELECT 9, N'Σεπ'
                            UNION ALL SELECT 10, N'Οκτ'
                            UNION ALL SELECT 11, N'Νοε'
                            UNION ALL SELECT 12, N'Δεκ'
                        )
                        SELECT
                            m.MonthName AS [Μήνας],
                            ISNULL(SUM(CASE
                                WHEN YEAR(p.PaymentDate) = YEAR(GETDATE())
                                 AND ms.StatusCode = '1'
                                THEN p.Amount
                                ELSE 0
                            END), 0) AS [Τρέχον Έτος],
                            ISNULL(SUM(CASE
                                WHEN YEAR(p.PaymentDate) = YEAR(GETDATE()) - 1
                                 AND ms.StatusCode = '1'
                                THEN p.Amount
                                ELSE 0
                            END), 0) AS [Προηγούμενο Έτος]
                        FROM Months m
                        LEFT JOIN Payments p
                            ON MONTH(p.PaymentDate) = m.MonthNum
                        LEFT JOIN Membership ms
                            ON ms.Id = p.MembershipId
                        GROUP BY
                            m.MonthNum,
                            m.MonthName
                        ORDER BY
                            m.MonthNum;";

            return ExecuteDataTable(sql);
        }

        private DataTable GetMembershipTypesPreviewData()
        {
            string sql = @"
                        SELECT
                            mt.Description AS [Τύπος Συνδρομής],
                            COUNT(m.Id) AS [Πλήθος]
                        FROM MembershipTypeList mt
                        LEFT JOIN Membership m
                            ON m.MembershipType = mt.MembershipType
                           AND YEAR(m.CreationDate) = YEAR(GETDATE())
                        GROUP BY
                            mt.MembershipType,
                            mt.Description
                        ORDER BY
                            mt.MembershipType;";

            return ExecuteDataTable(sql);
        }

        private DataTable GetGenderPreviewData()
        {
            string sql = @"
                        SELECT
                            CASE
                                WHEN Gender = 'Α' THEN N'Άνδρες'
                                WHEN Gender = 'Γ' THEN N'Γυναίκες'
                                ELSE Gender
                            END AS [Φύλο],
                            COUNT(*) AS [Πλήθος]
                        FROM Customers
                        WHERE Status = 1
                            AND Gender IS NOT NULL
                        GROUP BY Gender
                        ORDER BY [Φύλο];";

            return ExecuteDataTable(sql);
        }

        private DataTable GetServicesPreviewData()
        {
            string sql = @"
                        SELECT
                            s.ServiceDescription AS [Υπηρεσία],
                            COUNT(DISTINCT m.CustomerId) AS [Πελάτες]
                        FROM Membership m
                        INNER JOIN ServicesList s ON s.ServiceCode = m.ServiceCode
                        GROUP BY s.ServiceDescription
                        ORDER BY COUNT(DISTINCT m.CustomerId) DESC, s.ServiceDescription;";

            return ExecuteDataTable(sql);
        }

        private DataTable GetOccupationsPreviewData()
        {
            string sql = @"
                        SELECT
                            ISNULL(o.OccupationDesc, N'Χωρίς καταχώριση') AS [Επάγγελμα],
                            COUNT(c.Id) AS [Πελάτες]
                        FROM Customers c
                        LEFT JOIN OccupationList o ON o.OccupationId = c.OccupationId
                        WHERE c.Status = 1
                        GROUP BY ISNULL(o.OccupationDesc, N'Χωρίς καταχώριση')
                        ORDER BY [Επάγγελμα];";

            return ExecuteDataTable(sql);
        }

    }
}
