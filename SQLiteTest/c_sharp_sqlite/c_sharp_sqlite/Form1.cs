using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace c_sharp_sqlite
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\myData.db"))
            {
                SQLiteConnection.CreateFile("myData.db");
            }

            string name = textBox1.Text;
            string phone_number = textBox2.Text;

            //建立資料庫連線
            sqlite_conn = new SQLiteConnection("Data Source=myData.db");
            sqlite_conn.Open();

            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText= sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS phone 
                                                    (num INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, phone_number TEXT)";

            //create table header
            //INTEGER PRIMARY KEY AUTOINCREMENT=>auto increase index
            sqlite_cmd.ExecuteNonQuery(); //using behind every write cmd

            sqlite_cmd.CommandText = "INSERT INTO phone VALUES (null, '" + name + "','" + phone_number + "');";
            sqlite_cmd.ExecuteNonQuery();//using behind every write cmd

            sqlite_conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //建立資料庫連線
            sqlite_conn = new SQLiteConnection("Data Source=myData.db");
            sqlite_conn.Open();//Open the connection

            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM phone";//select table

            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())//read every data
            {
                string name = sqlite_datareader["name"].ToString();
                string phone_no= sqlite_datareader["phone_number"].ToString();

                MessageBox.Show(name + " : " + phone_no);
            }

            sqlite_conn.Close();
        }
    }
}
