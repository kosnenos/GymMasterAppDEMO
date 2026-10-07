namespace GymMasterAppDemo.Forms
{
    partial class WorkoutProgramEditForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProgramId = new System.Windows.Forms.Label();
            this.txtProgramId = new System.Windows.Forms.TextBox();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.txtCustomerFullname = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.lblGoal = new System.Windows.Forms.Label();
            this.cboGoal = new System.Windows.Forms.ComboBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.dgvWorkoutProgramDetails = new System.Windows.Forms.DataGridView();
            this.lblExercises = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkoutProgramDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProgramId);
            this.groupBox1.Controls.Add(this.txtProgramId);
            this.groupBox1.Controls.Add(this.lblCustomerId);
            this.groupBox1.Controls.Add(this.txtCustomerFullname);
            this.groupBox1.Controls.Add(this.lblCustomer);
            this.groupBox1.Controls.Add(this.txtCustomerId);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Στοιχεία Προγρ/τος Πελάτη ";
            // 
            // lblProgramId
            // 
            this.lblProgramId.AutoSize = true;
            this.lblProgramId.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblProgramId.Location = new System.Drawing.Point(23, 67);
            this.lblProgramId.Name = "lblProgramId";
            this.lblProgramId.Size = new System.Drawing.Size(138, 20);
            this.lblProgramId.TabIndex = 3;
            this.lblProgramId.Text = "Αριθμ. Προγρ/τος:";
            // 
            // txtProgramId
            // 
            this.txtProgramId.BackColor = System.Drawing.Color.AliceBlue;
            this.txtProgramId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.txtProgramId.Location = new System.Drawing.Point(167, 64);
            this.txtProgramId.Name = "txtProgramId";
            this.txtProgramId.ReadOnly = true;
            this.txtProgramId.Size = new System.Drawing.Size(109, 27);
            this.txtProgramId.TabIndex = 0;
            this.txtProgramId.TabStop = false;
            this.txtProgramId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblCustomerId.Location = new System.Drawing.Point(23, 33);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(103, 20);
            this.lblCustomerId.TabIndex = 3;
            this.lblCustomerId.Text = "Κωδ. Πελάτη:";
            // 
            // txtCustomerFullname
            // 
            this.txtCustomerFullname.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerFullname.Location = new System.Drawing.Point(340, 27);
            this.txtCustomerFullname.Name = "txtCustomerFullname";
            this.txtCustomerFullname.ReadOnly = true;
            this.txtCustomerFullname.Size = new System.Drawing.Size(386, 27);
            this.txtCustomerFullname.TabIndex = 2;
            this.txtCustomerFullname.TabStop = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblCustomer.Location = new System.Drawing.Point(282, 32);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(56, 20);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Ον/μο:";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerId.Location = new System.Drawing.Point(167, 27);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.ReadOnly = true;
            this.txtCustomerId.Size = new System.Drawing.Size(109, 27);
            this.txtCustomerId.TabIndex = 0;
            this.txtCustomerId.TabStop = false;
            // 
            // lblGoal
            // 
            this.lblGoal.AutoSize = true;
            this.lblGoal.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblGoal.Location = new System.Drawing.Point(12, 133);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(62, 20);
            this.lblGoal.TabIndex = 1;
            this.lblGoal.Text = "Στόχος:";
            // 
            // cboGoal
            // 
            this.cboGoal.FormattingEnabled = true;
            this.cboGoal.Location = new System.Drawing.Point(179, 129);
            this.cboGoal.Name = "cboGoal";
            this.cboGoal.Size = new System.Drawing.Size(203, 28);
            this.cboGoal.TabIndex = 0;
            // 
            // txtDuration
            // 
            this.txtDuration.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuration.Location = new System.Drawing.Point(179, 198);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(203, 27);
            this.txtDuration.TabIndex = 2;
            this.txtDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblDuration.Location = new System.Drawing.Point(12, 201);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(163, 20);
            this.lblDuration.TabIndex = 17;
            this.lblDuration.Text = "Διάρκεια (Εβδομάδες):";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(179, 164);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(203, 27);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblStartDate.Location = new System.Drawing.Point(12, 167);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(122, 20);
            this.lblStartDate.TabIndex = 18;
            this.lblStartDate.Text = "Ημ/νια Έναρξης:";
            // 
            // txtEndDate
            // 
            this.txtEndDate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtEndDate.Location = new System.Drawing.Point(497, 202);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(139, 27);
            this.txtEndDate.TabIndex = 15;
            this.txtEndDate.TabStop = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblEndDate.Location = new System.Drawing.Point(385, 205);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(106, 20);
            this.lblEndDate.TabIndex = 19;
            this.lblEndDate.Text = "Ημ/νια Λήξης:";
            // 
            // txtFrequency
            // 
            this.txtFrequency.BackColor = System.Drawing.SystemColors.Window;
            this.txtFrequency.Location = new System.Drawing.Point(179, 232);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(203, 27);
            this.txtFrequency.TabIndex = 3;
            this.txtFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblFrequency.Location = new System.Drawing.Point(12, 235);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(153, 20);
            this.lblFrequency.TabIndex = 21;
            this.lblFrequency.Text = "Συχνότητα (Ημέρες):";
            // 
            // dgvWorkoutProgramDetails
            // 
            this.dgvWorkoutProgramDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkoutProgramDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWorkoutProgramDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvWorkoutProgramDetails.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvWorkoutProgramDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWorkoutProgramDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkoutProgramDetails.Location = new System.Drawing.Point(16, 345);
            this.dgvWorkoutProgramDetails.Name = "dgvWorkoutProgramDetails";
            this.dgvWorkoutProgramDetails.Size = new System.Drawing.Size(1054, 243);
            this.dgvWorkoutProgramDetails.TabIndex = 5;
            // 
            // lblExercises
            // 
            this.lblExercises.AutoSize = true;
            this.lblExercises.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblExercises.Location = new System.Drawing.Point(12, 315);
            this.lblExercises.Name = "lblExercises";
            this.lblExercises.Size = new System.Drawing.Size(76, 20);
            this.lblExercises.TabIndex = 23;
            this.lblExercises.Text = "Ασκήσεις:";
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.SystemColors.Window;
            this.txtComments.Location = new System.Drawing.Point(179, 265);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(891, 27);
            this.txtComments.TabIndex = 4;
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblComments.Location = new System.Drawing.Point(12, 268);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(113, 20);
            this.lblComments.TabIndex = 25;
            this.lblComments.Text = "Παρατηρήσεις:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::GymMasterAppDemo.Properties.Resources._1904666_calculate_close_delete_hide_minimize_minus_remove_122516;
            this.btnCancel.Location = new System.Drawing.Point(930, 604);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "    Κλείσιμο";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SpringGreen;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::GymMasterAppDemo.Properties.Resources._1904659_arrow_backup_down_download_save_storage_transfer_122509;
            this.btnSave.Location = new System.Drawing.Point(751, 604);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(166, 42);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "    Αποθήκευση";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // WorkoutProgramEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 658);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblExercises);
            this.Controls.Add(this.dgvWorkoutProgramDetails);
            this.Controls.Add(this.txtFrequency);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.cboGoal);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.lblGoal);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.txtEndDate);
            this.Controls.Add(this.lblEndDate);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "WorkoutProgramEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkoutProgramEditForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkoutProgramDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.TextBox txtCustomerFullname;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblProgramId;
        private System.Windows.Forms.Label lblGoal;
        private System.Windows.Forms.TextBox txtProgramId;
        private System.Windows.Forms.ComboBox cboGoal;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.DataGridView dgvWorkoutProgramDetails;
        private System.Windows.Forms.Label lblExercises;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}