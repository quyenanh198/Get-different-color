using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GenAPN
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Main : Window
    {
        public static string strConn;
        public static SqlDataAdapter SDA;
        public static DataSet DS;

        ObservableCollection<Units> DB = new ObservableCollection<Units>();

        public Main()
        {
            InitializeComponent();

            strConn = GenAPN.Properties.Settings.Default.LocalDBConnectionString;
            //Tes();
        }


        public string Tes()
        {
            string hdd = "";

            SqlConnection cn = new SqlConnection(GenAPN.Properties.Settings.Default.Database1ConnectionString);
            
                cn.Open();

                using (SqlCommand QryCmd = cn.CreateCommand())
                {
                    QryCmd.CommandText = " SELECT * FROM RegionRef ";
                    SDA = new SqlDataAdapter(QryCmd.CommandText, cn);
                    DS = new DataSet();
                    SDA.Fill(DS, "TE");
                    string tmp, tmp2;
                    if (DS.Tables["TE"].Rows.Count != 0)
                    {
                     tmp = DS.Tables["TE"].Rows[0].ItemArray[0].ToString();
                     tmp2 = DS.Tables["TE"].Rows[0].ItemArray[1].ToString();
                    }

                    //QryCmd.ExecuteNonQuery();

                cn.Close();
                }
            


            return hdd;
        }

        public void ConnectListAndSaveSQLCompactExample()
        {
            // Create a connection to the file datafile.sdf in the program folder
            string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\LocalDB.sdf";
            SqlCeConnection connection = new SqlCeConnection("datasource=" + dbfile);

            // Read all rows from the table test_table into a dataset (note, the adapter automatically opens the connection)
            //SqlCeDataAdapter adapter = new SqlCeDataAdapter("select QTA_SN from Units_precheck", connection);
            //DataSet data = new DataSet();
            //adapter.Fill(data);

            connection.Open();

            SqlCeCommand cmd = connection.CreateCommand();
            cmd.CommandText = " SELECT * FROM Units_precheck ";
            //WHERE QTA_SN = 'C02K40WNDNCV' ";

            SqlCeDataReader reader = cmd.ExecuteReader();
            int index = 0;
            string lis;
            while (reader.Read())
            {
                //textBox1.Text += reader["QTA_SN"].ToString() + "\n";
                textBox1.Text += reader["RMACaseID"].ToString() + "\t";
                textBox1.Text += reader["QTA_SN"].ToString() + "\t";
                textBox1.Text += reader["Region"].ToString() + "\n";
                lis = reader["QTA_SN"].ToString();
                //dataGrid1.SetValue(
                index++;
            }
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            //dataGrid1.ItemsSource = cmd.ExecuteReader().Cast<DbDataRecord>().ToList();

            // Add a row to the test_table (assume that table consists of a text column)
            //data.Tables[0].Rows.Add(new object[] { "New row added by code" });

            // Save data back to the databasefile
            //adapter.Update(data);

            // Close 
            connection.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //ConnectListAndSaveSQLCompactExample();
            Tes();
        }

    }



    public class Units
    {
        public string OriSN { get; set; }
        public string SN { get; set; }
        public string CONFIG { get; set; }
        public string LCD { get; set; }
        public string Board { get; set; }
        public string RAM { get; set; }
        public int RAMQty { get; set; }
        public string RAMMLB { get; set; }
        public string VRAM { get; set; }
        public string HDD { get; set; }
        public string Region { get; set; }
        public int CompNum { get; set; }
        public string[] CompPN { get; set; }
    } 
}
