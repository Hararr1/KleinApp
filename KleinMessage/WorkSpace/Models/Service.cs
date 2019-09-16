using KleinAppDesktopUI.Library.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.WorkSpace.Models
{
    public class Service : IService
    {
        private IConnectionToServerModel _serverConnection;
        private string url = ConfigurationManager.AppSettings["server"];


        public event Action<string, string> MessageHandling;
        public event Action<string> TimeHandling;




        public async Task Connected()
        {
            _serverConnection = new ConnectionToServerModel();
            _serverConnection._connection = new HubConnection(url);
            _serverConnection._proxy = _serverConnection._connection.CreateHubProxy("ChatHub");

            _serverConnection._proxy.On<string, string>("BroadcastTextMessage", (n, m) => MessageHandling?.Invoke(n, m));
            _serverConnection._proxy.On<string, string>("UnicastTextMessage", (n, m) => MessageHandling?.Invoke(n, m));
            _serverConnection._proxy.On<string>("displayTime", (time) => TimeHandling?.Invoke(time));
            await _serverConnection._connection.Start();
        }

        public async Task SendUnicastMessageAsync(string friend, string message)
        {
            await _serverConnection._proxy.Invoke("UnicastImageMessage", new object[] { friend, message });
        }

        public async Task SendBroadcastMessageAsync(string msg)
        {
            await _serverConnection._proxy.Invoke("BroadcastTextMessage", msg);
        }
        public async Task LogIn()
        {
            if(ApplicationItemsCollection.Logged.FirstName != null)
            {
             await _serverConnection._proxy.Invoke("LogOnSever", ApplicationItemsCollection.Logged.FirstName);
            }

        }
        public async Task GetTime()
        {          
                await _serverConnection._proxy.Invoke("ServerTime");         
        }
    }
}
