using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Printing
{
    public class WorkoutProgramPrintDocument
    {
        private readonly WorkoutProgramPrintHeaderModel _header;
        private readonly List<WorkoutProgramPrintDetailModel> _details;
        private readonly Image _logo;

        private readonly PrintDocument _printDocument;

        private readonly List<PrintLine> _lines;

        private int _currentLineIndex;
        private bool _commentsPrinted;
        private int _currentPage;

        private readonly Font _fontGymInfo = new Font("Calibri", 9f, FontStyle.Regular);
        private readonly Font _fontTitle = new Font("Calibri", 13f, FontStyle.Bold);
        private readonly Font _fontLabel = new Font("Calibri", 9f, FontStyle.Bold);
        private readonly Font _fontBody = new Font("Calibri", 9f, FontStyle.Regular);
        private readonly Font _fontDay = new Font("Calibri", 10f, FontStyle.Bold);
        private readonly Font _fontMuscle = new Font("Calibri", 10f, FontStyle.Bold);
        private readonly Font _fontFooter = new Font("Calibri", 8f, FontStyle.Regular);

        public WorkoutProgramPrintDocument(
            WorkoutProgramPrintHeaderModel header,
            List<WorkoutProgramPrintDetailModel> details,
            Image logo = null)
        {
            _header = header;
            _details = details ?? new List<WorkoutProgramPrintDetailModel>();
            _logo = logo;

            _lines = BuildLines();

            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Margins = new Margins(45, 45, 35, 45);
            _printDocument.BeginPrint += PrintDocument_BeginPrint;
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void ShowPreview()
        {
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = _printDocument;
                preview.Width = 1200;
                preview.Height = 800;
                preview.StartPosition = FormStartPosition.CenterScreen;
                preview.ShowDialog();
            }
        }

        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentLineIndex = 0;
            _commentsPrinted = false;
            _currentPage = 0;
        }

        private List<PrintLine> BuildLines()
        {
            List<PrintLine> lines = new List<PrintLine>();

            var dayGroups = _details
                .GroupBy(x => x.DayOfWeek)
                .OrderBy(x => GetDayOrder(x.Key));

            foreach (var dayGroup in dayGroups)
            {
                lines.Add(PrintLine.CreateDay(dayGroup.Key));

                var muscleGroups = dayGroup
                    .GroupBy(x => x.MuscleGroup)
                    .OrderBy(x => x.Key);

                foreach (var muscleGroup in muscleGroups)
                {
                    lines.Add(PrintLine.CreateMuscle(muscleGroup.Key));
                    lines.Add(PrintLine.CreateColumns());

                    foreach (WorkoutProgramPrintDetailModel row in muscleGroup)
                    {
                        lines.Add(PrintLine.CreateExercise(
                            row.ExerciseName,
                            row.Sets.ToString(),
                            row.Reps.ToString(),
                            row.RestTime));
                    }

                    lines.Add(PrintLine.CreateSpacer());
                }
            }

            return lines;
        }

        private int GetDayOrder(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "1η Μέρα": return 1;
                case "2η Μέρα": return 2;
                case "3η Μέρα": return 3;
                case "4η Μέρα": return 4;
                case "5η Μέρα": return 5;
                case "6η Μέρα": return 6;
                default: return 99;
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            _currentPage++;

            Graphics g = e.Graphics;
            Rectangle bounds = e.MarginBounds;

            float y = bounds.Top;
            float footerTop = bounds.Bottom - 20;

            DrawPageHeader(g, bounds, ref y);
            DrawProgramInfo(g, bounds, ref y);

            while (_currentLineIndex < _lines.Count)
            {
                PrintLine line = _lines[_currentLineIndex];
                float lineHeight = GetLineHeight(line);

                if (y + lineHeight > footerTop)
                {
                    DrawFooter(g, bounds);
                    e.HasMorePages = true;
                    return;
                }

                DrawLine(g, bounds, ref y, line);
                _currentLineIndex++;
            }

            if (!_commentsPrinted)
            {
                string commentsLabel = "Παρατηρήσεις:";
                string commentsText = string.IsNullOrWhiteSpace(_header.Comments) ? "-" : _header.Comments;

                // Έξτρα απόσταση πριν από τις παρατηρήσεις
                float commentsTopSpacing = 25f;

                SizeF labelSize = g.MeasureString(commentsLabel, _fontLabel);
                SizeF textSize = g.MeasureString(commentsText, _fontBody, bounds.Width - 12);

                float neededHeight = commentsTopSpacing + labelSize.Height + 8 + textSize.Height + 16;
                if (y + neededHeight > footerTop)
                {
                    DrawFooter(g, bounds);
                    e.HasMorePages = true;
                    return;
                }

                y += commentsTopSpacing;

                g.DrawString(commentsLabel, _fontLabel, Brushes.Black, bounds.Left, y);
                y += labelSize.Height + 4;

                // RectangleF commentsBox = new RectangleF(bounds.Left, y, bounds.Width, textSize.Height + 12);
                // g.DrawRectangle(Pens.Gray, Rectangle.Round(commentsBox));
                g.DrawString(
                    commentsText,
                    _fontBody,
                    Brushes.Black,
                    new RectangleF(bounds.Left + 2, y, bounds.Width - 4, textSize.Height + 4));
                // new RectangleF(commentsBox.Left + 5, commentsBox.Top + 5, commentsBox.Width - 10, commentsBox.Height - 10));

                //y += commentsBox.Height + 8;
                y += textSize.Height + 10;
                _commentsPrinted = true;
            }

            DrawFooter(g, bounds);
            e.HasMorePages = false;
        }

        private void DrawPageHeader(Graphics g, Rectangle bounds, ref float y)
        {
            int left = bounds.Left;
            int width = bounds.Width;

            Rectangle logoRect = new Rectangle(left, (int)y, 95, 75);

            if (_logo != null)
            {
                float scale = Math.Min((float)logoRect.Width / _logo.Width,
                    (float)logoRect.Height / _logo.Height);
                float logoWidth = _logo.Width * scale;
                float logoHeight = _logo.Height * scale;
                g.DrawImage(_logo,
                    logoRect.Left + (logoRect.Width - logoWidth) / 2,
                    logoRect.Top + (logoRect.Height - logoHeight) / 2,
                    logoWidth, logoHeight);
            }

            float textX = left + 110;

            g.DrawString("GymMaster Fitness Center", _fontLabel, Brushes.Black, textX, y + 2);
            g.DrawString("Demo Street 10, 00000 Demo City", _fontGymInfo, Brushes.Black, textX, y + 22);
            g.DrawString("+30 210 0000000 | info@example.com", _fontGymInfo, Brushes.Black, textX, y + 38);

            y += 82;

            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            g.DrawString(
                "Ατομικό Πρόγραμμα Προπόνησης | Κωδ.: " + _header.ProgramId.ToString("D5"),
                _fontTitle,
                Brushes.Black,
                new RectangleF(left, y, width, 25),
                centerFormat);

            y += 28;
            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 10;
        }

        private void DrawProgramInfo(Graphics g, Rectangle bounds, ref float y)
        {
            float x = bounds.Left;

            g.DrawString("Κωδ. Πελάτη:", _fontLabel, Brushes.Black, x, y);
            g.DrawString(_header.CustomerId.ToString(), _fontBody, Brushes.Black, x + 82, y);

            g.DrawString("Επώνυμο:", _fontLabel, Brushes.Black, x + 150, y);
            g.DrawString(_header.LastName ?? string.Empty, _fontBody, Brushes.Black, x + 215, y);

            g.DrawString("Όνομα:", _fontLabel, Brushes.Black, x + 390, y);
            g.DrawString(_header.FirstName ?? string.Empty, _fontBody, Brushes.Black, x + 445, y);

            y += 22;

            g.DrawString("Στόχος:", _fontLabel, Brushes.Black, x, y);
            g.DrawString(_header.Goal ?? string.Empty, _fontBody, Brushes.Black, x + 55, y);

            g.DrawString("Συχνότητα:", _fontLabel, Brushes.Black, x + 250, y);
            g.DrawString(_header.Frequency.ToString() + " Ημέρες", _fontBody, Brushes.Black, x + 325, y);

            g.DrawString("Διάρκεια:", _fontLabel, Brushes.Black, x + 450, y);
            g.DrawString(_header.Duration.ToString() + " Εβδομάδες", _fontBody, Brushes.Black, x + 515, y);

            y += 22;

            g.DrawString("Έναρξη:", _fontLabel, Brushes.Black, x, y);
            g.DrawString(_header.StartDate.ToString("dd/MM/yyyy"), _fontBody, Brushes.Black, x + 55, y);

            g.DrawString("Λήξη:", _fontLabel, Brushes.Black, x + 250, y);
            g.DrawString(_header.EndDate.ToString("dd/MM/yyyy"), _fontBody, Brushes.Black, x + 290, y);

            y += 18;
            g.DrawLine(Pens.Black, bounds.Left, y, bounds.Right, y);
            y += 10;
        }

        private float GetLineHeight(PrintLine line)
        {
            switch (line.Type)
            {
                case PrintLineType.DayHeader: return 24f;
                case PrintLineType.MuscleHeader: return 20f;
                case PrintLineType.ColumnHeader: return 20f;
                case PrintLineType.ExerciseRow: return 18f;
                default: return 8f;
            }
        }

        private void DrawLine(Graphics g, Rectangle bounds, ref float y, PrintLine line)
        {
            float x = bounds.Left;
            float width = bounds.Width;

            // Εσοχή για την περιοχή των ασκήσεων (σαν 3 TAB περίπου)
            float detailsIndent = 60f;

            float detailX = x + detailsIndent;
            float detailWidth = width - detailsIndent;

            float wExercise = detailWidth * 0.64f;
            float wSets = detailWidth * 0.08f;
            float wReps = detailWidth * 0.14f;
            float wRest = detailWidth * 0.14f;

            if (line.Type == PrintLineType.DayHeader)
            {
                RectangleF rect = new RectangleF(x, y, width, 20);
                g.FillRectangle(Brushes.Gainsboro, rect);

                g.DrawString(line.Col1, _fontDay, Brushes.Black, x + 5, y + 2);
                y += 22;
                return;
            }

            if (line.Type == PrintLineType.MuscleHeader)
            {
                g.DrawString(line.Col1, _fontMuscle, Brushes.Black, x + 6, y + 2);
                y += 18;
                return;
            }

            if (line.Type == PrintLineType.ColumnHeader)
            {
                g.DrawString("Άσκηση", _fontLabel, Brushes.Black, detailX + 6, y + 2);
                g.DrawString("Σετ", _fontLabel, Brushes.Black, detailX + wExercise + 8, y + 2);
                g.DrawString("Επαναλήψεις", _fontLabel, Brushes.Black, detailX + wExercise + wSets + 8, y + 2);
                g.DrawString("Διάλειμμα", _fontLabel, Brushes.Black, detailX + wExercise + wSets + wReps + 8, y + 2); y += 18;
                
                return;
            }

            if (line.Type == PrintLineType.ExerciseRow)
            {
                StringFormat noWrapFormat = new StringFormat();
                noWrapFormat.FormatFlags = StringFormatFlags.NoWrap;
                noWrapFormat.Trimming = StringTrimming.EllipsisCharacter;

                g.DrawString(
                    line.Col1 ?? string.Empty,
                    _fontBody,
                    Brushes.Black,
                    new RectangleF(detailX + 6, y + 2, wExercise - 12, 16),
                    noWrapFormat);

                g.DrawString(
                    line.Col2 ?? string.Empty,
                    _fontBody,
                    Brushes.Black,
                    new RectangleF(detailX + wExercise + 8, y + 2, wSets - 10, 16),
                    noWrapFormat);

                g.DrawString(
                    line.Col3 ?? string.Empty,
                    _fontBody,
                    Brushes.Black,
                    new RectangleF(detailX + wExercise + wSets + 8, y + 2, wReps - 10, 16),
                    noWrapFormat);

                g.DrawString(
                    line.Col4 ?? string.Empty,
                    _fontBody,
                    Brushes.Black,
                    new RectangleF(detailX + wExercise + wSets + wReps + 8, y + 2, wRest - 10, 16),
                    noWrapFormat);

                y += 17;
                return;
            }

            y += 8;
        }
        private void DrawFooter(Graphics g, Rectangle bounds)
        {
            StringFormat right = new StringFormat();
            right.Alignment = StringAlignment.Far;

            g.DrawLine(Pens.Gray, bounds.Left, bounds.Bottom - 10, bounds.Right, bounds.Bottom - 10);
            g.DrawString(
                "Σελίδα " + _currentPage.ToString(),
                _fontFooter,
                Brushes.Black,
                new RectangleF(bounds.Left, bounds.Bottom - 8, bounds.Width, 18),
                right);
        }

        private class PrintLine
        {
            public PrintLineType Type { get; set; }
            public string Col1 { get; set; }
            public string Col2 { get; set; }
            public string Col3 { get; set; }
            public string Col4 { get; set; }

            public static PrintLine CreateDay(string value)
            {
                return new PrintLine { Type = PrintLineType.DayHeader, Col1 = value };
            }

            public static PrintLine CreateMuscle(string value)
            {
                return new PrintLine { Type = PrintLineType.MuscleHeader, Col1 = value };
            }

            public static PrintLine CreateColumns()
            {
                return new PrintLine { Type = PrintLineType.ColumnHeader };
            }

            public static PrintLine CreateExercise(string exercise, string sets, string reps, string rest)
            {
                return new PrintLine
                {
                    Type = PrintLineType.ExerciseRow,
                    Col1 = exercise,
                    Col2 = sets,
                    Col3 = reps,
                    Col4 = rest
                };
            }

            public static PrintLine CreateSpacer()
            {
                return new PrintLine { Type = PrintLineType.Spacer };
            }
        }

        private enum PrintLineType
        {
            DayHeader = 1,
            MuscleHeader = 2,
            ColumnHeader = 3,
            ExerciseRow = 4,
            Spacer = 5
        }
    }
}