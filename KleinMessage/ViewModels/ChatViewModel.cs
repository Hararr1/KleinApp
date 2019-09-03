using Caliburn.Micro;
using KleinAppDesktopUI.Library.ChatServer;
using KleinAppDesktopUI.Library.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Action = System.Action;

namespace KleinMessage.ViewModels
{
    public class ChatViewModel : Screen
    {
        ILoggedInUserModel _user;
        IConnectionToServerModel _connection;
        public ChatViewModel (ILoggedInUserModel user, IConnectionToServerModel connection)
            {
                // Firstly we need just connecting to us server. On the next step we create proxy between server and client to special Hub.             
                _user = user;
                _connection = connection;          
            }          

    }
}
