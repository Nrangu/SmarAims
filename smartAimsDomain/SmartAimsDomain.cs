using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.IO;

namespace smartAimsDomain
{
    public class SQLiteDomain
    {
        private SQLiteConnection connection = null;
        private string DBname = null;

        public bool isConnected {
            get{  return connection !=null?  true: false; }    
        }

        public bool connect(string DBname)
        {
            this.DBname = DBname;

            connection = new SQLiteConnection(string.Format("Data Source={0};", this.DBname));
            connection.Open();
            return true;
        }
        public void GetAims()
        {
            //Console.WriteLine("Smart aims Domains");
            //string databaseName = @"DBaims.db";
            //SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            //connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'aims';", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                Console.WriteLine(string.Format("{0} {1} {2} ", record["id"], record["title"], record["description"]));
            }
        }
    }
    public class Controller
    {
        private SQLiteDomain domain = new SQLiteDomain();
        public  void GetAims()
        {
            if ( !domain.isConnected)
            {
                domain.connect(@"DBaims.db");
            }

            domain.GetAims();
        }
    }
}
