
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerKleinApp.Hubs
{
    //After choose to who take the data ( something like this : Clients.All) we need to name our method (for example "displayTime").
    public class ChatHub : Hub
    {
        public void ServerTime()
        {
            do
            {               
                Clients.All.displayTime($"{DateTime.UtcNow.ToString("H:mm:ss")}");
                Thread.Sleep(TimeSpan.FromSeconds(1));      
               

            } while (true);
        }

        public void LogOnSever(string username)
        {
            Console.WriteLine($"{username} is connected", Console.ForegroundColor = ConsoleColor.Green);
        }
    }

        

  
}
