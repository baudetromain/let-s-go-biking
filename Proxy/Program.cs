﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceHost(typeof(ProxyService));
            Console.WriteLine("start proxy");
            service.Open();
            Console.ReadLine();
        }
    }
}
