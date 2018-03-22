using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DataLayer;
using System.Threading;
using System.Xml;
using System.Reflection;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace IQToolsManagementConsole
{
    public partial class ucDatabaseManagement : UserControl
    {
        frmMain fMain;

        public ucDatabaseManagement(frmMain frm)
        {            
            InitializeComponent();
            fMain = frm;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            clbDBs.Items.Clear();
            fBD1.ShowDialog();

            if (fBD1.SelectedPath != "")
            {
                if (cboEMR.SelectedItem.ToString().ToLower() == "iqcare" 
                    || cboEMR.SelectedItem.ToString().ToLower() == "icap")
                {
                    DirectoryInfo dir = new DirectoryInfo(fBD1.SelectedPath);
                    FileInfo[] allFiles = dir.GetFiles("*.bak");
                    foreach (FileInfo file in allFiles)
                    {
                        clbDBs.Items.Add(file.FullName, false);
                    }
                }
                else if (cboEMR.SelectedItem.ToString().ToLower() == "ctc2" || cboEMR.SelectedItem.ToString().ToLower() == "iqchart")
                {
                    DirectoryInfo dir = new DirectoryInfo(fBD1.SelectedPath);
                    FileInfo[] allFiles = dir.GetFiles("*.mdb");
                    foreach (FileInfo file in allFiles)
                    {
                        clbDBs.Items.Add(file.FullName, false);
                    }
                }
                else if (cboEMR.SelectedItem.ToString().ToLower() == "isante")
                {
                    DirectoryInfo dir = new DirectoryInfo(fBD1.SelectedPath);
                    FileInfo[] allFiles = dir.GetFiles("*.sql");
                    foreach (FileInfo file in allFiles)
                    {
                        clbDBs.Items.Add(file.FullName, false);
                    }
                }                   

            }

        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string emr = cboEMR.SelectedItem.ToString().ToLower();
            string[] selectedBaks = new string[clbDBs.CheckedItems.Count];
            for (int i = 0; i < clbDBs.CheckedItems.Count; i++)
            {
                selectedBaks[i] = clbDBs.CheckedItems[i].ToString();
            }

            Thread restoreThread = new Thread(() => RestoreDB(emr, selectedBaks));
            restoreThread.SetApartmentState(ApartmentState.STA);
            restoreThread.Start();
        }

        private void RestoreDB(string emr, string[] baks)
        {
            Entity en = new Entity();
           //localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            string restoreString = "";
            string dbName = "";
            string ACCconnString = "";
            string SQLconnString = "";
            int i = 0;
            #region IQCare
            if (emr == "iqcare" || emr == "icap")
            {
                foreach (string bak in baks)
                {
                   
                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", Properties.Resources.progress2);
                    dbName = bak.Substring(bak.LastIndexOf("\\") + 1, bak.LastIndexOf(".") - (bak.LastIndexOf("\\") + 1));
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Restoring " + dbName + " Please Wait....");
                    string filePath = "";

                    string cstr = clsGbl.cnnstr;
                    string currServer = cstr.Substring(cstr.IndexOf("Data Source=") + 12, cstr.IndexOf(";") - 12);

                    try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') ALTER DATABASE [" + dbName + "] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB +",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Drop DB Error " + ex.Message); }

                    try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') DROP DATABASE [" + dbName + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                    catch (Exception ex) {
                        //sn.ErrorLogging ( clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0 );
                        UpdateLog("Drop DB Error " + ex.Message); }
                    
                    Server sqlServer = new Server(currServer);

                    //if (sqlServer.Settings.DefaultFile == "")
                    //    filePath = sqlServer.Information.RootDirectory + "\\DATA";
                    //else filePath = sqlServer.Settings.DefaultFile;
                    filePath = "F:\\MSSQL\\DATA";
                    //C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA


                    //string path = @"SELECT SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1) FROM master.dbo.sysdatabases WHERE name = 'master'";
                    //DataRow dr = (DataRow)en.ReturnObject ( clsGbl.cnnstr, ClsUtility.theParams, path, ClsUtility.ObjectEnum.DataRow, "mssql" );
                    //filePath = dr[0].ToString ( );


                    //restoreString = "RESTORE DATABASE " + dbName + " " +
                    //                "FROM DISK = N'" + bak + "' " +
                    //                "WITH FILE = 1, " +
                    //                "MOVE N'TestDataBase_IQCare' TO N'C:\\Cohort\\DataFiles\\Database\\DataFiles\\" + dbName + ".mdf' , " +
                    //                "MOVE N'TestDataBase_IQCare_log' TO N'C:\\Cohort\\DataFiles\\Database\\DataFiles\\" + dbName + "_log.ldf'";

                    restoreString = "RESTORE DATABASE " + dbName + " " +
                                    "FROM DISK = N'" + bak + "' " +
                                    "WITH FILE = 1, " +
                                    "MOVE N'TestDataBase_IQCare' TO N'"+filePath+"\\" + dbName + ".mdf' , " +
                                    "MOVE N'TestDataBase_IQCare_log' TO N'"+filePath+"\\" + dbName + "_log.ldf'";
                    try
                    {
                        i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, restoreString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                    }
                    catch (Exception ex) {
                      //sn.ErrorLogging ( clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0 );
                        UpdateLog("Restore Error " + ex.Message); }                   
 

                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", null);
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Complete");
                }
            }
            #endregion IQCare

            #region CTC2
            else if (emr == "ctc2" || emr == "iqchart")
            {
                foreach (string bak in baks)
                {
                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", Properties.Resources.progress2);
                    dbName = bak.Substring(bak.LastIndexOf("\\") + 1, bak.LastIndexOf(".") - (bak.LastIndexOf("\\") + 1));
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Restoring " + dbName + " Please Wait....");
                    string filePath = "";

                    string cstr = clsGbl.cnnstr;
                    string currServer = cstr.Substring(cstr.IndexOf("Data Source=") + 12, cstr.IndexOf(";") - 12);

                    Server sqlServer = new Server(currServer);

                    string path = @"SELECT SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1) FROM master.dbo.sysdatabases WHERE name = 'master'";
                    DataRow dr = (DataRow)en.ReturnObject ( clsGbl.cnnstr,ClsUtility.theParams, path, ClsUtility.ObjectEnum.DataRow, "mssql" );
                    filePath = dr[0].ToString ( );
                    //if (sqlServer.Settings.DefaultFile == "")
                    // filePath = sqlServer.Information.RootDirectory + "\\DATA";
                    //else 
                    //   filePath = sqlServer.Settings.DefaultFile;
                  //  filePath = "C:\\Program Files\\Microsoft SQL Server\\MSSQL10.SQLEXPRESS\\MSSQL\\DATA";
                    sqlServer.ConnectionContext.Disconnect();

                    //Drop Database if it exists
                    try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams
                        , "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') ALTER DATABASE [" + dbName + "] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                    catch (Exception ex) {
                      //sn.ErrorLogging ( clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0 );
                        UpdateLog("Upsize Error " + ex.Message); 
                    
                   }

                    try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams
                        , "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') DROP DATABASE [" + dbName + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                      catch (Exception ex) {
                      //sn.ErrorLogging ( clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0 );
                        UpdateLog("Upsize Error " + ex.Message); }



                    //restoreString = "RESTORE DATABASE " + dbName + " " +
                    //                "FROM DISK = N'" + bak + "' " +
                    //                "WITH FILE = 1, " +
                    //                "MOVE N'TestDataBase_IQCare' TO N'" + filePath + "\\" + dbName + ".mdf' , " +
                    //                "MOVE N'TestDataBase_IQCare_log' TO N'" + filePath + "\\" + dbName + "_log.ldf'";


                    restoreString = "CREATE DATABASE [" + dbName + "] ON  PRIMARY " +
                                "( NAME = N'" + dbName + "'" +
                                ", FILENAME = N'" + filePath + "\\" + dbName + ".mdf' " +
                                //", SIZE = 2048KB " +
                                ", FILEGROWTH = 1024KB )" +
                                "LOG ON " +
                                "( NAME = N'" + dbName + "_log' " +
                                ", FILENAME = N'" + filePath + "\\" + dbName + "_log.ldf' " +
                                ", SIZE = 1024KB " +
                                ", FILEGROWTH = 10% ) ";
                    try
                    {
                        i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, restoreString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                    }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB+ ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Restore Error " + ex.Message); }
                        
                    ACCconnString = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source= " + bak 
                        + " ;Jet OLEDB:Database Password=acp7;Persist Security Info=False;";
                    SQLconnString = clsGbl.cnnstr + ";Initial Catalog=" + dbName + ";";

                    //Upsize Access Database
                    loadData(ACCconnString, SQLconnString);
                    CreateIndexes("ctc2", SQLconnString);

                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", null);
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Complete");
                }
            }
            #endregion CTC2
            
            else if(emr == "isante")
            {
                foreach (string bak in baks)
                {
                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", Properties.Resources.progress2);
                    dbName = bak.Substring(bak.LastIndexOf("\\") + 1, bak.LastIndexOf(".") - (bak.LastIndexOf("\\") + 1));
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Restoring " + dbName + " Please Wait....");
                    //string filePath = "";

                    string cstr = clsGbl.mySQLcnnstr;
                    string currServer = cstr.Substring(cstr.IndexOf("Data Source=") + 12, cstr.IndexOf(";") - 12);

                    try { DropMySQLDB(dbName); }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB+ ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Drop DB Error " + ex.Message); }

                    try
                    {
                        RestoreISanteDB(bak, dbName);
                    }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Restore DB Error " + ex.Message); }

                    //Server sqlServer = new Server(currServer);
                    //if (sqlServer.Settings.DefaultFile == "")
                    //    filePath = sqlServer.Information.RootDirectory + "\\DATA";
                    //else filePath = sqlServer.Settings.DefaultFile;


                    ////restoreString = "RESTORE DATABASE " + dbName + " " +
                    ////                "FROM DISK = N'" + bak + "' " +
                    ////                "WITH FILE = 1, " +
                    ////                "MOVE N'TestDataBase_IQCare' TO N'C:\\Cohort\\DataFiles\\Database\\DataFiles\\" + dbName + ".mdf' , " +
                    ////                "MOVE N'TestDataBase_IQCare_log' TO N'C:\\Cohort\\DataFiles\\Database\\DataFiles\\" + dbName + "_log.ldf'";

                    //restoreString = "RESTORE DATABASE " + dbName + " " +
                    //                "FROM DISK = N'" + bak + "' " +
                    //                "WITH FILE = 1, " +
                    //                "MOVE N'TestDataBase_IQCare' TO N'" + filePath + "\\" + dbName + ".mdf' , " +
                    //                "MOVE N'TestDataBase_IQCare_log' TO N'" + filePath + "\\" + dbName + "_log.ldf'";
                    //try
                    //{
                    //    i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, restoreString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                    //}
                    //catch (Exception ex) { UpdateLog("Restore Error " + ex.Message); }


                    SetControlPropertyThreadSafe(fMain.picProgress, "Image", null);
                    SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Complete");
                }
            }
        }

        private void CreateIndexes(string emr, string databaseConnectionString)
        {
          Entity ep = new Entity ( );

          if (emr.ToLower ( ) == "ctc2")
              {
                   //a cleaner and maintable alternative would be to use an SQL File placed in the Cohort Folder @Consider using the approach 
                    string createIndex = @"IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'tblStatusIndex')
		        BEGIN
			        EXEC('DROP INDEX tblStatus.tblStatusIndex')
		        END

		        IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'tblVisitsIndex')
		        BEGIN
			        EXEC('DROP INDEX tblVisits.tblVisitsIndex')
		        END

		        IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'tblPatientsIndex')
		        BEGIN
			        EXEC('DROP INDEX tblPatients.tblPatientsIndex')
		        END


		        IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'Non_tblStatusIndex')
		        BEGIN
			        EXEC('DROP INDEX tblStatus.Non_tblStatusIndex')
		        END


		        IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'Non_tblVisitsIndex')
		        BEGIN
			        EXEC('DROP INDEX tblVisits.Non_tblVisitsIndex')
		        END

		        IF EXISTS (SELECT * FROM SYS.INDEXES WHERE name = 'tblStartARTAnotherClinicIndex')
		        BEGIN
			        EXEC('DROP INDEX tblStartARTAnotherClinic.tblStartARTAnotherClinicIndex')
		        END


		        EXEC ('CREATE CLUSTERED INDEX [tblStatusIndex] ON [dbo].[tblStatus]
		        (
		        [PatientID] ASC,
		        [StatusDate] ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]')

		        EXEC ('CREATE  CLUSTERED INDEX [tblVisitsIndex] ON [dbo].[tblVisits]
		        (
		        [PatientID] ASC,
		        [VisitDate] ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		        ')

		        EXEC ('CREATE CLUSTERED INDEX [tblStartARTAnotherClinicIndex] ON [dbo].[tblStartARTAnotherClinic]
		        (
		        [PatientID] ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]')
 
 
		        EXEC ('CREATE CLUSTERED INDEX [tblPatientsIndex] ON [dbo].[tblpatients]
		        (
		        [PatientID] ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]')

		        EXEC ('CREATE NONCLUSTERED INDEX [Non_tblStatusIndex] ON [dbo].[tblStatus]
		        (
		        [Status] ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]')

		        EXEC ('CREATE NONCLUSTERED INDEX [Non_tblVisitsIndex] ON [dbo].[tblVisits]
		        (
		        [ARVStatusCode] ASC,
		         ARVCode  ASC
		        )WITH (STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]')";
            try
            {
                int i = (int)ep.ReturnObject(databaseConnectionString, ClsUtility.theParams, createIndex, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
            }
            catch (Exception ex)
            {
              //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB+ ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
              UpdateLog ( "Create Index Error " + ex.Message );
            }
          }
        }

        private void DropMySQLDB(string dbName)
        {
            Entity en = new Entity();
            ClsUtility.Init_Hashtable();
            string dropSQL = "DROP SCHEMA " + dbName + "";
            int i = (int)en.ReturnObject(clsGbl.mySQLcnnstr, ClsUtility.theParams, dropSQL, ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
        }

        private void RestoreISanteDB(string filePath, string dbName)
        {
            string createDB = "CREATE SCHEMA " + dbName + "";
            
            //string restoreString = "USE " + dbName + "; SOURCE " + filePath + "";
            //string restoreString = "SOURCE " + filePath + "";
            Entity en = new Entity();
            //localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            int i = 0;
            try
            {
                i = (int)en.ReturnObject(clsGbl.mySQLcnnstr, ClsUtility.theParams, createDB, ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
            }
            catch (Exception ex) {
                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ", <<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                UpdateLog(ex.Message); }
            i = 0;
            
            try
            {
                MySqlBackup mb = new MySqlBackup(clsGbl.mySQLcnnstr + ";Database=" + dbName);
                //UserFunctions
                mb.ImportInfo.FileName = "C:\\Cohort\\DataFiles\\Database\\userFunctions.sql";
                mb.Import();
                //Database
                mb.ImportInfo.FileName = filePath;
                mb.Import();
                //Views
                mb.ImportInfo.FileName = "C:\\Cohort\\DataFiles\\Database\\lampViews.sql";
                mb.Import();
            }
            catch (Exception ex) {
                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                UpdateLog(ex.Message); }

        }

        private void loadData(string accessConnstring, string sqlConnString)
        {
            Entity en = new Entity();
            localhost.Service1 sn = new localhost.Service1 ( );
            OleDbDataAdapter da;
            DataTable dt = new DataTable("temp");
            DataTable theDt = new DataTable();
            DataTableReader theDr = null;
            OleDbConnection conn = new OleDbConnection(accessConnstring);
            try
            {
                conn.Open();
                theDt = conn.GetSchema("Tables");
                theDr = theDt.CreateDataReader();
            

            while (theDr.Read())
            {
                if (theDr[3].ToString() == "TABLE")
                {
                    try
                    {
                        dt.Clear();
                        string table = theDr[2].ToString();
                        da = new OleDbDataAdapter("SELECT * FROM " + theDr[2].ToString() + ";", conn);
                        dt.BeginLoadData();
                        da.Fill(dt);
                        dt.EndLoadData();
                    }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB+ ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Upsize Error " + ex.Message); }
                }

                try 
                { 
                    crtTable(sqlConnString, theDr[2].ToString(), dt); 
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog("Upsize Error " + ex.Message); }
            }

            conn.Close();
            }
            catch (Exception ex)
            { UpdateLog(ex.Message); }

        }

        private string getConnItem(string item, SqlConnection conn)
        {
            switch (item.ToLower())
            {
                case "database":
                    return conn.Database;
                case "server":
                    return conn.DataSource;
                default:
                    return string.Empty;
            }
        }

        public bool crtTable(string connString, string tblName, DataTable theDt)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //Server svr = new Server(new ServerConnection(new SqlConnection(connString)));
            //Microsoft.SqlServer.Management.Smo.Database db = svr.Databases[getConnItem("database", new SqlConnection(connString))];
            //Microsoft.SqlServer.Management.Smo.Table tmp = new Microsoft.SqlServer.Management.Smo.Table(db, tblName);
            //Column tmpC = new Column();

            if (theDt.Columns.Count > 0)
            {
                Server svr = new Server(new ServerConnection(new SqlConnection(connString)));
                Microsoft.SqlServer.Management.Smo.Database db = svr.Databases[getConnItem("database", new SqlConnection(connString))];
                Microsoft.SqlServer.Management.Smo.Table tmp = new Microsoft.SqlServer.Management.Smo.Table(db, tblName);
                Column tmpC = new Column();
                foreach (DataColumn dc in theDt.Columns)
                {
                    //Create columns from datatable column schema
                    tmpC = new Column(tmp, dc.ColumnName);
                    tmpC.DataType = GetDataType(dc.DataType.ToString());
                    tmp.Columns.Add(tmpC);
                }
                tmp.Create();
                svr.ConnectionContext.Disconnect();
                
            }

            //Open a connection with destination database to bulk copy it
            using (SqlConnection connection =
                   new SqlConnection(connString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();


                    //Open bulkcopy connection.
                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                    {
                        Entity en = new Entity();
                        localhost.Service1 sn = new localhost.Service1 ( );
                        //Set destination table name
                        //to table previously created.
                        bulkcopy.DestinationTableName = "dbo.[" + tblName + "]";
                        bulkcopy.NotifyAfter = 1;
                        bulkcopy.BulkCopyTimeout = 600;
                        try
                        {
                            bulkcopy.WriteToServer(theDt);
                        }
                        catch (Exception ex)
                        {
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                            UpdateLog("Upsize Error - DB=" + ex.Message);
                            return false;
                        }
                        bulkcopy.Close();
                    }
                }
                catch (Exception ex)
                { UpdateLog(ex.Message); }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            theDt.Clear(); 
            //tmpC = null; tmp = null; svr = null; 
            theDt.Columns.Clear(); theDt = null;
            Cursor.Current = Cursors.Default;
            return true;

        }

        private DataType GetDataType(string dataType)
        {
            DataType DTTemp = null;

            switch (dataType)
            {
                case ("System.Decimal"):
                    DTTemp = DataType.Decimal(2, 18);
                    break;
                case ("System.Byte"):
                    DTTemp = DataType.TinyInt;
                    break;
                case ("System.Boolean"):
                    DTTemp = DataType.Bit;
                    break;
                case ("System.String"):
                    DTTemp = DataType.NVarChar(255);
                    break;
                case ("System.Int16"):
                    DTTemp = DataType.SmallInt;
                    break;
                case ("System.Int32"):
                    DTTemp = DataType.Int;
                    break;
                case ("System.Int64"):
                    DTTemp = DataType.BigInt;
                    break;
                case ("System.DateTime"):
                    DTTemp = DataType.DateTime;
                    break;
                //case ("System.DateTime"):
                //    DTTemp = DataType.NVarChar(255);
                //    break;
                case ("System.Double"):
                    DTTemp = DataType.Float;
                    break;
                case ("System.Single"):
                    DTTemp = DataType.Real;
                    break;
                default:
                    DTTemp = DataType.Variant;
                    break;
            }
            return DTTemp;
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(getConnString());
            string emr = cboEMR.SelectedItem.ToString().ToLower();
            string[] selectedDbs = new string[clbDBs.CheckedItems.Count];
            if (clbDBs.CheckedItems.Count > 0)
            {
                //tcMain.SelectedTab = tpLogs;
                for (int i = 0; i < clbDBs.CheckedItems.Count; i++)
                {
                    selectedDbs[i] = clbDBs.CheckedItems[i].ToString();
                }

                Thread refreshThread = new Thread(() => refreshIQTools(emr, selectedDbs, chkCreate.Checked));
                refreshThread.SetApartmentState(ApartmentState.STA);
                refreshThread.Start();
            }
            else MessageBox.Show("Please Select A Database to Refresh", "IQTools", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void refreshIQTools(string emr, string[] dbs, bool recreate)
        {
            SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Processing");
            SetControlPropertyThreadSafe(fMain.picProgress, "Image", Properties.Resources.progress2);
            Entity en = new Entity();
            //localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            DataRow dr;
            
            foreach (string db in dbs)
            {
                if (recreate)
                {

                    if (emr == "iqchart")
                    {
                        DropIQToolsDB("IQTools_" + db);
                        try
                        {
                            dr = (DataRow)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = " + db, ClsUtility.theParams, "SELECT TOP 1 DataVer FROM zz_varData", ClsUtility.ObjectEnum.DataRow, "mssql");
                            emr = "iqchart" + dr[0].ToString().Substring(0, 1);
                        }
                        catch (Exception ex) {
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                            UpdateLog("Version Query Error" + ex.Message); }
                        RestoreIQToolsDB(emr, db);
                    }
                    else if (emr == "isante")
                    {
                        RestoreIQToolsDB(emr, db);
                    }
                    else
                    {
                        DropIQToolsDB("IQTools_" + db);
                        RestoreIQToolsDB(emr, db);
                    }
                }
                //refresh
                ClearTables("IQTools_" + db, db, emr);
                CreateTables("IQTools_" + db, db, emr);
            }
            SetControlPropertyThreadSafe(fMain.picProgress, "Image", null);
            SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Complete. Please Review Logs");
        }

        private void btnLoadDbs_Click(object sender, EventArgs e)
        {
            clbDBs.Items.Clear();
            Entity en = new Entity();
            localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            DataTable dt = new DataTable();
            if (cboEMR.SelectedItem.ToString() == "ISante")
            {
                try
                {
                    dt = (DataTable)en.ReturnObject(clsGbl.mySQLcnnstr, ClsUtility.theParams, "SHOW DATABASES LIKE 'ISante%'", ClsUtility.ObjectEnum.DataTable, "mysql");
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB +  ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                string sqlString = "Select Name from sys.databases where name like '" + cboEMR.SelectedItem.ToString().ToLower() + "%'";
                try
                {
                    dt = (DataTable)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, sqlString, ClsUtility.ObjectEnum.DataTable, "mssql");
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreDB>> : " + ex.Message, clsGbl.applicationName, 0);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
            }
            DataTableReader dr = dt.CreateDataReader();
            while (dr.Read())
            {
                clbDBs.Items.Add(dr[0].ToString());
            }
        }

        private void DropIQToolsDB(string dbName)
        {
            SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Dropping Database " + dbName);
            string dropString = "DROP Database " + dbName; //Write a better drop command. This will fail if database in use
            Entity en = new Entity();
            //localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            int i = 0;

            try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') ALTER DATABASE [" + dbName + "] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
            catch (Exception ex) {
                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: DropIQToolsDB>> : " + ex.Message, clsGbl.applicationName, 0);
                UpdateLog("Drop DB Error " + ex.Message); }

            try { i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, "IF EXISTS(Select name from sys.databases where name = '" + dbName + "') DROP DATABASE [" + dbName + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
            catch (Exception ex) {
                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: DropIQToolsDB>> : " + ex.Message, clsGbl.applicationName, 0);
                UpdateLog("Drop DB Error " + ex.Message); }

            SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "...");

        }

        private void UpdateLog(string p)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { fMain.txtLog.Text += " \n " + p; }));
            }
        }

        private void RestoreIQToolsDB(string emr, string db)
        {
            Entity en = new Entity();
            localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();
            // Does NOT Check For IQCare Version
            if (emr != "isante")
            {
                string restoreString = "RESTORE DATABASE IQTools_" + db.Trim() + " \n" +
                                        //"FROM DISK = 'C:\\Cohort\\DataFiles\\Database\\IQTools_" + emr.Trim() + ".Bak' " +
                                        //Using one DB going forward
                                        "FROM DISK = 'C:\\DBs\\IQTools.bak' " +
                                        "WITH REPLACE, " +
                                        "MOVE 'IQTools' TO 'F:\\MSSQL\\DATA\\IQTools_" + db.Trim() + ".mdf' , " +
                                        "MOVE 'IQTools_log' TO 'F:\\MSSQL\\DATA\\IQTools_" + db.Trim() + "_log.ldf'";

                SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Restoring IQTools_" + db);
                try
                {
                    int i = (int)en.ReturnObject(clsGbl.cnnstr, ClsUtility.theParams, restoreString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                }
                catch (Exception ex)
                {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreIQToolsDB, ReturnObject>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message);
                }

                string dbString = "UPDATE aa_Database SET DBase = '" + db.Trim() + "'";
                try
                {
                    int i = (int)en.ReturnObject(clsGbl.cnnstr + ";Initial Catalog = IQTools_" + db.Trim(), ClsUtility.theParams, dbString, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                }
                catch (Exception ex)
                {
                    UpdateLog(ex.Message);
                }
            }
            else 
            {
                //Drop 
                SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Restoring IQTools_" + db);
                try { DropMySQLDB("IQTools_" + db); }catch(Exception ex){
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreIQToolsDB, DropMySQLDB>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message);}
                //Create
                try
                {
                    int i = (int)en.ReturnObject(clsGbl.mySQLcnnstr, ClsUtility.theParams, "CREATE SCHEMA IQTools_" + db + ";", ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreIQToolsDB, ReturnObject>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message); }
                //Restore
                try
                {
                    string file = "C:\\Cohort\\DataFiles\\Database\\IQTools.sql";
                    MySqlBackup mb = new MySqlBackup(clsGbl.mySQLcnnstr + ";Database=IQTools_" + db + ";");
                    mb.ImportInfo.FileName = file;
                    mb.Import();
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: RestoreIQToolsDB, Import>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message); }


            }
        }

        private void ClearTables(string IQToolsDB, string EMRDB, string emr)
        {

            int i = 0;
            DataTable dt = new DataTable();
            Entity en = new Entity();
            localhost.Service1 sn = new localhost.Service1 ( );
            ClsUtility.Init_Hashtable();

            SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Clearing TEMP Tables on " + IQToolsDB);
            #region NotISante
            if (emr != "isante")
            {

                string iqtConnString = clsGbl.cnnstr + ";Initial Catalog=" + IQToolsDB;
                string toDrop = "SELECT [Name] FROM sys.Synonyms UNION SELECT [Name] From Sys.Tables Where [Name] Like " +
                                "'TPS%' Or [Name] Like 'Mgr%' Or [Name] Like 'Tmp%'";

                try
                {
                    dt = (DataTable)en.ReturnObject(iqtConnString, ClsUtility.theParams, toDrop, ClsUtility.ObjectEnum.DataTable, "mssql");
                    DataTableReader dr = dt.CreateDataReader();
                    while (dr.Read())
                    {
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP SYNONYM [dbo].[" + dr[0].ToString() + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ea) 
                        { //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [1st sub try]>> : " + ea.Message, clsGbl.applicationName, 0); 
                        }
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE [" + dr[0].ToString() + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ea) 
                        { 
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB +  ",<<ucDatabaseManagement.cs: ClearTables, [2nd sub try]>> : " + ea.Message, clsGbl.applicationName, 0); 
                        }

                    }
                    if (emr == "iqcare")
                    {
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE mst_patient", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ea) { 
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [3rd sub try]>> : " + ea.Message, clsGbl.applicationName, 0); 
                        }

                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE dtl_patientContacts", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ea) { 
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [4th sub try]>> : " + ea.Message, clsGbl.applicationName, 0); 
                        }

                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE tmp_PatientMaster", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ea) 
                        { //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [5th sub try]>> : " + ea.Message, clsGbl.applicationName, 0); 
                        }
                    }
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [Main try inside IF]>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message); }
            }
            #endregion NotISante
            else
            {
               
                string iqtConnString = clsGbl.mySQLcnnstr + ";Database=" + IQToolsDB + ";";
                string toDrop = "SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES " +
                                "WHERE TABLE_SCHEMA = '" + IQToolsDB + "' " +
                                "AND TABLE_NAME LIKE 'tmp%' " +
                                "AND TABLE_NAME NOT LIKE 'iqs%'";
                dt = null;
                 
                try
                 {
                    dt = (DataTable)en.ReturnObject(iqtConnString, ClsUtility.theParams, toDrop, ClsUtility.ObjectEnum.DataTable, "mysql");

                    DataTableReader dr = dt.CreateDataReader();
                    while (dr.Read())
                    {
                        if (dr[1].ToString() == "VIEW")
                        {
                            try
                            {
                                i = 0;
                                i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP VIEW `" + dr[0].ToString() + "`", ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
                            } catch (Exception ea) { 
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [1st try in ELSE]>> : " + ea.Message, clsGbl.applicationName, 0); 
                            }
                        }
                        else if (dr[1].ToString() == "BASE TABLE")
                        {
                            try{
                                i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE `" + dr[0].ToString() + "`", ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
                            } catch (Exception ea) { 
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: ClearTables, [2nd try in ELSE]>> : " + ea.Message, clsGbl.applicationName, 0); 
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ", <<ucDatabaseManagement.cs: ClearTables, [Main try in ELSE]>> : " + ex.Message, clsGbl.applicationName, 0);
                }
            }

        }

        private void CreateTables(string IQToolsDB, string EMRDB, string emr)
        {

            #region NotISANTE
            if (emr != "isante")
            {
                String synonymsSQL = "";
                //Connection Strings
                string cstr = clsGbl.cnnstr;
                string currServer = cstr.Substring(cstr.IndexOf("Data Source=") + 12, cstr.IndexOf(";") - 12);
                String iqcConnString = cstr + ";Initial Catalog=" + EMRDB;
                String iqtConnString = cstr + ";Initial Catalog=" + IQToolsDB;
                //

                //Create Synonyms
                int i = 0;
                DataTable dt = new DataTable();

                Entity en = new Entity();
                localhost.Service1 sn = new localhost.Service1 ( );
                ClsUtility.Init_Hashtable();
                if (emr == "iqcare" || emr == "smartcare")
                {
                    synonymsSQL = "SELECT TABLE_NAME,'" + currServer + "' flServer,TABLE_NAME,'true' snActive " +
                                        "FROM INFORMATION_SCHEMA.TABLES where Table_Name not like '%sysdiagrams%' " +
                                        "and table_Name not like '%tblSetupFacilities%'";
                    dt = (DataTable)en.ReturnObject(iqcConnString, ClsUtility.theParams, synonymsSQL, ClsUtility.ObjectEnum.DataTable, "mssql");
                }
                else if (emr == "ctc2")
                {
                    //Get List of Tables From aa_Tables Where EMR = 'CTC2'
                    synonymsSQL = "Select tblName,'" + currServer + "' flServer, tblName, 'true' snActive  From aa_Tables Where EMR = 'CTC2' And snActive = 1";
                    dt = (DataTable)en.ReturnObject(iqtConnString, ClsUtility.theParams, synonymsSQL, ClsUtility.ObjectEnum.DataTable, "mssql");
                }

                SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Creating TEMP Tables on " + IQToolsDB);

                DataTableReader dr = dt.CreateDataReader();
                while (dr.Read())
                    {
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "CREATE SYNONYM [dbo].[" + dr[2].ToString() + "] FOR " +
                                "[" + currServer + "].[" + EMRDB + "].[dbo].[" + dr[0].ToString() + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ex) {
                            //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ", <<ucDatabaseManagement.cs: CreateTables, [1st try]>> : " + ex.Message, clsGbl.applicationName, 0);
                            UpdateLog("Synonyms Error - " + ex.Message); }
                    }
                //End Create Synonyms

              try
                {
                    if (emr == "iqcare" || emr == "ctc2")
                    {
                        //Create Decoded mst_Patient
                        if (emr == "iqcare")
                        {
                            #region mst_patient

                            i = 0;


                            i = 0;
                            string decodeMstPatient = "Open symmetric key Key_CTC decryption by password='ttwbvXWpqb5WOLfLrBgisw==';" +
                                                    "Select *, " +
                                                    " convert(varchar(100),decryptbykey(MiddleName)) dMiddleName," +
                                                    " convert(varchar(100),decryptbykey(firstname)) dFirstName, convert(varchar(100),decryptbykey(LastName))dLastName," +
                                                    " convert(varchar(100),decryptbykey(Address))dAddress, convert(varchar(100),decryptbykey(Phone)) dPhone" +
                                                    " INTO " + IQToolsDB + ".dbo.mst_patient_decoded  From " +
                                                    " mst_patient Where mst_patient.deleteflag is null or mst_patient.deleteflag=0;" +
                                                    " Close symmetric key Key_CTC";
                            try { i = (int)en.ReturnObject(iqcConnString, ClsUtility.theParams, decodeMstPatient, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                            catch (Exception ex)
                            {
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: CreateTables, [3rd try]>> : " + ex.Message, clsGbl.applicationName, 0);
                                UpdateLog("Mst_patient Error " + ex.Message);
                            }

                            #endregion
                        }
                        i = 0;

                        //Create Base Tables////////////////
                        #region runSPs
                        //if (emr == "iqcare")
                        //{
                        //    DataRow fDr = (DataRow)en.ReturnObject(iqcConnString, ClsUtility.theParams, "SELECT TOP 1 FacilityName FROM mst_Facility WHERE DeleteFlag = 0", ClsUtility.ObjectEnum.DataRow, "mssql");
                        //    ClsUtility.AddParameters("@FacilityName", SqlDbType.Text, fDr[0].ToString());
                        //    ClsUtility.AddParameters("@EMR", SqlDbType.Text, "iqcare");
                        //    ClsUtility.AddParameters("@EMRVersion", SqlDbType.Text, "3.5");
                        //    ClsUtility.AddParameters("@VisitPK", SqlDbType.Text, "0");
                        //    ClsUtility.AddParameters("@PatientPK", SqlDbType.Text, "0");
                        //}
                        //else if (emr == "ctc2")
                        //{
                        //    ClsUtility.AddParameters("@FacilityName", SqlDbType.Text, "");
                        //    ClsUtility.AddParameters("@EMR", SqlDbType.Text, "ctc2");
                        //    ClsUtility.AddParameters("@EMRVersion", SqlDbType.Text, "");
                        //    ClsUtility.AddParameters("@PatientPK", SqlDbType.Text, "0");
                        //    ClsUtility.AddParameters("@VisitPK", SqlDbType.Text, "0");
                        //}

                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_RefreshIQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        }
                        catch (Exception ex) { UpdateLog(IQToolsDB + " pr_RefreshIQTools - " + ex.Message); }

                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateSiteDetails_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateSiteDetails_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePatientMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreatePatientMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePharmacyMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreatePharmacyMaster_IQTools " + ex.Message); }

                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateClinicalEncountersMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateClinicalEncountersMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateLastStatusMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateLastStatusMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateARTPatientsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateARTPatientsMaster_IQTools " + ex.Message); }

                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateLabMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateLabMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePregnanciesMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreatePregnanciesMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateOIsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateOIsMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateTBPatientsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateTBPatientsMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateHEIMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateHEIMaster_IQTools " + ex.Message); }
                        //try
                        //{

                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateANCMothersMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateANCMothersMaster_IQTools " + ex.Message); }
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateFamilyInfoMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateFamilyInfoMaster_IQTools " + ex.Message); }
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateIQToolsViews_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateIQToolsViews_IQTools " + ex.Message); }
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateLastReportStatusMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_CreateLastReportStatusMaster_IQTools " + ex.Message); }


                        ClsUtility.Init_Hashtable();
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_FlattenPharmacy_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_FlattenPharmacy_IQTools " + ex.Message); }
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_FlattenLabs_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_FlattenPharmacy_IQTools " + ex.Message); }
                        //try
                        //{
                        //    i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_FlattenVisits_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                        //}
                        //catch (Exception ex) { UpdateLog(IQToolsDB + " pr_FlattenPharmacy_IQTools " + ex.Message); }

                        #endregion RunSPs
                    }

                    else
                    {
                        #region OldWay
                        //Recreate TEMP Tables Soon to be depracated
                        double tableCount = 0;
                        double progress = 0;
                        double div = 0;
                        string tblName = "";
                        string qryDef = "";

                        DataRow drow = (DataRow)en.ReturnObject(iqtConnString, ClsUtility.theParams, "SELECT Count(qryID) Total FROM aa_queries where MkTable Is Not Null and deleteflag is null", ClsUtility.ObjectEnum.DataRow, "mssql");
                        int numberOfTables = (int)drow[0];

                        dt.Clear();
                        dt = (DataTable)en.ReturnObject(iqtConnString, ClsUtility.theParams, "SELECT qryName, qryDefinition From aa_queries where MkTable Is Not Null and deleteflag is null Order By MkTable", ClsUtility.ObjectEnum.DataTable, "mssql");
                        dr = dt.CreateDataReader();

                        while (dr.Read())
                        {
                            tableCount = tableCount + 1;
                            div = tableCount / numberOfTables;
                            progress = div * 100;
                            tblName = dr[0].ToString().Substring(4, dr[0].ToString().Length - 4);
                            qryDef = dr[1].ToString();
                            i = 0;
                            try { i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "DROP TABLE [tmp_" + tblName + "]", ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                            catch (Exception ex)
                            {
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: CreateTables, [6th try]>> : " + ex.Message, clsGbl.applicationName, 0);
                                UpdateLog("Drop Table Error " + tblName + ex.Message);
                            }
                            i = 0;
                            try { i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "SELECT * INTO tmp_" + tblName + " From " + dr[0].ToString(), ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql"); }
                            catch (Exception ex)
                            {
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: CreateTables, [7th try]>> : " + ex.Message, clsGbl.applicationName, 0);
                                UpdateLog("Create Table Error " + tblName + ex.Message);
                            }

                            i = 0;
                            string indexCreate = "";
                            if (emr == "iqcare")
                            {
                                indexCreate = "CREATE CLUSTERED INDEX [IDX_Ptn_pk] ON [dbo].[tmp_" + tblName + "] (	[Ptn_Pk] ASC " +
                                              ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF " +
                                              ", DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";

                            }
                            else if (emr == "ctc")
                            {
                                indexCreate = "CREATE CLUSTERED INDEX [IDX_PatientPK] ON [dbo].[tmp_" + tblName + "] (	[PatientPK] ASC " +
                                              ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF " +
                                              ", DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
                            }
                            try
                            {
                                i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, indexCreate, ClsUtility.ObjectEnum.ExecuteNonQuery, "mssql");
                            }
                            catch (Exception ex)
                            {
                                //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: CreateTables, [8th try]>> : " + ex.Message, clsGbl.applicationName, 0);
                                UpdateLog("Create Indexes Error " + tblName + ex.Message);
                            }

                            //UpdateProgress((int)progress);

                        }
                        #endregion OldWay
                    }

                    
                }
                catch (Exception ex) {
                    //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ",<<ucDatabaseManagement.cs: CreateTables, [Main try]>> : " + ex.Message, clsGbl.applicationName, 0);
                    UpdateLog(ex.Message); }
            }
            #endregion

            #region ISANTE
            else if (emr == "isante")
            {
                SetControlPropertyThreadSafe(fMain.lblProgress, "Text", "Creating TEMP Tables on " + IQToolsDB);
                string isanteConnString = clsGbl.mySQLcnnstr + ";Database=" + EMRDB + ";";
                string iqtoolsConnString = clsGbl.mySQLcnnstr + ";Database=" + IQToolsDB + ";";
                int i = 0;
                string toCreate = "SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES " +
                                  "WHERE TABLE_SCHEMA = '"+EMRDB+"'";
                string sql = "";
                Entity en = new Entity();
                localhost.Service1 sn = new localhost.Service1 ( );
                ClsUtility.Init_Hashtable();
                DataTable dt = new DataTable();

                dt = (DataTable)en.ReturnObject(isanteConnString, ClsUtility.theParams, toCreate, ClsUtility.ObjectEnum.DataTable, "mysql");
                DataTableReader dr = dt.CreateDataReader();

                while (dr.Read())
                {
                    try
                    {
                        sql = "CREATE OR REPLACE VIEW `" + dr[0].ToString() + "` AS SELECT * FROM `" + EMRDB + "`.`" + dr[0].ToString() + "`;";
                        i = (int)en.ReturnObject(iqtoolsConnString, ClsUtility.theParams, sql, ClsUtility.ObjectEnum.ExecuteNonQuery, "mysql");
                    }
                    catch (Exception ex) {
                        //sn.ErrorLogging(clsGbl.cnnstr + ";Initial Catalog = " + clsGbl.iqtoolsDB + ", <<ucDatabaseManagement.cs: CreateTables, [1st in ELSE]>> : " + ex.Message, clsGbl.applicationName, 0);
                        UpdateLog("Create View Error " + ex.Message); }
                }
                dr.Dispose();
                createBaseTables(emr, IQToolsDB, EMRDB);              

            }
            #endregion
        }

        private void btnTruncate_Click(object sender, EventArgs e)
        {
            string cstr = clsGbl.cnnstr;
            string currServer = cstr.Substring(cstr.IndexOf("Data Source=") + 12, cstr.IndexOf(";") - 12);
            //MessageBox.Show(currServer);
            //fMain.lblProgress.Text = "Hello";
            //fMain.picProgress.Image = Properties.Resources.progress2;
            Server sqlServer = new Server(currServer);
            if (sqlServer.Settings.DefaultFile == "")
                MessageBox.Show(sqlServer.Information.RootDirectory + "\\DATA");
            else MessageBox.Show(sqlServer.Settings.DefaultFile);
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clbDBs.Items.Count; i++)
            {
                clbDBs.SetItemChecked(i, chkSelect.Checked);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdateVersion_Click(object sender, EventArgs e)
        {

        }

        private void updateEMR(string version, string emr, string[] selectedDBs)
        {
            Entity en = new Entity();
            ClsUtility.Init_Hashtable();
            if (emr == "iqcare")
            {
                foreach (string db in selectedDBs)
                {

                }
            }
        }

        private void createBaseTables(string emr, string iqtoolsDB, string emrDB)
        {
            string emrConnString = getConnString(clsGbl.mainServerType.ToLower(), emrDB);
            string iqtConnString = getConnString(clsGbl.mainServerType.ToLower(), iqtoolsDB);
            //

            //Create Synonyms
            int i = 0;
            DataTable dt = new DataTable();

            Entity en = new Entity();
            ClsUtility.Init_Hashtable();
            if (emr == "iqcare")
                        {
                            DataRow fDr = (DataRow)en.ReturnObject(emrConnString, ClsUtility.theParams
                                , "SELECT TOP 1 FacilityName FROM mst_Facility WHERE DeleteFlag = 0", ClsUtility.ObjectEnum.DataRow, "mssql");
                            ClsUtility.AddParameters("@FacilityName", SqlDbType.Text, fDr[0].ToString());
                            ClsUtility.AddParameters("@EMR", SqlDbType.Text, "iqcare");
                            ClsUtility.AddParameters("@EMRVersion", SqlDbType.Text, "");
                        }
            else if (emr == "ctc2" && clsGbl.mainServerType.ToLower() == "mssql")
                        {
                            ClsUtility.AddParameters("@FacilityName", SqlDbType.Text, "");
                            ClsUtility.AddParameters("@EMR", SqlDbType.Text, "ctc2");
                            ClsUtility.AddParameters("@EMRVersion", SqlDbType.Text, "");
                            ClsUtility.AddParameters("@PatientPK", SqlDbType.Text, "0");
                            ClsUtility.AddParameters("@VisitPK", SqlDbType.Text, "0");
                        }
            else if (emr == "isante")
            {
                ClsUtility.AddParameters("FacilityName", SqlDbType.Text, "");
                ClsUtility.AddParameters("EMR", SqlDbType.Text, "isante");
                ClsUtility.AddParameters("EMRVersion", SqlDbType.Text, "");
            }

                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateSiteDetails_IQTools"
                                , ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateSiteDetails_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePatientMaster_IQTools"
                                , ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreatePatientMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePharmacyMaster_IQTools"
                                , ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreatePharmacyMaster_IQTools " + ex.Message); }
                       
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateClinicalEncountersMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateClinicalEncountersMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateLastStatusMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateLastStatusMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateARTPatientsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateARTPatientsMaster_IQTools " + ex.Message); }

                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateLabMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateLabMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreatePregnanciesMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreatePregnanciesMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateOIsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateOIsMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateTBPatientsMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateTBPatientsMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateHEIMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateHEIMaster_IQTools " + ex.Message); }
                        try
                        {

                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateANCMothersMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateANCMothersMaster_IQTools " + ex.Message); }
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateFamilyInfoMaster_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateFamilyInfoMaster_IQTools " + ex.Message); }
                        try
                        {
                            i = (int)en.ReturnObject(iqtConnString, ClsUtility.theParams, "pr_CreateIQToolsViews_IQTools", ClsUtility.ObjectEnum.ExecuteNonQuery, clsGbl.mainServerType.ToLower());
                        }
                        catch (Exception ex) { UpdateLog(iqtoolsDB + " pr_CreateIQToolsViews_IQTools " + ex.Message); }
                        ClsUtility.Init_Hashtable();
                        
        }

        private string getConnString(string server, string DB)
        {
            if (server.ToLower() == "mssql")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Application.StartupPath + "\\app.config");
                XmlNode node = xmlDoc.SelectSingleNode("//add[@key='msSQLConnString']");
                if (node != null)
                {
                    return node.Attributes["value"].Value + ";Inital Catalog=" + DB;
                }
                return "";
            }
            else if (server.ToLower() == "mysql")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Application.StartupPath + "\\app.config");
                XmlNode node = xmlDoc.SelectSingleNode("//add[@key='mySQLconnString']");
                if (node != null)
                {
                    return node.Attributes["value"].Value + ";Database=" + DB;
                }
                return "";
            }
            else return System.Configuration.ConfigurationManager.ConnectionStrings["msSQLconnString"].ConnectionString + ";Initial Catalog=" + DB;
        }
    
    }
}
