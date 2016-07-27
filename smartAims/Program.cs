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
        static object m()
        {
            int k = 11;
            return k;
        }
        static void Main(string[] args)
        {

            int k = 10;
            object k2 = k;

            //int k3 =() m();

            Console.WriteLine("test start");

            Controller controller = new Controller(@"DBaims.db");

            DataTable dataTable;

            controller.GetAims( out dataTable );
            //controller.GetAims();

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
