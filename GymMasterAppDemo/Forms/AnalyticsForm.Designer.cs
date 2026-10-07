namespace GymMasterAppDemo.Views
{
    partial class AnalyticsForm
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
            this.tabAnalytics = new System.Windows.Forms.TabControl();
            this.tabRecentCustomers = new System.Windows.Forms.TabPage();
            this.lblNumNewCustomers = new System.Windows.Forms.Label();
            this.dgvRecentCustomers = new System.Windows.Forms.DataGridView();
            this.tabExpiringToday = new System.Windows.Forms.TabPage();
            this.lblExpiredToday = new System.Windows.Forms.Label();
            this.dgvExpiringToday = new System.Windows.Forms.DataGridView();
            this.tabUnpaidMemberships = new System.Windows.Forms.TabPage();
            this.lblNumOfUnpaid = new System.Windows.Forms.Label();
            this.dgvUnpaidMemberships = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabAnalytics.SuspendLayout();
            this.tabRecentCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentCustomers)).BeginInit();
            this.tabExpiringToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiringToday)).BeginInit();
            this.tabUnpaidMemberships.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnpaidMemberships)).BeginInit();
            this.SuspendLayout();
            // 
            // tabAnalytics
            // 
            this.tabAnalytics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAnalytics.Controls.Add(this.tabRecentCustomers);
            this.tabAnalytics.Controls.Add(this.tabExpiringToday);
            this.tabAnalytics.Controls.Add(this.tabUnpaidMemberships);
            this.tabAnalytics.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tabAnalytics.Location = new System.Drawing.Point(10, 109);
            this.tabAnalytics.Name = "tabAnalytics";
            this.tabAnalytics.SelectedIndex = 0;
            this.tabAnalytics.Size = new System.Drawing.Size(978, 475);
            this.tabAnalytics.TabIndex = 8;
            // 
            // tabRecentCustomers
            // 
            this.tabRecentCustomers.Controls.Add(this.lblNumNewCustomers);
            this.tabRecentCustomers.Controls.Add(this.dgvRecentCustomers);
            this.tabRecentCustomers.Location = new System.Drawing.Point(4, 29);
            this.tabRecentCustomers.Name = "tabRecentCustomers";
            this.tabRecentCustomers.Padding = new System.Windows.Forms.Padding(3);
            this.tabRecentCustomers.Size = new System.Drawing.Size(970, 442);
            this.tabRecentCustomers.TabIndex = 0;
            this.tabRecentCustomers.Text = " Νέες Εγγραφές";
            this.tabRecentCustomers.UseVisualStyleBackColor = true;
            // 
            // lblNumNewCustomers
            // 
            this.lblNumNewCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumNewCustomers.AutoSize = true;
            this.lblNumNewCustomers.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNewCustomers.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNumNewCustomers.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNumNewCustomers.Location = new System.Drawing.Point(10, 408);
            this.lblNumNewCustomers.Name = "lblNumNewCustomers";
            this.lblNumNewCustomers.Size = new System.Drawing.Size(14, 23);
            this.lblNumNewCustomers.TabIndex = 2;
            this.lblNumNewCustomers.Text = " ";
            // 
            // dgvRecentCustomers
            // 
            this.dgvRecentCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentCustomers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvRecentCustomers.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvRecentCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentCustomers.Location = new System.Drawing.Point(6, 12);
            this.dgvRecentCustomers.Name = "dgvRecentCustomers";
            this.dgvRecentCustomers.ReadOnly = true;
            this.dgvRecentCustomers.Size = new System.Drawing.Size(955, 389);
            this.dgvRecentCustomers.TabIndex = 0;
            // 
            // tabExpiringToday
            // 
            this.tabExpiringToday.Controls.Add(this.lblExpiredToday);
            this.tabExpiringToday.Controls.Add(this.dgvExpiringToday);
            this.tabExpiringToday.Location = new System.Drawing.Point(4, 29);
            this.tabExpiringToday.Name = "tabExpiringToday";
            this.tabExpiringToday.Padding = new System.Windows.Forms.Padding(3);
            this.tabExpiringToday.Size = new System.Drawing.Size(970, 442);
            this.tabExpiringToday.TabIndex = 1;
            this.tabExpiringToday.Text = " Λήγουν Σήμερα";
            this.tabExpiringToday.UseVisualStyleBackColor = true;
            // 
            // lblExpiredToday
            // 
            this.lblExpiredToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExpiredToday.AutoSize = true;
            this.lblExpiredToday.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiredToday.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblExpiredToday.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblExpiredToday.Location = new System.Drawing.Point(8, 408);
            this.lblExpiredToday.Name = "lblExpiredToday";
            this.lblExpiredToday.Size = new System.Drawing.Size(14, 23);
            this.lblExpiredToday.TabIndex = 3;
            this.lblExpiredToday.Text = " ";
            // 
            // dgvExpiringToday
            // 
            this.dgvExpiringToday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExpiringToday.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpiringToday.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvExpiringToday.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvExpiringToday.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExpiringToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpiringToday.Location = new System.Drawing.Point(8, 11);
            this.dgvExpiringToday.Name = "dgvExpiringToday";
            this.dgvExpiringToday.ReadOnly = true;
            this.dgvExpiringToday.Size = new System.Drawing.Size(955, 389);
            this.dgvExpiringToday.TabIndex = 1;
            // 
            // tabUnpaidMemberships
            // 
            this.tabUnpaidMemberships.Controls.Add(this.lblNumOfUnpaid);
            this.tabUnpaidMemberships.Controls.Add(this.dgvUnpaidMemberships);
            this.tabUnpaidMemberships.Location = new System.Drawing.Point(4, 29);
            this.tabUnpaidMemberships.Name = "tabUnpaidMemberships";
            this.tabUnpaidMemberships.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnpaidMemberships.Size = new System.Drawing.Size(970, 442);
            this.tabUnpaidMemberships.TabIndex = 2;
            this.tabUnpaidMemberships.Text = " Ανεξόφλητες Συνδρομές";
            this.tabUnpaidMemberships.UseVisualStyleBackColor = true;
            // 
            // lblNumOfUnpaid
            // 
            this.lblNumOfUnpaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumOfUnpaid.AutoSize = true;
            this.lblNumOfUnpaid.BackColor = System.Drawing.Color.Transparent;
            this.lblNumOfUnpaid.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNumOfUnpaid.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNumOfUnpaid.Location = new System.Drawing.Point(8, 407);
            this.lblNumOfUnpaid.Name = "lblNumOfUnpaid";
            this.lblNumOfUnpaid.Size = new System.Drawing.Size(14, 23);
            this.lblNumOfUnpaid.TabIndex = 3;
            this.lblNumOfUnpaid.Text = " ";
            // 
            // dgvUnpaidMemberships
            // 
            this.dgvUnpaidMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUnpaidMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnpaidMemberships.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvUnpaidMemberships.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvUnpaidMemberships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUnpaidMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnpaidMemberships.Location = new System.Drawing.Point(8, 12);
            this.dgvUnpaidMemberships.Name = "dgvUnpaidMemberships";
            this.dgvUnpaidMemberships.ReadOnly = true;
            this.dgvUnpaidMemberships.Size = new System.Drawing.Size(955, 389);
            this.dgvUnpaidMemberships.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(255, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Αναλυτικές Αναφορές";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.LightSalmon;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::GymMasterAppDemo.Properties.Resources.cancel_close_delete_exit_logout_remove_x_icon_123217;
            this.btnClose.Location = new System.Drawing.Point(955, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightSkyBlue;
            this.btnExportExcel.FlatAppearance.BorderSize = 0;
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Image = global::GymMasterAppDemo.Properties.Resources.excel_office_4658;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(798, 61);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(183, 42);
            this.btnExportExcel.TabIndex = 10;
            this.btnExportExcel.Text = "   Εξαγωγή σε Excel";
            this.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightSkyBlue;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::GymMasterAppDemo.Properties.Resources.refreshpageaction_114520;
            this.btnRefresh.Location = new System.Drawing.Point(630, 61);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(146, 42);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "  Ανανέωση";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(999, 597);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabAnalytics);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnalyticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aναφορές | GymMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AnalyticsForm_Load);
            this.tabAnalytics.ResumeLayout(false);
            this.tabRecentCustomers.ResumeLayout(false);
            this.tabRecentCustomers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentCustomers)).EndInit();
            this.tabExpiringToday.ResumeLayout(false);
            this.tabExpiringToday.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiringToday)).EndInit();
            this.tabUnpaidMemberships.ResumeLayout(false);
            this.tabUnpaidMemberships.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnpaidMemberships)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabAnalytics;
        private System.Windows.Forms.TabPage tabRecentCustomers;
        private System.Windows.Forms.TabPage tabExpiringToday;
        private System.Windows.Forms.TabPage tabUnpaidMemberships;
        private System.Windows.Forms.DataGridView dgvRecentCustomers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Label lblNumNewCustomers;
        private System.Windows.Forms.Label lblExpiredToday;
        private System.Windows.Forms.DataGridView dgvExpiringToday;
        private System.Windows.Forms.Label lblNumOfUnpaid;
        private System.Windows.Forms.DataGridView dgvUnpaidMemberships;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
    }
}