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
    public class Domains
    {
        public void GetData()
        {
            Console.WriteLine("Smart aims Domains");
            string databaseName = @"cyber.db";
            SQLiteConnection. CreateFile(databaseName);
            Console.WriteLine(File.Exists(databaseName) ? "База данных создана" : "Возникла ошиюка при создании базы данных");
            Console.ReadKey(true);
        }
    }
}
