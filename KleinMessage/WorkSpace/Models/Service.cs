using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
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
        public event EventHandler IsSomebodyLoggedHandler;
        public event EventHandler TakeTextMessageHandler;
        //public event EventHandler TimeHandler;



        public async Task Connected()
        {
            _serverConnection = new ConnectionToServerModel();
            _serverConnection._connection = new HubConnection(url);
            _serverConnection._proxy = _serverConnection._connection.CreateHubProxy("ChatHub");

            _serverConnection._proxy.On<User>("WhoIsLogin", (u) => IsSomebodyLoggedHandler?.Invoke(u, EventArgs.Empty));
            _serverConnection._proxy.On<User, string>("TakeMessage", (user, message) => TakeTextMessageHandler?.Invoke(new object[] { user, message }, EventArgs.Empty));

            //_serverConnection._proxy.On<List<User>>("LogOnSever", (users) => LogOnServerHandler?.Invoke(users, EventArgs.Empty));
            //_serverConnection._proxy.On<User>("WhoIsLogin", (u) => IsSomebodyLoggedHandler?.Invoke(u, EventArgs.Empty));
            //_serverConnection._proxy.On<List<User>>("LogOnSever", (users) => LogOnServerHandler?.Invoke(users, EventArgs.Empty));
            //_serverConnection._proxy.On<User>("WhoIsLogin", (u) => IsSomebodyLoggedHandler?.Invoke(u, EventArgs.Empty));

            //_serverConnection._proxy.On<User>("WhoIsLogin",(u) => IsSomebodyLoggedHandler?.Invoke(u, EventArgs.Empty));

            ////_serverConnection._proxy.On<string, string>("UnicastTextMessage", ()

            ////_serverConnection._proxy.On<string, string>("UnicastTextMessage", (n, m) => MessageHandling?.Invoke(n, m));
            ////_serverConnection._proxy.On<string>("displayTime", (time) => TimeHandling?.Invoke(time));
            await _serverConnection._connection.Start();
        }

        public async Task<List<User>> LogOnServer()
        {
            List<User> UsersList = await _serverConnection._proxy.Invoke<List<User>>("LogInInvokeServer", new object[] { ApplicationItemsCollection.Logged?.FirstName, ApplicationItemsCollection.Logged?.UserId } );

            return UsersList;
        }

        public async Task SendMessage(string whoSendMessageIDApi, string WhoTakeMessage, string IDApi, string message)
        {
            await _serverConnection._proxy.Invoke("SendMessage", new object[] { whoSendMessageIDApi,  WhoTakeMessage,  IDApi,  message });

        }

        public async Task Disconnected()
        {
            await _serverConnection._proxy.Invoke("Disconnect", ApplicationItemsCollection.Logged?.UserId);
        }

        //public async Task SendBroadcastMessageAsync(string msg)
        //{
        //    await _serverConnection._proxy.Invoke("BroadcastTextMessage", msg);
        //}
  
        //public async Task<string> GetTime()
        //{          
        //     return  await _serverConnection._proxy.Invoke<string>("ServerTime");         
        //}
    }
}
