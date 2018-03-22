using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Web.Administration;
using System.Configuration;
using System.DirectoryServices;
using DataLayer;

namespace IQToolsManagementConsole
{
    public partial class ucWebserviceMan : UserControl
    {
        private string _WebServUrl;
        private string _WebSiteName;
        private string _ID;
        private string _State;
        private Site _Site;

        public ucWebserviceMan()
        {
            InitializeComponent();

            InitializeWebservise();
            ManageControls();
            PopulateGrid();
            
        }

        #region service manage
        private void InitializeWebservise()
        {
            _WebServUrl = ConfigurationSettings.AppSettings["WebServiceUrl"].ToString();
            _WebSiteName = ConfigurationSettings.AppSettings["IISWebSiteEntryName"].ToString();

            lblSiteName.Text = _WebSiteName;
            lblSiteUrl.Text = _WebServUrl;
            try
            {
                if (!string.IsNullOrEmpty(_WebSiteName))
                {
                    var server = new ServerManager();
                    _Site = server.Sites.FirstOrDefault(s => s.Name == _WebSiteName);
                    if (_Site != null)
                    {
                        _ID = _Site.Id.ToString();
                        _State = _Site.State.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un-expected error occured, try running as an administrator!!");
            }
        }

        private void ManageControls()
        {
            lblStatus.Text = _State;
            if (_State == "Stopped")
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;

                //btnStart.Invoke((MethodInvoker)delegate { btnStart.Enabled = true; });
                //btnStop.Invoke((MethodInvoker)delegate { btnStop.Enabled = false; });
            }
            else
            {
                //btnStart.Invoke((MethodInvoker)delegate { btnStart.Enabled = false; });
                //btnStop.Invoke((MethodInvoker)delegate { btnStop.Enabled = true; });

                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopStartSite("Stop");
            ManageControls();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StopStartSite("Start");
            ManageControls();
        }

        private void StopStartSite(string action)
        {
            try
            {
                DirectoryEntry w3svc2 = new DirectoryEntry(String.Concat("IIS://" + _WebServUrl + "/W3SVC/", _ID));
                if (w3svc2 != null)
                {
                    w3svc2.Invoke(action, new object[0]);
                    int stateValue = Convert.ToInt32(w3svc2.Properties["ServerState"].Value);
                    if (stateValue == 2)
                        _State = "Started";
                    else if (stateValue == 4)
                        _State = "Stopped";
                }
            }
            catch (Exception e) { }
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
                            "FROM [IQTools_IQCare].[dbo].[aa_errorLogs] l " +
                            "WHERE Application LIKE '%Web Service%'";

                ds = (DataSet)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog =IQTools_IQCare", ClsUtility.theParams, sql, ClsUtility.ObjectEnum.DataSet, "mssql");

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        

        
    }
}
