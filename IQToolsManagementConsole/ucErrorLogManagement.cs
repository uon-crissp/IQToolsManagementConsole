using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;


namespace IQToolsManagementConsole
{
    public partial class ucErrorLogManagement : UserControl
    {
        public ucErrorLogManagement()
        {
            InitializeComponent();
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            Entity en = new Entity();
            ClsUtility.Init_Hashtable();
            DataSet ds;

            try
            {
                string sql = "SELECT " +
                            "[CapturedDate] as [Date] " +
                            ",(SELECT FirstName FROM aa_users WHERE [UID] = l.[UserID]) as [User] " +
                            ",[Application] " +
                            ",[Message] " +
                            "FROM [IQTools_IQCare].[dbo].[aa_errorLogs] l";

                ds = (DataSet)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog =IQTools_IQCare", ClsUtility.theParams, sql, ClsUtility.ObjectEnum.DataSet, "mssql");

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Dock = DockStyle.Fill;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
