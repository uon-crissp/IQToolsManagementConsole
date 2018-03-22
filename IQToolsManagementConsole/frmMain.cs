using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using DataLayer;
using System.Reflection;
using System.Xml;


namespace IQToolsManagementConsole
{
    public partial class frmMain : Form
    {
        
        public frmMain()
        {
            InitializeComponent();
        }
        //string connString = "Data Source=.\\sql2008r2;uid=sa;password=c0nstella";

        public Image picProgressImage
        {
            set
            {               
                this.picProgress.Image = value;
                this.InitializeComponent();
            }
            
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tpUserOps)
            {
                //clsGbl.cnnstr = getConnString();
                //MessageBox.Show("User Ops");
                ucUserMangement um = new ucUserMangement();
                um.Parent = tcMain.SelectedTab;
                um.Dock = DockStyle.Fill;
                um.Show();                
            }
            else if (tcMain.SelectedTab == tpDBOps)
            {
                ucDatabaseManagement ucDb = new ucDatabaseManagement(this);
                ucDb.Parent = tcMain.SelectedTab;
                ucDb.Dock = DockStyle.Fill;
                ucDb.Show();
            }
            else if (tcMain.SelectedTab == ErrorLog)
            {
                ucErrorLogManagement errMan = new ucErrorLogManagement();
                errMan.Parent = tcMain.SelectedTab;
                errMan.Dock = DockStyle.Fill;
                errMan.Show();
            }
            else if (tcMain.SelectedTab == tpWebsrvMan)
            {
                /*ucWebserviceManagement wenMan = new ucWebserviceManagement();
                wenMan.Parent = tcMain.SelectedTab;
                wenMan.Dock = DockStyle.Fill;
                wenMan.Show();*/

                ucWebserviceMan wenMan = new ucWebserviceMan();
                wenMan.Parent = tcMain.SelectedTab;
                wenMan.Dock = DockStyle.Fill;
                wenMan.Show();
            }
            else if (tcMain.SelectedTab == tpQueryOps)
            {
                ucQueryManagement qMan = new ucQueryManagement();
                qMan.Parent = tcMain.SelectedTab;
                qMan.Dock = DockStyle.Fill;
                qMan.Show();
            }
        }

        private string getConnString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + "\\app.config");
            XmlNode node = xmlDoc.SelectSingleNode("//add[@key='connString']");
            if (node != null)
            {
                return node.Attributes["value"].Value;
            }
            return "";
        }

        private string getMySQLConnString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + "\\app.config");
            XmlNode node = xmlDoc.SelectSingleNode("//add[@key='mySQLconnString']");
            if (node != null)
            {
                return node.Attributes["value"].Value;
            }
            return "";
        }

        private string getIQToolsDB()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + "\\app.config");
            XmlNode node = xmlDoc.SelectSingleNode("//add[@key='IQToolsDB']");
            if (node != null)
            {
                return node.Attributes["value"].Value;
            }
            return "";
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            clsGbl.cnnstr = getConnString();
            clsGbl.mySQLcnnstr = getMySQLConnString();
            tcMain_SelectedIndexChanged(sender, e);
            clsGbl.iqtoolsDB = getIQToolsDB();
            clsGbl.mainServerType = getMainServerType();
            lblVersion.Text = "Version " + Application.ProductVersion.Trim();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }

        private string getMainServerType()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + "\\app.config");
            XmlNode node = xmlDoc.SelectSingleNode("//add[@key='mainServerType']");
            if (node != null)
            {
                return node.Attributes["value"].Value;
            }
            return "";
        }
    }
}
