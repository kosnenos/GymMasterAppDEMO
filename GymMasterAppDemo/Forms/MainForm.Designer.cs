namespace GymMasterAppDemo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslFormName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslManager = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslCopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.btnBackupDBAll = new System.Windows.Forms.Button();
            this.btnExitApp = new System.Windows.Forms.Button();
            this.btnAnalytics = new System.Windows.Forms.Button();
            this.btnWorksOuts = new System.Windows.Forms.Button();
            this.btnMemberships = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslDb,
            this.tslFormName,
            this.tslManager,
            this.tslCopyright});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(928, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslDb
            // 
            this.tslDb.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslDb.Name = "tslDb";
            this.tslDb.Size = new System.Drawing.Size(228, 19);
            this.tslDb.Spring = true;
            this.tslDb.Text = "toolStripStatusLabel1";
            this.tslDb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslFormName
            // 
            this.tslFormName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslFormName.Name = "tslFormName";
            this.tslFormName.Size = new System.Drawing.Size(228, 19);
            this.tslFormName.Spring = true;
            this.tslFormName.Text = "toolStripStatusLabel2";
            // 
            // tslManager
            // 
            this.tslManager.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslManager.Name = "tslManager";
            this.tslManager.Size = new System.Drawing.Size(228, 19);
            this.tslManager.Spring = true;
            this.tslManager.Text = "toolStripStatusLabel3";
            // 
            // tslCopyright
            // 
            this.tslCopyright.AutoSize = false;
            this.tslCopyright.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tslCopyright.Name = "tslCopyright";
            this.tslCopyright.Size = new System.Drawing.Size(228, 19);
            this.tslCopyright.Spring = true;
            this.tslCopyright.Text = "toolStripStatusLabel4";
            this.tslCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.IsSplitterFixed = true;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.splitMain.Panel1.Controls.Add(this.btnBackupDBAll);
            this.splitMain.Panel1.Controls.Add(this.btnExitApp);
            this.splitMain.Panel1.Controls.Add(this.btnAnalytics);
            this.splitMain.Panel1.Controls.Add(this.btnWorksOuts);
            this.splitMain.Panel1.Controls.Add(this.btnMemberships);
            this.splitMain.Panel1.Controls.Add(this.btnCustomers);
            this.splitMain.Panel1.Controls.Add(this.btnHome);
            this.splitMain.Panel1.Controls.Add(this.pictureBoxLogo);
            this.splitMain.Panel1MinSize = 200;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitMain.Panel2.Controls.Add(this.pnlContent);
            this.splitMain.Size = new System.Drawing.Size(928, 551);
            this.splitMain.SplitterDistance = 200;
            this.splitMain.TabIndex = 5;
            // 
            // btnBackupDBAll
            // 
            this.btnBackupDBAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBackupDBAll.FlatAppearance.BorderSize = 0;
            this.btnBackupDBAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupDBAll.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnBackupDBAll.Image = global::GymMasterAppDemo.Properties.Resources._1904659_arrow_backup_down_download_save_storage_transfer_122509__1_;
            this.btnBackupDBAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackupDBAll.Location = new System.Drawing.Point(0, 441);
            this.btnBackupDBAll.Name = "btnBackupDBAll";
            this.btnBackupDBAll.Size = new System.Drawing.Size(200, 55);
            this.btnBackupDBAll.TabIndex = 5;
            this.btnBackupDBAll.Text = "    Backup";
            this.btnBackupDBAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackupDBAll.UseVisualStyleBackColor = true;
            this.btnBackupDBAll.Click += new System.EventHandler(this.btnBackupDBAll_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExitApp.FlatAppearance.BorderSize = 0;
            this.btnExitApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExitApp.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnExitApp.Image = global::GymMasterAppDemo.Properties.Resources.power_off_on_icon_152197;
            this.btnExitApp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExitApp.Location = new System.Drawing.Point(0, 496);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(200, 55);
            this.btnExitApp.TabIndex = 4;
            this.btnExitApp.Text = "    Έξοδος";
            this.btnExitApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExitApp.UseVisualStyleBackColor = true;
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // btnAnalytics
            // 
            this.btnAnalytics.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAnalytics.FlatAppearance.BorderSize = 0;
            this.btnAnalytics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalytics.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnAnalytics.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnAnalytics.Image = global::GymMasterAppDemo.Properties.Resources.setting_balance_equalizer_icon_152217;
            this.btnAnalytics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalytics.Location = new System.Drawing.Point(0, 319);
            this.btnAnalytics.Name = "btnAnalytics";
            this.btnAnalytics.Size = new System.Drawing.Size(200, 55);
            this.btnAnalytics.TabIndex = 3;
            this.btnAnalytics.Text = "    Αναλυτικά";
            this.btnAnalytics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnalytics.UseVisualStyleBackColor = true;
            this.btnAnalytics.Click += new System.EventHandler(this.btnAnalytics_Click);
            // 
            // btnWorksOuts
            // 
            this.btnWorksOuts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWorksOuts.FlatAppearance.BorderSize = 0;
            this.btnWorksOuts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorksOuts.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnWorksOuts.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnWorksOuts.Image = global::GymMasterAppDemo.Properties.Resources.dumbbell_gym_icon_124413__2_;
            this.btnWorksOuts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWorksOuts.Location = new System.Drawing.Point(0, 264);
            this.btnWorksOuts.Name = "btnWorksOuts";
            this.btnWorksOuts.Size = new System.Drawing.Size(200, 55);
            this.btnWorksOuts.TabIndex = 5;
            this.btnWorksOuts.Text = "    Προγράμματα";
            this.btnWorksOuts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnWorksOuts.UseVisualStyleBackColor = true;
            this.btnWorksOuts.Click += new System.EventHandler(this.btnWorksOuts_Click);
            // 
            // btnMemberships
            // 
            this.btnMemberships.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMemberships.FlatAppearance.BorderSize = 0;
            this.btnMemberships.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemberships.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMemberships.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnMemberships.Image = global::GymMasterAppDemo.Properties.Resources.folder_archive_icon_152198;
            this.btnMemberships.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemberships.Location = new System.Drawing.Point(0, 209);
            this.btnMemberships.Name = "btnMemberships";
            this.btnMemberships.Size = new System.Drawing.Size(200, 55);
            this.btnMemberships.TabIndex = 2;
            this.btnMemberships.Text = "    Συνδρομές";
            this.btnMemberships.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMemberships.UseVisualStyleBackColor = true;
            this.btnMemberships.Click += new System.EventHandler(this.btnMemberships_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCustomers.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnCustomers.Image = global::GymMasterAppDemo.Properties.Resources.users_people_workers_customers_icon_124243;
            this.btnCustomers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.Location = new System.Drawing.Point(0, 154);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(200, 55);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "    Πελάτες";
            this.btnCustomers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnHome.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnHome.Image = global::GymMasterAppDemo.Properties.Resources.home_house_icon_152213;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(0, 99);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(200, 55);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "    Αρχική";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxLogo.Image = global::GymMasterAppDemo.Properties.Resources.GymMasterLogo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(200, 99);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 2;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Click += new System.EventHandler(this.pictureBoxLogo_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(724, 551);
            this.pnlContent.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 575);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Icon = global::GymMasterAppDemo.Properties.Resources.GymMasterIcon;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GymMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslDb;
        private System.Windows.Forms.ToolStripStatusLabel tslFormName;
        private System.Windows.Forms.ToolStripStatusLabel tslManager;
        private System.Windows.Forms.ToolStripStatusLabel tslCopyright;
        private System.Windows.Forms.Button btnAnalytics;
        private System.Windows.Forms.Button btnMemberships;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnExitApp;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnWorksOuts;
        private System.Windows.Forms.Button btnBackupDBAll;
    }
}

