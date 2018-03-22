namespace IQToolsManagementConsole
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpDBOps = new System.Windows.Forms.TabPage();
            this.tpUserOps = new System.Windows.Forms.TabPage();
            this.tpQueryOps = new System.Windows.Forms.TabPage();
            this.tpLogs = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.ErrorLog = new System.Windows.Forms.TabPage();
            this.tpWebsrvMan = new System.Windows.Forms.TabPage();
            this.lblProgress = new System.Windows.Forms.Label();
            this.picProgress = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpLogs.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 561);
            this.splitContainer1.SplitterDistance = 74;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::IQToolsManagementConsole.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(814, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tcMain);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer2.Panel2.Controls.Add(this.lblVersion);
            this.splitContainer2.Panel2.Controls.Add(this.lblProgress);
            this.splitContainer2.Panel2.Controls.Add(this.picProgress);
            this.splitContainer2.Size = new System.Drawing.Size(1010, 486);
            this.splitContainer2.SplitterDistance = 433;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpDBOps);
            this.tcMain.Controls.Add(this.tpUserOps);
            this.tcMain.Controls.Add(this.tpQueryOps);
            this.tcMain.Controls.Add(this.tpLogs);
            this.tcMain.Controls.Add(this.ErrorLog);
            this.tcMain.Controls.Add(this.tpWebsrvMan);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1008, 431);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpDBOps
            // 
            this.tpDBOps.Location = new System.Drawing.Point(4, 24);
            this.tpDBOps.Margin = new System.Windows.Forms.Padding(0);
            this.tpDBOps.Name = "tpDBOps";
            this.tpDBOps.Size = new System.Drawing.Size(1000, 403);
            this.tpDBOps.TabIndex = 0;
            this.tpDBOps.Text = "Database Operations";
            this.tpDBOps.UseVisualStyleBackColor = true;
            // 
            // tpUserOps
            // 
            this.tpUserOps.Location = new System.Drawing.Point(4, 22);
            this.tpUserOps.Name = "tpUserOps";
            this.tpUserOps.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserOps.Size = new System.Drawing.Size(1000, 402);
            this.tpUserOps.TabIndex = 1;
            this.tpUserOps.Text = "User Management";
            this.tpUserOps.UseVisualStyleBackColor = true;
            // 
            // tpQueryOps
            // 
            this.tpQueryOps.Location = new System.Drawing.Point(4, 22);
            this.tpQueryOps.Margin = new System.Windows.Forms.Padding(0);
            this.tpQueryOps.Name = "tpQueryOps";
            this.tpQueryOps.Size = new System.Drawing.Size(1000, 402);
            this.tpQueryOps.TabIndex = 2;
            this.tpQueryOps.Text = "Query Management";
            this.tpQueryOps.UseVisualStyleBackColor = true;
            // 
            // tpLogs
            // 
            this.tpLogs.Controls.Add(this.splitContainer4);
            this.tpLogs.Location = new System.Drawing.Point(4, 22);
            this.tpLogs.Name = "tpLogs";
            this.tpLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogs.Size = new System.Drawing.Size(1000, 402);
            this.tpLogs.TabIndex = 3;
            this.tpLogs.Text = "Logs";
            this.tpLogs.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnSaveLog);
            this.splitContainer4.Panel1.Controls.Add(this.btnClearLog);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.txtLog);
            this.splitContainer4.Size = new System.Drawing.Size(994, 396);
            this.splitContainer4.SplitterDistance = 329;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(17, 62);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(140, 25);
            this.btnSaveLog.TabIndex = 1;
            this.btnSaveLog.Text = "&Save Log To File";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(17, 21);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(140, 25);
            this.btnClearLog.TabIndex = 0;
            this.btnClearLog.Text = "&Clear Log Window";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(664, 396);
            this.txtLog.TabIndex = 0;
            // 
            // ErrorLog
            // 
            this.ErrorLog.Location = new System.Drawing.Point(4, 22);
            this.ErrorLog.Name = "ErrorLog";
            this.ErrorLog.Padding = new System.Windows.Forms.Padding(3);
            this.ErrorLog.Size = new System.Drawing.Size(1000, 402);
            this.ErrorLog.TabIndex = 4;
            this.ErrorLog.Text = "Error logs view";
            this.ErrorLog.UseVisualStyleBackColor = true;
            // 
            // tpWebsrvMan
            // 
            this.tpWebsrvMan.Location = new System.Drawing.Point(4, 22);
            this.tpWebsrvMan.Name = "tpWebsrvMan";
            this.tpWebsrvMan.Padding = new System.Windows.Forms.Padding(3);
            this.tpWebsrvMan.Size = new System.Drawing.Size(1000, 402);
            this.tpWebsrvMan.TabIndex = 5;
            this.tpWebsrvMan.Text = "Webservice Management";
            this.tpWebsrvMan.UseVisualStyleBackColor = true;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(61, 30);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 14);
            this.lblProgress.TabIndex = 1;
            // 
            // picProgress
            // 
            this.picProgress.Location = new System.Drawing.Point(12, 7);
            this.picProgress.Name = "picProgress";
            this.picProgress.Size = new System.Drawing.Size(42, 39);
            this.picProgress.TabIndex = 0;
            this.picProgress.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(897, 31);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(46, 15);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 561);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "IQTools Management Console";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpLogs.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpDBOps;
        private System.Windows.Forms.TabPage tpUserOps;
        private System.Windows.Forms.TabPage tpQueryOps;
        private System.Windows.Forms.TabPage tpLogs;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Button btnClearLog;
        public System.Windows.Forms.PictureBox picProgress;
        public System.Windows.Forms.Label lblProgress;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage ErrorLog;
        private System.Windows.Forms.TabPage tpWebsrvMan;
        private System.Windows.Forms.Label lblVersion;
    }
}