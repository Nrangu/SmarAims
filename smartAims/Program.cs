using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartAimsDomain;
namespace smartAims
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("test start");

            Controller controller = new Controller();

            DataTable dataTable;

            controller.GetAims(out dataTable);

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
