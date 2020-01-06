using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Windows.Forms;

namespace Janus_Test
{
    class 
        SqliteData
    {
        private string _connectionString;
        private SQLiteConnection sqlite;

        public SqliteData()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            this._connectionString = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
            sqlite = new SQLiteConnection(this._connectionString);

        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            sqlite.Close();
            return dt;
        }
    }
}