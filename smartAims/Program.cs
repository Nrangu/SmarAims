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
            Domains domain = new Domains();
            domain.GetData();
            Console.ReadKey();
        }
    }
}
