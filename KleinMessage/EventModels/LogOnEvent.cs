using KleinAppDesktopUI.Library.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.EventModels
{
    public class LogOnEvent
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LogOnEvent(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
