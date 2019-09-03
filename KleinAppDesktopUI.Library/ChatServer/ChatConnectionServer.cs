using KleinAppDesktopUI.Library.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinAppDesktopUI.Library.ChatServer
{
    public class ChatConnectionServer : IChatConnectionServer
    {
        private HubConnection HubConnection;
        private IHubProxy proxy;
        public string serverTime;
        private ILoggedInUserModel _loggedInUser;

        public ChatConnectionServer(ILoggedInUserModel loggedInUser)
        {
            Initliazie();
            _loggedInUser = loggedInUser;
        }

        private void Initliazie()
        {
            string url = ConfigurationManager.AppSettings["server"];
            HubConnection = new HubConnection(url);
            proxy = HubConnection.CreateHubProxy("ChatHub");

            try
            {
                HubConnection.Start().Wait();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetServerTime()
        {
            try
            {
                var result = proxy.On<string>("ServerTime", time =>

               {
                   serverTime = time;
               });


            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
