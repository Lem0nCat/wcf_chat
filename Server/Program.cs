﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    internal class Program {
        static void Main(string[] args) {
            using (var host = new ServiceHost(typeof(Classes.ServiceChat))) {
                host.Open();
                Console.WriteLine("Host started!");
                Console.ReadLine();
            }
        }
    }
}
