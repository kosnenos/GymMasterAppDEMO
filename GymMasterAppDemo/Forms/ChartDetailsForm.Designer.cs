namespace GymMasterAppDemo.Forms
{
    partial class ChartDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlBackCol = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlGridButtons = new System.Windows.Forms.TableLayoutPanel();
            this.dgvChartData = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportJPG = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBackCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.pnlGridButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChartData)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBackCol
            // 
            this.pnlBackCol.ColumnCount = 2;
            this.pnlBackCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.78798F));
            this.pnlBackCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.21202F));
            this.pnlBackCol.Controls.Add(this.chart1, 0, 0);
            this.pnlBackCol.Controls.Add(this.pnlGridButtons, 1, 0);
            this.pnlBackCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackCol.Location = new System.Drawing.Point(0, 31);
            this.pnlBackCol.Name = "pnlBackCol";
            this.pnlBackCol.RowCount = 1;
            this.pnlBackCol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlBackCol.Size = new System.Drawing.Size(1264, 650);
            this.pnlBackCol.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(775, 644);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // pnlGridButtons
            // 
            this.pnlGridButtons.ColumnCount = 1;
            this.pnlGridButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlGridButtons.Controls.Add(this.dgvChartData, 0, 0);
            this.pnlGridButtons.Controls.Add(this.pnlButtons, 0, 1);
            this.pnlGridButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridButtons.Location = new System.Drawing.Point(784, 3);
            this.pnlGridButtons.Name = "pnlGridButtons";
            this.pnlGridButtons.RowCount = 2;
            this.pnlGridButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.04348F));
            this.pnlGridButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.95652F));
            this.pnlGridButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlGridButtons.Size = new System.Drawing.Size(477, 644);
            this.pnlGridButtons.TabIndex = 4;
            // 
            // dgvChartData
            // 
            this.dgvChartData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChartData.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvChartData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChartData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChartData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvChartData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChartData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChartData.Location = new System.Drawing.Point(3, 3);
            this.dgvChartData.MultiSelect = false;
            this.dgvChartData.Name = "dgvChartData";
            this.dgvChartData.RowTemplate.Height = 30;
            this.dgvChartData.Size = new System.Drawing.Size(471, 400);
            this.dgvChartData.TabIndex = 5;
            // 
            // pnlButtons
            // 
            this.pnlButtons.ColumnCount = 1;
            this.pnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButtons.Controls.Add(this.btnClose, 0, 3);
            this.pnlButtons.Controls.Add(this.btnExcelExport, 0, 2);
            this.pnlButtons.Controls.Add(this.btnPrint, 0, 0);
            this.pnlButtons.Controls.Add(this.btnExportJPG, 0, 1);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(3, 409);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.RowCount = 4;
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlButtons.Size = new System.Drawing.Size(471, 232);
            this.pnlButtons.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSalmon;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(3, 177);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(465, 52);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Κλείσιμο";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.BackColor = System.Drawing.Color.LightGreen;
            this.btnExcelExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExcelExport.FlatAppearance.BorderSize = 0;
            this.btnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.btnExcelExport.Location = new System.Drawing.Point(3, 119);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(465, 52);
            this.btnExcelExport.TabIndex = 1;
            this.btnExcelExport.Text = " Εξαγωγή στο MS Excel";
            this.btnExcelExport.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Lime;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.btnPrint.Location = new System.Drawing.Point(3, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(465, 52);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = " Εκτύπωση";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnExportJPG
            // 
            this.btnExportJPG.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportJPG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportJPG.FlatAppearance.BorderSize = 0;
            this.btnExportJPG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportJPG.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.btnExportJPG.Location = new System.Drawing.Point(3, 61);
            this.btnExportJPG.Name = "btnExportJPG";
            this.btnExportJPG.Size = new System.Drawing.Size(465, 52);
            this.btnExportJPG.TabIndex = 2;
            this.btnExportJPG.Text = " Εξαγωγή σε JPG";
            this.btnExportJPG.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1264, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Προεπισκόπηση γραφήματος";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChartDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.pnlBackCol);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Icon = global::GymMasterAppDemo.Properties.Resources.GymMasterIcon;
            this.Name = "ChartDetailsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChartDetailsForm";
            this.pnlBackCol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.pnlGridButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChartData)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlBackCol;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel pnlGridButtons;
        private System.Windows.Forms.DataGridView dgvChartData;
        private System.Windows.Forms.TableLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Button btnExportJPG;
        private System.Windows.Forms.Button btnClose;
    }
}