namespace GymMasterAppDemo.Forms
{
    partial class PaymentsMembershipForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerFullname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemainder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatusDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMembershipId = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.cmbMembershipType = new System.Windows.Forms.ComboBox();
            this.txtPaymentAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustomerFullname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCustomerId);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Στοιχεία Πελάτη ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(23, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Κωδικός:";
            // 
            // txtCustomerFullname
            // 
            this.txtCustomerFullname.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerFullname.Location = new System.Drawing.Point(96, 62);
            this.txtCustomerFullname.Name = "txtCustomerFullname";
            this.txtCustomerFullname.ReadOnly = true;
            this.txtCustomerFullname.Size = new System.Drawing.Size(324, 27);
            this.txtCustomerFullname.TabIndex = 2;
            this.txtCustomerFullname.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.Location = new System.Drawing.Point(23, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ον/μο:";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerId.Location = new System.Drawing.Point(96, 27);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.ReadOnly = true;
            this.txtCustomerId.Size = new System.Drawing.Size(324, 27);
            this.txtCustomerId.TabIndex = 0;
            this.txtCustomerId.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRemainder);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtStatusDesc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMembershipId);
            this.groupBox2.Location = new System.Drawing.Point(486, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 106);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Στοιχεία Συνδρομής";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "€";
            // 
            // txtRemainder
            // 
            this.txtRemainder.BackColor = System.Drawing.Color.Wheat;
            this.txtRemainder.Location = new System.Drawing.Point(415, 62);
            this.txtRemainder.Name = "txtRemainder";
            this.txtRemainder.ReadOnly = true;
            this.txtRemainder.Size = new System.Drawing.Size(105, 27);
            this.txtRemainder.TabIndex = 5;
            this.txtRemainder.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label5.Location = new System.Drawing.Point(335, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Υπόλοιπο:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label3.Location = new System.Drawing.Point(23, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Αριθμ. Συνδρομής:";
            // 
            // txtStatusDesc
            // 
            this.txtStatusDesc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtStatusDesc.Location = new System.Drawing.Point(161, 62);
            this.txtStatusDesc.Name = "txtStatusDesc";
            this.txtStatusDesc.ReadOnly = true;
            this.txtStatusDesc.Size = new System.Drawing.Size(159, 27);
            this.txtStatusDesc.TabIndex = 2;
            this.txtStatusDesc.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label4.Location = new System.Drawing.Point(23, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Κατάσταση:";
            // 
            // txtMembershipId
            // 
            this.txtMembershipId.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMembershipId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.txtMembershipId.Location = new System.Drawing.Point(161, 27);
            this.txtMembershipId.Name = "txtMembershipId";
            this.txtMembershipId.ReadOnly = true;
            this.txtMembershipId.Size = new System.Drawing.Size(159, 27);
            this.txtMembershipId.TabIndex = 0;
            this.txtMembershipId.TabStop = false;
            this.txtMembershipId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtComments);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtEndDate);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtStartDate);
            this.groupBox3.Controls.Add(this.cmbService);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cmbPaymentMethod);
            this.groupBox3.Controls.Add(this.cmbMembershipType);
            this.groupBox3.Controls.Add(this.txtPaymentAmount);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1071, 146);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Λεπτομέρειες";
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(161, 102);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(828, 27);
            this.txtComments.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label15.Location = new System.Drawing.Point(20, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 20);
            this.label15.TabIndex = 31;
            this.label15.Text = "Παρατηρήσεις:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label14.Location = new System.Drawing.Point(430, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 20);
            this.label14.TabIndex = 30;
            this.label14.Text = "Λήξη:";
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(496, 64);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(166, 27);
            this.txtEndDate.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label13.Location = new System.Drawing.Point(430, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 20);
            this.label13.TabIndex = 28;
            this.label13.Text = "Έναρξη:";
            // 
            // txtStartDate
            // 
            this.txtStartDate.Location = new System.Drawing.Point(496, 28);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.ReadOnly = true;
            this.txtStartDate.Size = new System.Drawing.Size(166, 27);
            this.txtStartDate.TabIndex = 27;
            // 
            // cmbService
            // 
            this.cmbService.FormattingEnabled = true;
            this.cmbService.Location = new System.Drawing.Point(161, 64);
            this.cmbService.Name = "cmbService";
            this.cmbService.Size = new System.Drawing.Size(199, 28);
            this.cmbService.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label8.Location = new System.Drawing.Point(20, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "Υπηρεσία:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(991, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "€";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label12.Location = new System.Drawing.Point(730, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Τρόπος Πληρωμής:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(871, 64);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(118, 28);
            this.cmbPaymentMethod.TabIndex = 4;
            // 
            // cmbMembershipType
            // 
            this.cmbMembershipType.FormattingEnabled = true;
            this.cmbMembershipType.Location = new System.Drawing.Point(161, 28);
            this.cmbMembershipType.Name = "cmbMembershipType";
            this.cmbMembershipType.Size = new System.Drawing.Size(199, 28);
            this.cmbMembershipType.TabIndex = 1;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Location = new System.Drawing.Point(871, 28);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Size = new System.Drawing.Size(118, 27);
            this.txtPaymentAmount.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label9.Location = new System.Drawing.Point(20, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "Τύπος Συνδρομής:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label10.Location = new System.Drawing.Point(730, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 20);
            this.label10.TabIndex = 1;
            this.label10.Text = "Ποσό Πληρωμής:";
            // 
            // dgvPayments
            // 
            this.dgvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvPayments.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Location = new System.Drawing.Point(12, 334);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.Size = new System.Drawing.Size(1071, 264);
            this.dgvPayments.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label11.Location = new System.Drawing.Point(12, 303);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "Κινήσεις Πληρωμών";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSalmon;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GymMasterAppDemo.Properties.Resources._1904654_cancel_close_cross_delete_reject_remove_stop_1225041;
            this.btnDelete.Location = new System.Drawing.Point(758, 605);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 42);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "    Διαγραφή";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.Gold;
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Image = global::GymMasterAppDemo.Properties.Resources._3643781_check_checklist_complete_done_mark_113406__1_;
            this.btnPayment.Location = new System.Drawing.Point(12, 605);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(137, 42);
            this.btnPayment.TabIndex = 7;
            this.btnPayment.Text = "  Εξόφληση";
            this.btnPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPayment.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SpringGreen;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::GymMasterAppDemo.Properties.Resources._1904659_arrow_backup_down_download_save_storage_transfer_122509;
            this.btnSave.Location = new System.Drawing.Point(582, 605);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 42);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "  Αποθήκευση";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::GymMasterAppDemo.Properties.Resources._1904666_calculate_close_delete_hide_minimize_minus_remove_122516;
            this.btnCancel.Location = new System.Drawing.Point(946, 605);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 42);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "   Κλείσιμο";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // PaymentsMembershipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 653);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "PaymentsMembershipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PaymentsMembershipForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerFullname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStatusDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMembershipId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemainder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.TextBox txtPaymentAmount;
        private System.Windows.Forms.ComboBox cmbMembershipType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.ComboBox cmbService;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnDelete;
    }
}