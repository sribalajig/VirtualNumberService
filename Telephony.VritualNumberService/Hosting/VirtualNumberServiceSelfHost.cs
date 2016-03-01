using System;
using Nancy.Hosting.Self;

namespace Telephony.VritualNumberService.Hosting
{
    public class Hosting
    {
        public static void Main()
        {
            var host = new NancyHost(new Uri("http://localhost:12345/telephony/"));

            host.Start();

            Console.ReadKey();

            host.Stop();
        }
    }
}