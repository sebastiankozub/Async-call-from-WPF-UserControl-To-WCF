using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Interviews.VM.PriceHumanizer.ServiceDefinition.Contracts;

namespace Interviews.VM.PriceHumanizer.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHost();
        }

        private static void RunHost()
        {
            using (ServiceHost host = new ServiceHost(
                typeof(PriceHumanizerService)))
            {
                Console.WriteLine("PriceHumanizerService host starting...");
                host.Open();
                Console.WriteLine("PriceHumanizerService working.");
                Console.WriteLine("Press [ENTER] to exit...");
                Console.ReadKey();
            }
        }
    }
}
