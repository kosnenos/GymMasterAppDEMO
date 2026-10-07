using System;
using System.Drawing;
using System.Drawing.Printing;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Printing
{
    public class CustomerMembershipPrintService
    {
        private const int LogoAreaHeight = 60;
        private const int DateTextHeight = 20;
        private const int CustomerInfoHeight = 86;
        private const int GapAfterCustomerInfo = 14;
        private const int TableHeaderHeight = 30;
        private const int RowHeight = 26;
        private const int FooterReservedHeight = 55;

        private CustomerMembershipPrintModel _report;
        private Image _logoImage;
        private int _currentRowIndex;
        private int _pageNumber;
        private int _totalPages;

        public PrintDocument CreatePrintDocument(CustomerMembershipPrintModel report, Image logoImage = null)
        {
            if (report == null)
                throw new ArgumentNullException("report");

            _report = report;
            _logoImage = logoImage;

            PrintDocument document = new PrintDocument();
            document.DocumentName = string.Format("Συνδρομές Πελάτη - {0}", _report.CustomerId);
            document.DefaultPageSettings.Landscape = false;
            document.DefaultPageSettings.Margins = new Margins(45, 45, 45, 60);

            document.BeginPrint += OnBeginPrint;
            document.PrintPage += OnPrintPage;

            return document;
        }

        private void OnBeginPrint(object sender, PrintEventArgs e)
        {
            _currentRowIndex = 0;
            _pageNumber = 0;

            PrintDocument document = sender as PrintDocument;
            _totalPages = CalculateTotalPages(document);
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            _pageNumber++;

            Graphics g = e.Graphics;
            Rectangle bounds = e.MarginBounds;

            using (Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold))
            using (Font subTitleFont = new Font("Segoe UI", 8.5f, FontStyle.Italic))
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (Font headerFont = new Font("Segoe UI Semibold", 8.7f, FontStyle.Bold))
            using (Font rowFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            using (Font footerFont = new Font("Segoe UI", 8, FontStyle.Regular))
            using (Pen accentPen = new Pen(Color.SteelBlue, 2.5f))
            using (Pen borderPen = new Pen(Color.FromArgb(185, 185, 185), 1f))
            using (SolidBrush accentBrush = new SolidBrush(Color.SteelBlue))
            using (SolidBrush headerBackBrush = new SolidBrush(Color.FromArgb(230, 238, 246)))
            using (SolidBrush alternateBackBrush = new SolidBrush(Color.FromArgb(247, 249, 252)))
            using (SolidBrush textGrayBrush = new SolidBrush(Color.DimGray))
            {
                int y = bounds.Top;

                // =========================
                // Header με λογότυπο
                // =========================
                Rectangle logoRect = new Rectangle(bounds.Left, y, 100, 50);
                DrawLogo(g, _logoImage, logoRect);

                // Πρώτα κατεβαίνουμε κάτω από το λογότυπο
                y += 58;

                // Μικρή απόσταση μεταξύ λογοτύπου και τίτλου
                y += 10;

                // Τίτλος κάτω από το λογότυπο
                /*DrawCenteredText(
                    g,
                    "Συνδρομές Πελάτη",
                    titleFont,
                    Brushes.Black,
                    bounds.Left,
                    y,
                    bounds.Width,
                    28);
                */

                g.DrawString(
                        "Συνδρομές Πελάτη",
                        titleFont,
                        Brushes.Black,
                        bounds.Left,
                        y);

                y += 34;

                g.DrawLine(accentPen, bounds.Left, y, bounds.Right, y);
                y += 8;

                // =========================
                // Στοιχεία πελάτη
                // =========================
                y = DrawCustomerInfoBlock(
                    g,
                    bounds,
                    y,
                    labelFont,
                    valueFont,
                    borderPen,
                    accentBrush);

                y += GapAfterCustomerInfo;

                // =========================
                // Header πίνακα
                // =========================
                int[] columnWidths = { 90, 145, 85, 85, 155, 75, 95 };
                string[] headers =
                {
                    "Αριθμ.\nΣυνδρομής",
                    "Τύπος Συνδρομής",
                    "Ημ. Έναρξης",
                    "Ημ. Λήξης",
                    "Υπηρεσία",
                    "Κόστος (€)",
                    "Κατάσταση"
                };

                DrawTableRow(
                    g,
                    headers,
                    columnWidths,
                    bounds.Left,
                    y,
                    TableHeaderHeight,
                    headerFont,
                    borderPen,
                    headerBackBrush,
                    alternateBackBrush,
                    true,
                    false);

                y += TableHeaderHeight;

                // =========================
                // Γραμμές πίνακα
                // =========================
                while (_currentRowIndex < _report.Memberships.Count)
                {
                    if (y + RowHeight > bounds.Bottom - FooterReservedHeight)
                    {
                        DrawFooter(g, bounds, bounds.Bottom - 8, footerFont, textGrayBrush);
                        e.HasMorePages = true;
                        return;
                    }

                    CustomerMembershipPrintRowModel item = _report.Memberships[_currentRowIndex];

                    string[] values =
                    {
                        item.MembershipId.ToString(),
                        Safe(item.MembershipTypeDescription),
                        item.StartDate.ToString("dd/MM/yyyy"),
                        item.EndDate.ToString("dd/MM/yyyy"),
                        Safe(item.ServiceDescription),
                        item.Price.ToString("N2"),
                        Safe(item.StatusDescriptionText)
                    };

                    bool isAlternate = (_currentRowIndex % 2 == 1);

                    DrawTableRow(
                        g,
                        values,
                        columnWidths,
                        bounds.Left,
                        y,
                        RowHeight,
                        rowFont,
                        borderPen,
                        headerBackBrush,
                        alternateBackBrush,
                        false,
                        isAlternate);

                    y += RowHeight;
                    _currentRowIndex++;
                }

                // =========================
                // Σύνολο εγγραφών
                // =========================
                y += 8;

                g.DrawLine(borderPen, bounds.Left, y, bounds.Right, y);
                y += 8;

                g.DrawString(
                    string.Format("Σύνολο Εγγραφών: {0}", _report.Memberships.Count),
                    labelFont,
                    Brushes.Black,
                    bounds.Left,
                    y);

                // Footer κάτω δεξιά: Σελίδα Χ από Υ
                DrawFooter(g, bounds, bounds.Bottom - 8, footerFont, textGrayBrush);

                e.HasMorePages = false;
            }
        }

        private int DrawCustomerInfoBlock(
            Graphics g,
            Rectangle bounds,
            int y,
            Font labelFont,
            Font valueFont,
            Pen borderPen,
            Brush accentBrush)
        {
            int rectHeight = CustomerInfoHeight;
            Rectangle infoRect = new Rectangle(bounds.Left, y, bounds.Width, rectHeight);

            g.FillRectangle(Brushes.WhiteSmoke, infoRect);
            g.DrawRectangle(borderPen, infoRect);

            g.FillRectangle(accentBrush, new Rectangle(infoRect.Left, infoRect.Top, 5, infoRect.Height));

            int paddingX = 14;
            int paddingY = 10;

            int left = bounds.Left + paddingX;
            int contentY = y + paddingY;

            int label1Width = 120;
            int value1Width = 230;
            int label2Width = 110;
            int value2Width = bounds.Width - (label1Width + value1Width + label2Width) - (paddingX * 2);

            DrawLabelValue(g, "Πελάτης:", _report.FullName, left, contentY, label1Width, value1Width, labelFont, valueFont);
            DrawLabelValue(g, "Κωδ. Πελάτη:", _report.CustomerId.ToString(), left + label1Width + value1Width, contentY, label2Width, value2Width, labelFont, valueFont);

            contentY += 24;

            DrawLabelValue(g, "Ημ/νία Εγγραφής:", _report.CreationDate.ToString("dd/MM/yyyy"), left, contentY, label1Width, value1Width, labelFont, valueFont);
            DrawLabelValue(g, "Τηλέφωνο:", Safe(_report.Mobile), left + label1Width + value1Width, contentY, label2Width, value2Width, labelFont, valueFont);

            contentY += 24;

            DrawLabelValue(g, "Email:", Safe(_report.Email), left, contentY, label1Width, bounds.Width - label1Width - (paddingX * 2), labelFont, valueFont);

            return y + rectHeight;
        }

        private void DrawLabelValue(
            Graphics g,
            string label,
            string value,
            int x,
            int y,
            int labelWidth,
            int valueWidth,
            Font labelFont,
            Font valueFont)
        {
            RectangleF labelRect = new RectangleF(x, y, labelWidth, 20);
            RectangleF valueRect = new RectangleF(x + labelWidth, y, valueWidth, 20);

            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                format.Trimming = StringTrimming.EllipsisCharacter;

                g.DrawString(label, labelFont, Brushes.Black, labelRect, format);
                g.DrawString(Safe(value), valueFont, Brushes.Black, valueRect, format);
            }
        }

        private void DrawCenteredText(
            Graphics g,
            string text,
            Font font,
            Brush brush,
            int x,
            int y,
            int width,
            int height)
        {
            RectangleF rect = new RectangleF(x, y, width, height);

            using (StringFormat centerFormat = new StringFormat())
            {
                centerFormat.Alignment = StringAlignment.Center;
                centerFormat.LineAlignment = StringAlignment.Center;

                g.DrawString(text, font, brush, rect, centerFormat);
            }
        }

        private void DrawTableRow(
            Graphics g,
            string[] values,
            int[] widths,
            int x,
            int y,
            int height,
            Font font,
            Pen borderPen,
            Brush headerBackBrush,
            Brush alternateBackBrush,
            bool isHeader,
            bool isAlternate)
        {
            int currentX = x;

            for (int i = 0; i < widths.Length; i++)
            {
                Rectangle rect = new Rectangle(currentX, y, widths[i], height);

                if (isHeader)
                    g.FillRectangle(headerBackBrush, rect);
                else if (isAlternate)
                    g.FillRectangle(alternateBackBrush, rect);
                else
                    g.FillRectangle(Brushes.White, rect);

                g.DrawRectangle(borderPen, rect);

                RectangleF textRect = new RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4);

                using (StringFormat format = new StringFormat())
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;

                    if (isHeader)
                    {
                        format.Alignment = StringAlignment.Center;
                    }
                    else
                    {
                        if (i == 0 || i == 2 || i == 3)
                            format.Alignment = StringAlignment.Center;
                        else if (i == 5)
                            format.Alignment = StringAlignment.Far;
                        else
                            format.Alignment = StringAlignment.Near;
                    }

                    g.DrawString(values[i] ?? string.Empty, font, Brushes.Black, textRect, format);
                }

                currentX += widths[i];
            }
        }

        private void DrawFooter(
            Graphics g,
            Rectangle bounds,
            int y,
            Font footerFont,
            Brush textGrayBrush)
        {
            RectangleF rightRect = new RectangleF(bounds.Right - 120, y, 120, 18);

            using (StringFormat rightFormat = new StringFormat())
            {
                rightFormat.Alignment = StringAlignment.Far;
                rightFormat.LineAlignment = StringAlignment.Center;

                g.DrawString(
                    string.Format("Σελίδα {0} από {1}", _pageNumber, _totalPages),
                    footerFont,
                    textGrayBrush,
                    rightRect,
                    rightFormat);
            }
        }

        private void DrawLogo(Graphics g, Image logoImage, Rectangle targetRect)
        {
            if (logoImage == null)
                return;

            float ratioX = (float)targetRect.Width / (float)logoImage.Width;
            float ratioY = (float)targetRect.Height / (float)logoImage.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int drawWidth = (int)(logoImage.Width * ratio);
            int drawHeight = (int)(logoImage.Height * ratio);

            int drawX = targetRect.X;
            int drawY = targetRect.Y + ((targetRect.Height - drawHeight) / 2);

            g.DrawImage(logoImage, new Rectangle(drawX, drawY, drawWidth, drawHeight));
        }

        private int CalculateTotalPages(PrintDocument document)
        {
            int rowsPerPage = CalculateRowsPerPage(document);

            if (rowsPerPage <= 0)
                rowsPerPage = 1;

            int totalRows = (_report.Memberships == null) ? 0 : _report.Memberships.Count;

            if (totalRows == 0)
                return 1;

            return (int)Math.Ceiling((double)totalRows / rowsPerPage);
        }

        private int CalculateRowsPerPage(PrintDocument document)
        {
            if (document == null)
                return 1;

            int pageHeight = document.DefaultPageSettings.PaperSize.Height;
            int topMargin = document.DefaultPageSettings.Margins.Top;
            int bottomMargin = document.DefaultPageSettings.Margins.Bottom;

            int printableHeight = pageHeight - topMargin - bottomMargin;

            int fixedHeightBeforeRows =
                LogoAreaHeight +
                8 +
                DateTextHeight +
                CustomerInfoHeight +
                GapAfterCustomerInfo +
                TableHeaderHeight;

            int availableHeightForRows = printableHeight - fixedHeightBeforeRows - FooterReservedHeight;

            if (availableHeightForRows <= 0)
                return 1;

            int rowsPerPage = availableHeightForRows / RowHeight;

            return rowsPerPage <= 0 ? 1 : rowsPerPage;
        }

        private string Safe(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }
    }
}