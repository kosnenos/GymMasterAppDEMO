namespace GymMasterAppDemo.Forms
{
    partial class WorkoutProgramForm
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEditUpdate = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.dgvWorkoutPrograms = new System.Windows.Forms.DataGridView();
            this.lblProgrammata = new System.Windows.Forms.Label();
            this.lblPelaltes = new System.Windows.Forms.Label();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkoutPrograms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.PaleGreen;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::GymMasterAppDemo.Properties.Resources.printer_outlined_symbol_of_the_tool_icon_icons_com_57775;
            this.btnPrint.Location = new System.Drawing.Point(919, 387);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(35, 37);
            this.btnPrint.TabIndex = 27;
            this.btnPrint.Text = " ";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(423, 37);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Ατομικά Προγράμματα Άθλησης";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::GymMasterAppDemo.Properties.Resources.cancel_close_delete_exit_logout_remove_x_icon_123217;
            this.btnClose.Location = new System.Drawing.Point(976, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 25;
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
            this.btnDelete.Location = new System.Drawing.Point(969, 387);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(35, 37);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = " ";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEditUpdate
            // 
            this.btnEditUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUpdate.BackColor = System.Drawing.Color.LawnGreen;
            this.btnEditUpdate.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnEditUpdate.FlatAppearance.BorderSize = 0;
            this.btnEditUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditUpdate.Image = global::GymMasterAppDemo.Properties.Resources.interface_edit_pencil_writing_school_icon_133013;
            this.btnEditUpdate.Location = new System.Drawing.Point(876, 387);
            this.btnEditUpdate.Name = "btnEditUpdate";
            this.btnEditUpdate.Size = new System.Drawing.Size(35, 37);
            this.btnEditUpdate.TabIndex = 23;
            this.btnEditUpdate.Text = " ";
            this.btnEditUpdate.UseVisualStyleBackColor = false;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.BackColor = System.Drawing.Color.SkyBlue;
            this.btnAddNew.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.btnAddNew.FlatAppearance.BorderSize = 0;
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Image = global::GymMasterAppDemo.Properties.Resources._1904677_add_addition_calculate_charge_create_new_plus_122527;
            this.btnAddNew.Location = new System.Drawing.Point(833, 387);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(35, 37);
            this.btnAddNew.TabIndex = 22;
            this.btnAddNew.Text = " ";
            this.btnAddNew.UseVisualStyleBackColor = false;
            // 
            // dgvWorkoutPrograms
            // 
            this.dgvWorkoutPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkoutPrograms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWorkoutPrograms.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvWorkoutPrograms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWorkoutPrograms.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvWorkoutPrograms.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvWorkoutPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkoutPrograms.Location = new System.Drawing.Point(9, 430);
            this.dgvWorkoutPrograms.MultiSelect = false;
            this.dgvWorkoutPrograms.Name = "dgvWorkoutPrograms";
            this.dgvWorkoutPrograms.RowTemplate.Height = 30;
            this.dgvWorkoutPrograms.Size = new System.Drawing.Size(997, 141);
            this.dgvWorkoutPrograms.TabIndex = 21;
            // 
            // lblProgrammata
            // 
            this.lblProgrammata.AutoSize = true;
            this.lblProgrammata.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblProgrammata.Location = new System.Drawing.Point(9, 402);
            this.lblProgrammata.Name = "lblProgrammata";
            this.lblProgrammata.Size = new System.Drawing.Size(189, 21);
            this.lblProgrammata.TabIndex = 20;
            this.lblProgrammata.Text = "Ατομικά Προγράμματα:";
            // 
            // lblPelaltes
            // 
            this.lblPelaltes.AutoSize = true;
            this.lblPelaltes.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblPelaltes.Location = new System.Drawing.Point(9, 147);
            this.lblPelaltes.Name = "lblPelaltes";
            this.lblPelaltes.Size = new System.Drawing.Size(74, 21);
            this.lblPelaltes.TabIndex = 19;
            this.lblPelaltes.Text = "Πελάτες:";
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
            this.dgvCustomers.Location = new System.Drawing.Point(9, 175);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.RowTemplate.Height = 30;
            this.dgvCustomers.Size = new System.Drawing.Size(995, 199);
            this.dgvCustomers.TabIndex = 18;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::GymMasterAppDemo.Properties.Resources.refresh_arrow_icon_131504;
            this.btnRefresh.Location = new System.Drawing.Point(461, 101);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = " ";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = global::GymMasterAppDemo.Properties.Resources.search_find_locate_icon_131498;
            this.btnSearch.Location = new System.Drawing.Point(417, 103);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 30);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = " ";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(9, 82);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(265, 20);
            this.lblSearch.TabIndex = 15;
            this.lblSearch.Text = "Πληκ/στε Κωδικό ή Επώνυμο Πελάτη:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(9, 105);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(402, 27);
            this.txtSearch.TabIndex = 14;
            // 
            // WorkoutProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 593);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEditUpdate);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.dgvWorkoutPrograms);
            this.Controls.Add(this.lblProgrammata);
            this.Controls.Add(this.lblPelaltes);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WorkoutProgramForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkoutProgramForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkoutPrograms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEditUpdate;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView dgvWorkoutPrograms;
        private System.Windows.Forms.Label lblProgrammata;
        private System.Windows.Forms.Label lblPelaltes;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
    }
}