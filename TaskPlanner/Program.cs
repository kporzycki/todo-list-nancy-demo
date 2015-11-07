using System;
using Nancy.Hosting.Self;

namespace TaskPlanner
{
    class Program
    {
        static void Main()
        {
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            using (var nancyHost = new NancyHost(new Uri("http://localhost:8888/"), new Bootstrapper(), hostConfigs))
            {
                nancyHost.Start();
                Console.WriteLine("Nancy now listening. Press enter to stop.");
                Console.ReadLine();
            }
        }
    }
}
