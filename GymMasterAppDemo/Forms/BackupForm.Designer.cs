namespace GymMasterAppDemo.Forms
{
    partial class BackupForm
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
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pgbProgressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtDestinationBackup = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCreateBackup
            // 
            this.btnCreateBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateBackup.Location = new System.Drawing.Point(527, 224);
            this.btnCreateBackup.Name = "btnCreateBackup";
            this.btnCreateBackup.Size = new System.Drawing.Size(146, 50);
            this.btnCreateBackup.TabIndex = 0;
            this.btnCreateBackup.Text = " Δημιουργία";
            this.btnCreateBackup.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(679, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 50);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = " Ακύρωση";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pgbProgressBar
            // 
            this.pgbProgressBar.Location = new System.Drawing.Point(13, 170);
            this.pgbProgressBar.Name = "pgbProgressBar";
            this.pgbProgressBar.Size = new System.Drawing.Size(787, 30);
            this.pgbProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbProgressBar.TabIndex = 2;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(13, 142);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(75, 20);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "Προόδος:";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(812, 33);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Δημιουργία Αντιγράφου Ασφαλείας";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(13, 53);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(85, 20);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "Τοποθεσία:";
            // 
            // txtDestinationBackup
            // 
            this.txtDestinationBackup.Location = new System.Drawing.Point(17, 77);
            this.txtDestinationBackup.Name = "txtDestinationBackup";
            this.txtDestinationBackup.ReadOnly = true;
            this.txtDestinationBackup.Size = new System.Drawing.Size(783, 27);
            this.txtDestinationBackup.TabIndex = 6;
            // 
            // BackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(812, 293);
            this.Controls.Add(this.txtDestinationBackup);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pgbProgressBar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateBackup);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Icon = global::GymMasterAppDemo.Properties.Resources.GymMasterIcon;
            this.Name = "BackupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Αντίγραφο Ασφαλείας | GymMaster";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pgbProgressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtDestinationBackup;
    }
}