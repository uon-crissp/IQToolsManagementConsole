using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataLayer;

namespace IQToolsManagementConsole
{
    public partial class ucUserMangement : UserControl
    {
        public ucUserMangement()
        {
            InitializeComponent();
            LoadGroups();
            //LoadUsers();
        }

        private void LoadGroups()
        {
            Entity en = new Entity();
            localhost.Service1 sn = new localhost.Service1 ( );
            DataTable dt = new DataTable();
            ClsUtility.Init_Hashtable();

            dt = (DataTable)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB, ClsUtility.theParams, "SELECT [GID],[GroupName] FROM aa_UserGroups", ClsUtility.ObjectEnum.DataTable, "mssql");
            cbUserGroups.DisplayMember = "GroupName";
            cbUserGroups.ValueMember = "GID";
            cbUserGroups.DataSource = dt;
            cbUserGroups.Refresh();
            try
            {
                cbUserGroups.Text = "IQTools User";
            }catch(Exception){}
        }
        
        private void LoadUsers()
        {
            Entity en = new Entity();
            DataTable dt = new DataTable();
            ClsUtility.Init_Hashtable();

            dt = (DataTable)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB, ClsUtility.theParams, "SELECT * FROM aa_users", ClsUtility.ObjectEnum.DataTable, "mssql");
            dgvUsers.DataSource = dt;
            dgvUsers.Refresh();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPass1.Text = "";
            txtPass2.Text = "";
            chkEnableUser.Checked = false;
            chkChangePassword.Checked = false;
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text != "" && txtEmail.Text != "")
            {
                if(passwordsMatch(txtPass1.Text, txtPass2.Text))
                {
                    Entity en = new Entity();
                    localhost.Service1 sn = new localhost.Service1 ( );
                    ClsUtility.Init_Hashtable();
                    string sqlString = "INSERT INTO aa_users (FirstName, LastName, UserName, GID, Password, CreateDate, DeleteFlag, Email) VALUES (" +
                                        "'" + txtFirstName.Text + "' " +
                                        ",'" + txtLastName.Text + "' " +
                                        ",'" + txtUserName.Text + "' " +
                                        ",'" + cbUserGroups.SelectedValue.ToString() + "' " +
                                        ",'" + ClsUtility.Encrypt(txtPass1.Text) + "' " +
                                        ",getdate()" +
                                        //",null " +
                                        ",0,'" + txtEmail.Text + "')";
                                        
                                        /*+
                                        ",1 "
                                        
                                        +
                                        ",'" + txtEmail.Text + "') ";*/

                    try
                    {
                        int i = (int)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB, ClsUtility.theParams, sqlString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");

                    }
                    catch (Exception ex) {
                        string err = clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucUserManagement.cs: btnSaveUser_Click>>:" + ex.Message;
                        //sn.ErrorLogging(err, clsGbl.applicationName, 0);
                           MessageBox.Show(ex.Message); }
                    finally { btnUsers_Click(sender, e); }

                }
                else
                    MessageBox.Show("Password mismatch", "Error");
            }
            else MessageBox.Show("Missing Data", "Error");
        }

        private bool passwordsMatch(string pass1, string pass2)
        {
            if (pass1 == pass2)
                return true;
            else return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUserActivity_Click(object sender, EventArgs e)
        {
            LoadUsersHistory();
        }

        private void LoadUsersHistory()
        {
            Entity en = new Entity();
            DataTable dt = new DataTable();
            ClsUtility.Init_Hashtable();

            string sql = "SELECT " +
	                     "   u.FirstName + ' ' + u.LastName as [User], " +
	                     "   uh.LoginTime, " +
                         "   uh.LogOutTime " +
                         "FROM [IQTools_IQCare].[dbo].[aa_UserHistory] uh " +
                         "INNER JOIN [IQTools_IQCare].[dbo].[aa_Users] u " +
                         "ON uh.UID = u.UID"; 

            dt = (DataTable)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB, ClsUtility.theParams, sql, ClsUtility.ObjectEnum.DataTable, "mssql");
            dgvUsers.DataSource = dt;
            dgvUsers.Refresh();
        }
    }
}
