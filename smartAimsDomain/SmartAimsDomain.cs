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
        private SQLiteConnection _connection = null;
        private SQLiteCommand _command = null;
        private string _DBname = null;
        private string _TableAims = "aims";

        public bool isConnected {
            get{  return _connection !=null?  true: false; }    
        }

        public bool connect(string DBname)
        {
            this._DBname = DBname;

            _connection = new SQLiteConnection(string.Format("Data Source={0};", this._DBname));
            _connection.Open();
            return true;
        }

        public string[] GetFields()
        {
            
            _command = new SQLiteCommand("SELECT * FROM 'aims';", _connection);

            SQLiteDataReader reader = _command.ExecuteReader();

            string[] fields = new string[reader.FieldCount];

            for (int i = 0; i < reader.FieldCount ;  i++)
            {
                fields[i] = reader.GetName(i);
            }
            return fields; 

        }

        public DataTable GetAims()
        {
            return GetTableData(_TableAims);
        }
        
        private DataTable GetTableData( string tableName)
        {

//            _command = new SQLiteCommand("SELECT Count(*) FROM 'aims';", _connection);
            _command = new SQLiteCommand("SELECT Count(*) FROM '"+ tableName +"';", _connection);


            object reader1 = _command.ExecuteScalar();
            int count = Convert.ToInt32(reader1.ToString());


            _command = new SQLiteCommand("SELECT * FROM '" + tableName +"'", _connection);

            SQLiteDataReader reader = _command.ExecuteReader();

            string[] fields = new string[reader.FieldCount];


            for (int i = 0; i < reader.FieldCount; i++)
            {
                fields[i] = reader.GetName(i);
            }

            object[,] records = new object[count, reader.FieldCount];

            int k = 0;
            foreach (DbDataRecord record in reader)
            {
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    records[k, j] = record[j];
                }
                k++;
            }

            return new DataTable(fields, records, count);
        }
    }
    /// <summary>
    ///  TO DO
    /// did 1. Передавать в конструктор количество полей и строк
    /// did 2. Реализовать свойства для получения количества полей и строк
    ///     3. По возможности реализовать возможность перебора полей используя foreach
    ///     4. По возможности реализовать возможность перебора строк используя foreach
    /// </summary>
    public class DataTable
    {
        private string[] _fields;
        private object[,] _data;
        private int _countRows = 0;


        protected DataTable() { }

        public DataTable(string[] fields, object[,] data, int countRows)
        {
            _fields = fields;
            _data = data;
            _countRows = countRows;
        }

        public int CountFields
        {
            get
            {
                return _fields.Length;
            }
        }

        public int CountRows
        {
            get
            {
                return _countRows;
            }
        }

        // проверяем есть ли поле с именем index в списке полей
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

        // получаем имя поля по индексу
        public string this[int index]
        {
            get
            {
                return _fields[index];
            }
        }

        // по номеру строки и имени поля получаем данные
        /// <summary>
        /// TO DO 
        /// did реализовать такое же свойство для получения
        ///     данных по номеру строки и индексу
        /// </summary>
        /// <param name="index"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public object this[int index , string field]{
            get{ /// TODO сделать проверки на ошибки!!!
                return _data[index, fieldNumber(field)];
            }
        }

        public object this[int index, int fieldIndex]
        {
            get
            {
                return _data[index, fieldIndex];
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

    /// <summary>
    /// TO DO
    /// DID 1. Передавать базу как параметр
    /// 2. Передавать таблицу как параметр
    /// 3. Реализовать передачу имени таблицы 
    ///    в запрос через параметры.
    /// </summary>
    public class Controller
    {
        private SQLiteDomain domain = new SQLiteDomain();

        private string DBName;

        public Controller(string DBName)
        {
            this.DBName = DBName;
        }

        public string[] GetFileds()
        {
            if (!domain.isConnected)
            {
                domain.connect(DBName);
            }
            return domain.GetFields();
        }
/*
        public void GetAims()
        {
            if (!domain.isConnected)
            {
                domain.connect(DBName);
            }

            domain.GetAims();
        }
        */
        public void GetAims(out DataTable dataTable)
        {
            if (!domain.isConnected)
            {
                domain.connect(DBName);
            }

            dataTable = domain.GetAims( );

        }
    }
}
