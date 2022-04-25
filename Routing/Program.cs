using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Routing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceHost(typeof(RoutingService));
            Console.WriteLine("start routing");
            service.Open();
            Console.ReadLine();
        }
    }
}
