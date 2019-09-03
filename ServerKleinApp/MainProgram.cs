using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerKleinApp
{
    class MainProgram
    {
        static void Main(string [] args)
        {
            string url = "http://localhost:5050";
            using(WebApp.Start<Startup>(url))
            {
                Console.WriteLine($" Services started at: {DateTime.UtcNow:D} at Url: {url}");
                Console.ReadLine();
            }
            
        }
    }
}
