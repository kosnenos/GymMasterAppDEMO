namespace GymMasterAppDemo.Forms
{
    partial class MembershipForm
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.lblPelaltes = new System.Windows.Forms.Label();
            this.lblSindromes = new System.Windows.Forms.Label();
            this.dgvMemberships = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 84);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(402, 27);
            this.txtSearch.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 61);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(265, 20);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Πληκ/στε Κωδικό ή Επώνυμο Πελάτη:";
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(12, 154);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.RowTemplate.Height = 30;
            this.dgvCustomers.Size = new System.Drawing.Size(1002, 199);
            this.dgvCustomers.TabIndex = 4;
            // 
            // lblPelaltes
            // 
            this.lblPelaltes.AutoSize = true;
            this.lblPelaltes.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblPelaltes.Location = new System.Drawing.Point(12, 126);
            this.lblPelaltes.Name = "lblPelaltes";
            this.lblPelaltes.Size = new System.Drawing.Size(74, 21);
            this.lblPelaltes.TabIndex = 5;
            this.lblPelaltes.Text = "Πελάτες:";
            // 
            // lblSindromes
            // 
            this.lblSindromes.AutoSize = true;
            this.lblSindromes.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblSindromes.Location = new System.Drawing.Point(12, 381);
            this.lblSindromes.Name = "lblSindromes";
            this.lblSindromes.Size = new System.Drawing.Size(96, 21);
            this.lblSindromes.TabIndex = 6;
            this.lblSindromes.Text = "Συνδρομές:";
            // 
            // dgvMemberships
            // 
            this.dgvMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMemberships.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvMemberships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMemberships.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMemberships.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemberships.Location = new System.Drawing.Point(12, 409);
            this.dgvMemberships.MultiSelect = false;
            this.dgvMemberships.Name = "dgvMemberships";
            this.dgvMemberships.RowTemplate.Height = 30;
            this.dgvMemberships.Size = new System.Drawing.Size(1004, 141);
            this.dgvMemberships.TabIndex = 7;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 37);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Συνδρομές Πελατών";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::GymMasterAppDemo.Properties.Resources.cancel_close_delete_exit_logout_remove_x_icon_123217;
            this.btnClose.Location = new System.Drawing.Point(983, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = " ";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GymMasterAppDemo.Properties.Resources._1904654_cancel_close_cross_delete_reject_remove_stop_122504;
            this.btnDelete.Location = new System.Drawing.Point(979, 366);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(35, 37);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = " ";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnPayments
            // 
            this.btnPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPayments.BackColor = System.Drawing.Color.LawnGreen;
            this.btnPayments.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.Image = global::GymMasterAppDemo.Properties.Resources._1904675_configuration_edit_gear_options_preferences_setting_settings_122525;
            this.btnPayments.Location = new System.Drawing.Point(883, 366);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Size = new System.Drawing.Size(35, 37);
            this.btnPayments.TabIndex = 9;
            this.btnPayments.Text = " ";
            this.btnPayments.UseVisualStyleBackColor = false;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.BackColor = System.Drawing.Color.SkyBlue;
            this.btnAddNew.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnAddNew.FlatAppearance.BorderSize = 0;
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Image = global::GymMasterAppDemo.Properties.Resources._1904677_add_addition_calculate_charge_create_new_plus_122527;
            this.btnAddNew.Location = new System.Drawing.Point(838, 366);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(35, 37);
            this.btnAddNew.TabIndex = 8;
            this.btnAddNew.Text = " ";
            this.btnAddNew.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::GymMasterAppDemo.Properties.Resources.refresh_arrow_icon_131504;
            this.btnRefresh.Location = new System.Drawing.Point(464, 80);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = " ";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = global::GymMasterAppDemo.Properties.Resources.search_find_locate_icon_131498;
            this.btnSearch.Location = new System.Drawing.Point(420, 82);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = " ";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.PaleGreen;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::GymMasterAppDemo.Properties.Resources.printer_outlined_symbol_of_the_tool_icon_icons_com_57775;
            this.btnPrint.Location = new System.Drawing.Point(928, 366);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(35, 37);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = " ";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // MembershipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1030, 563);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPayments);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.dgvMemberships);
            this.Controls.Add(this.lblSindromes);
            this.Controls.Add(this.lblPelaltes);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MembershipForm";
            this.Text = "Συνδρομές Πελατών | GymMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Label lblPelaltes;
        private System.Windows.Forms.Label lblSindromes;
        private System.Windows.Forms.DataGridView dgvMemberships;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPrint;
    }
}