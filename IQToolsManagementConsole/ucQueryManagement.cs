using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IQToolsManagementConsole
{
    public partial class ucQueryManagement : UserControl
    {
        public ucQueryManagement()
        {
            InitializeComponent();
        }

        private void txtSQL_Leave(object sender, EventArgs e)
        {
            try
            {
                qbMain.SQL = txtSQL.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parsing error");
            }

        }

        private void plainTextSQLBuilder1_SQLUpdated(object sender, EventArgs e)
        {
            // Text of SQL query has been updated by the query builder.
            txtSQL.Text = plainTextSQLBuilder1.SQL;

        }

        private void QueryBuilder_SetupProperties()
        {
            // Assign Expression Editor
            qbMain.ExpressionEditor = expressionEditor1;
            txtSQL.QueryBuilder = qbMain;

        }

        private void QueryBuilder_SetupMetadata()
        {
            qbMain.MetadataContainer.LoadFromXMLFile("Resources\\IQTools_IQCare.xml");
        }

        private void QueryBuilder_Init(object sender, EventArgs e)
        {
            this.QueryBuilder_SetupProperties();
            this.QueryBuilder_SetupMetadata();

        }
    }
}
