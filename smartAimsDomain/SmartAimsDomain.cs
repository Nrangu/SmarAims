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
        private SQLiteCommand command = null;
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

        public string[] GetFields()
        {
            
            command = new SQLiteCommand("SELECT * FROM 'aims';", connection);

            SQLiteDataReader reader = command.ExecuteReader();

            string[] fields = new string[reader.FieldCount];

            for (int i = 0; i < reader.FieldCount ;  i++)
            {
                fields[i] = reader.GetName(i);
            }
            return fields; 

        }

        public void GetAims()
        {
            string[] str;
            command = new SQLiteCommand("SELECT Count(*) FROM 'aims';", connection);

            object reader1 = command.ExecuteScalar();
            int count = Convert.ToInt32(reader1.ToString());


            Console.WriteLine("count = {0}", count);

            command = new SQLiteCommand("SELECT * FROM 'aims';", connection);
            


            SQLiteDataReader reader = command.ExecuteReader();

            string[] fields = new string[ reader.FieldCount];

            
            for ( int i = 0;  i < reader.FieldCount; i++)
            {
                Console.WriteLine(reader.GetName(i));
                fields[i] = reader.GetName(i);
            }

            object[,] records = new object[count, reader.FieldCount];

             int k = 0;
            foreach (DbDataRecord record in reader)
            {
                Console.WriteLine(string.Format("{0} {1} {2} ", record["id"].GetType(), record["title"], record["description"]));
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    records[k, j] = record[j];
                }
                k++;
            }
        }
        public DataTable GetData()
        {
          
            command = new SQLiteCommand("SELECT Count(*) FROM 'aims';", connection);

            object reader1 = command.ExecuteScalar();
            int count = Convert.ToInt32(reader1.ToString());


            Console.WriteLine("count = {0}", count);

            command = new SQLiteCommand("SELECT * FROM 'aims';", connection);



            SQLiteDataReader reader = command.ExecuteReader();

            string[] fields = new string[reader.FieldCount];


            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine(reader.GetName(i));
                fields[i] = reader.GetName(i);
            }

            object[,] records = new object[count, reader.FieldCount];

            int k = 0;
            foreach (DbDataRecord record in reader)
            {
                Console.WriteLine(string.Format("{0} {1} {2} ", record["id"].GetType(), record["title"], record["description"]));
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    records[k, j] = record[j];
                }
                k++;
            }

            return new DataTable(fields, records);
        }
    }

    public class DataTable
    {
        private string[] _fields;
        private object[,] _data;

        protected DataTable() { }

        public DataTable(string[] fields, object[,] data)
        {
            _fields = fields;
            _data = data;
        }

        public bool this[string index] {
            get {

                for (int i = 0; i < _fields.Length; i++)
                {
                    if (index == _fields[i])
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        public string this[int index]
        {
            get
            {
                return _fields[index];
            }
        }

        public object this[int index , string field]{
            get{ /// TODO сделать проверки на ошибки!!!
                return _data[index, fieldNumber(field)];
            }
        }

        protected int fieldNumber(string field)
        {
            for( int i = 0; i < _fields.Length; i++  )
            {
                if ( field == _fields[i] ) {
                    return i;
                }
            }
            return -1;
        }
    }


    public class Controller
    {
        private SQLiteDomain domain = new SQLiteDomain();

        private string DBName;

        public Controller()
        {
            DBName = @"DBaims.db";
        }

        public string[] GetFileds()
        {
            if (!domain.isConnected)
            {
                domain.connect(DBName);
            }
            return domain.GetFields();
        }

        public  void GetAims()
        {
            if ( !domain.isConnected)
            {
                domain.connect(DBName);
            }

            domain.GetAims();
        }

        public void GetAims(out DataTable dataTable)
        {
            if (!domain.isConnected)
            {
                domain.connect(DBName);
            }

            dataTable = domain.GetData();

        }
    }
}
