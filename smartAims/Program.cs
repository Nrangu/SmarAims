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
            //Domains domain = new Domains();
            //domain.GetData();
            Controller controller = new Controller();
            controller.GetAims();
            Console.WriteLine("ok");
            controller.GetAims();
            Console.ReadKey();
        }
    }
}
