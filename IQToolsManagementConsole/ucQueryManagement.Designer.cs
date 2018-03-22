namespace IQToolsManagementConsole
{
    partial class ucQueryManagement
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQueryManagement));
            this.tcQuery = new System.Windows.Forms.TabControl();
            this.tpDesign = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.qbMain = new ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder();
            this.autoSyntaxProvider1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.AutoSyntaxProvider(this.components);
            this.txtSQL = new ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor();
            this.tpPreview = new System.Windows.Forms.TabPage();
            this.tpManage = new System.Windows.Forms.TabPage();
            this.plainTextSQLBuilder1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(this.components);
            this.expressionEditor1 = new ActiveDatabaseSoftware.ExpressionEditor.ExpressionEditor(this.components);
            this.tcQuery.SuspendLayout();
            this.tpDesign.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcQuery
            // 
            this.tcQuery.Controls.Add(this.tpDesign);
            this.tcQuery.Controls.Add(this.tpPreview);
            this.tcQuery.Controls.Add(this.tpManage);
            this.tcQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcQuery.Location = new System.Drawing.Point(0, 0);
            this.tcQuery.Name = "tcQuery";
            this.tcQuery.Padding = new System.Drawing.Point(0, 0);
            this.tcQuery.SelectedIndex = 0;
            this.tcQuery.Size = new System.Drawing.Size(800, 463);
            this.tcQuery.TabIndex = 0;
            // 
            // tpDesign
            // 
            this.tpDesign.Controls.Add(this.splitContainer1);
            this.tpDesign.Location = new System.Drawing.Point(4, 22);
            this.tpDesign.Margin = new System.Windows.Forms.Padding(0);
            this.tpDesign.Name = "tpDesign";
            this.tpDesign.Size = new System.Drawing.Size(792, 437);
            this.tpDesign.TabIndex = 0;
            this.tpDesign.Text = "Design";
            this.tpDesign.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.qbMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtSQL);
            this.splitContainer1.Size = new System.Drawing.Size(792, 437);
            this.splitContainer1.SplitterDistance = 298;
            this.splitContainer1.TabIndex = 1;
            // 
            // qbMain
            // 
            this.qbMain.AddObjectFormOptions.MinimumSize = new System.Drawing.Size(430, 430);
            this.qbMain.CriteriaListOptions.BackColor = System.Drawing.SystemColors.Window;
            this.qbMain.CriteriaListOptions.CriteriaListFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qbMain.DiagramObjectColor = System.Drawing.SystemColors.Window;
            this.qbMain.DiagramObjectFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qbMain.DiagramPaneColor = System.Drawing.SystemColors.Window;
            this.qbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qbMain.ExpressionEditor = null;
            this.qbMain.FieldListOptions.DescriptionColumnOptions.Color = System.Drawing.Color.LightBlue;
            this.qbMain.FieldListOptions.MarkColumnOptions.PKIcon = ((System.Drawing.Image)(resources.GetObject("resource.PKIcon")));
            this.qbMain.FieldListOptions.NameColumnOptions.Color = System.Drawing.SystemColors.WindowText;
            this.qbMain.FieldListOptions.NameColumnOptions.PKColor = System.Drawing.SystemColors.WindowText;
            this.qbMain.FieldListOptions.TypeColumnOptions.Color = System.Drawing.SystemColors.GrayText;
            this.qbMain.FocusedDiagramObjectColor = System.Drawing.SystemColors.Window;
            this.qbMain.Location = new System.Drawing.Point(0, 0);
            this.qbMain.MetadataProvider = null;
            this.qbMain.MetadataTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.qbMain.MetadataTreeOptions.ImageList = null;
            this.qbMain.Name = "qbMain";
            this.qbMain.OfflineMode = true;
            this.qbMain.QueryStructureTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.qbMain.QueryStructureTreeOptions.ImageList = null;
            this.qbMain.Size = new System.Drawing.Size(792, 298);
            this.qbMain.SleepModeText = null;
            this.qbMain.SnapSize = new System.Drawing.Size(5, 5);
            this.qbMain.SQLChanging = false;
            this.qbMain.SyntaxProvider = this.autoSyntaxProvider1;
            this.qbMain.TabIndex = 0;
            this.qbMain.TreeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qbMain.Load += new System.EventHandler(this.QueryBuilder_Init);
            // 
            // autoSyntaxProvider1
            // 
            this.autoSyntaxProvider1.IdentCaseSens = ActiveDatabaseSoftware.ActiveQueryBuilder.IdentCaseSensitivity.Insensitive;
            // 
            // txtSQL
            // 
            this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQL.CommentColor = System.Drawing.Color.Green;
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.FunctionColor = System.Drawing.Color.Purple;
            this.txtSQL.InactiveSelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSQL.KeywordColor = System.Drawing.Color.Blue;
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.NumberColor = System.Drawing.Color.DarkCyan;
            this.txtSQL.QueryBuilder = null;
            this.txtSQL.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.txtSQL.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.txtSQL.Size = new System.Drawing.Size(792, 135);
            this.txtSQL.StringColor = System.Drawing.Color.DarkRed;
            this.txtSQL.TabIndex = 1;
            this.txtSQL.TextColor = System.Drawing.SystemColors.WindowText;
            this.txtSQL.TextPadding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtSQL.Leave += new System.EventHandler(this.txtSQL_Leave);
            // 
            // tpPreview
            // 
            this.tpPreview.Location = new System.Drawing.Point(4, 22);
            this.tpPreview.Margin = new System.Windows.Forms.Padding(0);
            this.tpPreview.Name = "tpPreview";
            this.tpPreview.Size = new System.Drawing.Size(792, 437);
            this.tpPreview.TabIndex = 1;
            this.tpPreview.Text = "Preview";
            this.tpPreview.UseVisualStyleBackColor = true;
            // 
            // tpManage
            // 
            this.tpManage.Location = new System.Drawing.Point(4, 22);
            this.tpManage.Margin = new System.Windows.Forms.Padding(0);
            this.tpManage.Name = "tpManage";
            this.tpManage.Size = new System.Drawing.Size(792, 437);
            this.tpManage.TabIndex = 2;
            this.tpManage.Text = "Manage";
            this.tpManage.UseVisualStyleBackColor = true;
            // 
            // plainTextSQLBuilder1
            // 
            this.plainTextSQLBuilder1.DynamicIndents = false;
            this.plainTextSQLBuilder1.DynamicRightMargin = false;
            this.plainTextSQLBuilder1.QueryBuilder = this.qbMain;
            this.plainTextSQLBuilder1.SQLUpdated += new System.EventHandler(this.plainTextSQLBuilder1_SQLUpdated);
            // 
            // expressionEditor1
            // 
            this.expressionEditor1.ActiveUnionSubQuery = null;
            this.expressionEditor1.BackColor = System.Drawing.SystemColors.Window;
            this.expressionEditor1.CommentColor = System.Drawing.Color.Green;
            this.expressionEditor1.Expression = "";
            this.expressionEditor1.FunctionColor = System.Drawing.Color.Purple;
            this.expressionEditor1.Height = 531;
            this.expressionEditor1.InactiveSelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.expressionEditor1.KeywordColor = System.Drawing.Color.Blue;
            this.expressionEditor1.Left = 0;
            this.expressionEditor1.NumberColor = System.Drawing.Color.DarkCyan;
            this.expressionEditor1.QueryBuilder = null;
            this.expressionEditor1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.expressionEditor1.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.expressionEditor1.ShowSuggestionAfterCharCount = 1;
            this.expressionEditor1.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.expressionEditor1.StringColor = System.Drawing.Color.DarkRed;
            this.expressionEditor1.TextColor = System.Drawing.SystemColors.WindowText;
            this.expressionEditor1.TextEditorFont = new System.Drawing.Font("Courier New", 9F);
            this.expressionEditor1.Top = 0;
            this.expressionEditor1.Width = 754;
            // 
            // ucQueryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcQuery);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucQueryManagement";
            this.Size = new System.Drawing.Size(800, 463);
            this.tcQuery.ResumeLayout(false);
            this.tpDesign.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcQuery;
        private System.Windows.Forms.TabPage tpDesign;
        private System.Windows.Forms.TabPage tpPreview;
        private System.Windows.Forms.TabPage tpManage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder qbMain;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.AutoSyntaxProvider autoSyntaxProvider1;
        private ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor txtSQL;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder plainTextSQLBuilder1;
        private ActiveDatabaseSoftware.ExpressionEditor.ExpressionEditor expressionEditor1;
    }
}
