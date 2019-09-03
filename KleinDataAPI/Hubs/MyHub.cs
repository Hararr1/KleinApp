using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KleinDataAPI.Hubs
{
    public class MyHub : Hub
    {


        public void Send (string name, string message)
        {
            Clients.All.addMessage(name, message);
        }


    }
}