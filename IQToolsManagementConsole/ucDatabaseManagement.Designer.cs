namespace IQToolsManagementConsole
{
    partial class ucDatabaseManagement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnUpdateVersion = new System.Windows.Forms.Button();
            this.rdbL2 = new System.Windows.Forms.RadioButton();
            this.rdbL1 = new System.Windows.Forms.RadioButton();
            this.btnDeIdentify = new System.Windows.Forms.Button();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.picProgress2 = new System.Windows.Forms.PictureBox();
            this.lblProgress2 = new System.Windows.Forms.Label();
            this.btnTruncate = new System.Windows.Forms.Button();
            this.cboCountries = new System.Windows.Forms.ComboBox();
            this.chkCreate = new System.Windows.Forms.CheckBox();
            this.btnLoadDbs = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEMR = new System.Windows.Forms.ComboBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.cboQuarter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clbDBs = new System.Windows.Forms.CheckedListBox();
            this.fBD1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdateVersion);
            this.splitContainer1.Panel1.Controls.Add(this.rdbL2);
            this.splitContainer1.Panel1.Controls.Add(this.rdbL1);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeIdentify);
            this.splitContainer1.Panel1.Controls.Add(this.chkSelect);
            this.splitContainer1.Panel1.Controls.Add(this.picProgress2);
            this.splitContainer1.Panel1.Controls.Add(this.lblProgress2);
            this.splitContainer1.Panel1.Controls.Add(this.btnTruncate);
            this.splitContainer1.Panel1.Controls.Add(this.cboCountries);
            this.splitContainer1.Panel1.Controls.Add(this.chkCreate);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadDbs);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cboEMR);
            this.splitContainer1.Panel1.Controls.Add(this.btnRestore);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
            this.splitContainer1.Panel1.Controls.Add(this.cboYear);
            this.splitContainer1.Panel1.Controls.Add(this.cboQuarter);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.clbDBs);
            this.splitContainer1.Size = new System.Drawing.Size(807, 361);
            this.splitContainer1.SplitterDistance = 373;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnUpdateVersion
            // 
            this.btnUpdateVersion.Enabled = false;
            this.btnUpdateVersion.Location = new System.Drawing.Point(29, 268);
            this.btnUpdateVersion.Name = "btnUpdateVersion";
            this.btnUpdateVersion.Size = new System.Drawing.Size(145, 34);
            this.btnUpdateVersion.TabIndex = 20;
            this.btnUpdateVersion.Text = "Update Version";
            this.btnUpdateVersion.UseVisualStyleBackColor = true;
            this.btnUpdateVersion.Click += new System.EventHandler(this.btnUpdateVersion_Click);
            // 
            // rdbL2
            // 
            this.rdbL2.AutoSize = true;
            this.rdbL2.Location = new System.Drawing.Point(300, 238);
            this.rdbL2.Margin = new System.Windows.Forms.Padding(0);
            this.rdbL2.Name = "rdbL2";
            this.rdbL2.Size = new System.Drawing.Size(36, 17);
            this.rdbL2.TabIndex = 19;
            this.rdbL2.TabStop = true;
            this.rdbL2.Text = "L2";
            this.rdbL2.UseVisualStyleBackColor = true;
            // 
            // rdbL1
            // 
            this.rdbL1.AutoSize = true;
            this.rdbL1.Location = new System.Drawing.Point(300, 221);
            this.rdbL1.Margin = new System.Windows.Forms.Padding(0);
            this.rdbL1.Name = "rdbL1";
            this.rdbL1.Size = new System.Drawing.Size(36, 17);
            this.rdbL1.TabIndex = 18;
            this.rdbL1.TabStop = true;
            this.rdbL1.Text = "L1";
            this.rdbL1.UseVisualStyleBackColor = true;
            // 
            // btnDeIdentify
            // 
            this.btnDeIdentify.Location = new System.Drawing.Point(188, 221);
            this.btnDeIdentify.Name = "btnDeIdentify";
            this.btnDeIdentify.Size = new System.Drawing.Size(109, 34);
            this.btnDeIdentify.TabIndex = 17;
            this.btnDeIdentify.Text = "De-Identify";
            this.btnDeIdentify.UseVisualStyleBackColor = true;
            // 
            // chkSelect
            // 
            this.chkSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(338, 2);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(30, 17);
            this.chkSelect.TabIndex = 16;
            this.chkSelect.Text = "-";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // picProgress2
            // 
            this.picProgress2.Location = new System.Drawing.Point(29, 308);
            this.picProgress2.Name = "picProgress2";
            this.picProgress2.Size = new System.Drawing.Size(36, 36);
            this.picProgress2.TabIndex = 14;
            this.picProgress2.TabStop = false;
            // 
            // lblProgress2
            // 
            this.lblProgress2.AutoSize = true;
            this.lblProgress2.BackColor = System.Drawing.SystemColors.Control;
            this.lblProgress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblProgress2.Location = new System.Drawing.Point(83, 279);
            this.lblProgress2.Name = "lblProgress2";
            this.lblProgress2.Size = new System.Drawing.Size(0, 13);
            this.lblProgress2.TabIndex = 15;
            // 
            // btnTruncate
            // 
            this.btnTruncate.Location = new System.Drawing.Point(29, 221);
            this.btnTruncate.Name = "btnTruncate";
            this.btnTruncate.Size = new System.Drawing.Size(145, 34);
            this.btnTruncate.TabIndex = 13;
            this.btnTruncate.Text = "&Truncate Databases";
            this.btnTruncate.UseVisualStyleBackColor = true;
            this.btnTruncate.Click += new System.EventHandler(this.btnTruncate_Click);
            // 
            // cboCountries
            // 
            this.cboCountries.FormattingEnabled = true;
            this.cboCountries.Items.AddRange(new object[] {
            "Kenya",
            "Nigeria",
            "Uganda",
            "Haiti",
            "Tanzania",
            "Rwanda",
            "Guyana",
            "Ethiopia"});
            this.cboCountries.Location = new System.Drawing.Point(61, 16);
            this.cboCountries.Name = "cboCountries";
            this.cboCountries.Size = new System.Drawing.Size(101, 21);
            this.cboCountries.TabIndex = 1;
            // 
            // chkCreate
            // 
            this.chkCreate.AutoSize = true;
            this.chkCreate.Location = new System.Drawing.Point(188, 180);
            this.chkCreate.Name = "chkCreate";
            this.chkCreate.Size = new System.Drawing.Size(170, 17);
            this.chkCreate.TabIndex = 11;
            this.chkCreate.Text = "(Re)Create IQTools Database";
            this.chkCreate.UseVisualStyleBackColor = true;
            // 
            // btnLoadDbs
            // 
            this.btnLoadDbs.Location = new System.Drawing.Point(29, 138);
            this.btnLoadDbs.Name = "btnLoadDbs";
            this.btnLoadDbs.Size = new System.Drawing.Size(145, 35);
            this.btnLoadDbs.TabIndex = 12;
            this.btnLoadDbs.Text = "Load &Databases";
            this.btnLoadDbs.UseVisualStyleBackColor = true;
            this.btnLoadDbs.Click += new System.EventHandler(this.btnLoadDbs_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(188, 138);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(145, 35);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh IQTools";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Country";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "EMR";
            // 
            // cboEMR
            // 
            this.cboEMR.FormattingEnabled = true;
            this.cboEMR.Items.AddRange(new object[] {
            "IQCare",
            "IQChart",
            "CTC2",
            "ISante",
            "ICAP"});
            this.cboEMR.Location = new System.Drawing.Point(222, 16);
            this.cboEMR.Name = "cboEMR";
            this.cboEMR.Size = new System.Drawing.Size(102, 21);
            this.cboEMR.TabIndex = 3;
            this.cboEMR.Text = "IQCare";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(188, 100);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(146, 32);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "&Restore to SQL Server";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Year";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(29, 100);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(145, 32);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "&Load Backups";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cboYear
            // 
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Items.AddRange(new object[] {
            "2011",
            "2012",
            "2013",
            "2014",
            "2015"});
            this.cboYear.Location = new System.Drawing.Point(60, 49);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(101, 21);
            this.cboYear.TabIndex = 5;
            // 
            // cboQuarter
            // 
            this.cboQuarter.FormattingEnabled = true;
            this.cboQuarter.Items.AddRange(new object[] {
            "Q1",
            "Q2",
            "Q3",
            "Q4"});
            this.cboQuarter.Location = new System.Drawing.Point(222, 49);
            this.cboQuarter.Name = "cboQuarter";
            this.cboQuarter.Size = new System.Drawing.Size(102, 21);
            this.cboQuarter.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quarter";
            // 
            // clbDBs
            // 
            this.clbDBs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbDBs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbDBs.FormattingEnabled = true;
            this.clbDBs.Location = new System.Drawing.Point(0, 0);
            this.clbDBs.Name = "clbDBs";
            this.clbDBs.Size = new System.Drawing.Size(431, 359);
            this.clbDBs.TabIndex = 0;
            // 
            // fBD1
            // 
            this.fBD1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ucDatabaseManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDatabaseManagement";
            this.Size = new System.Drawing.Size(807, 361);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProgress2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCountries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboEMR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboQuarter;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnLoadDbs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkCreate;
        private System.Windows.Forms.Button btnTruncate;
        private System.Windows.Forms.CheckedListBox clbDBs;
        private System.Windows.Forms.FolderBrowserDialog fBD1;
        private System.Windows.Forms.Label lblProgress2;
        private System.Windows.Forms.PictureBox picProgress2;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.Button btnDeIdentify;
        private System.Windows.Forms.Button btnUpdateVersion;
        private System.Windows.Forms.RadioButton rdbL2;
        private System.Windows.Forms.RadioButton rdbL1;

    }
}
