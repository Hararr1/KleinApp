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
        public ILoggedInUserModel _user;
        public IConnectionToServerModel _serverModel;
        public LogOnEvent(ILoggedInUserModel user, IConnectionToServerModel serverModel)
        {
            _user = user;
            _serverModel = serverModel;
        }
    }
}
