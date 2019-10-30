using Microsoft.AspNet.SignalR;
using ServerKleinApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerKleinApp.Hubs
{
    //After choose to who take the data ( something like this : Clients.All) we need to name our method (for example "displayTime").
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, User> ChatClients = new ConcurrentDictionary<string, User>();

        public void ServerTime()
        {
            do
            {

                Clients.All.displayTime($"{DateTime.Now.ToString("H:mm:ss")}");
                Thread.Sleep(TimeSpan.FromSeconds(1));

            } while (true);
        }

        public List<User> LogInInvokeServer(string username, string IDApi)
        {
            if (ChatClients.ContainsKey(IDApi) == false)
            {
                User user = new User { ID = Context.ConnectionId, Name = username, IDApi = IDApi};
                bool isAdded = ChatClients.TryAdd(IDApi, user);
                List<User> users = new List<User>(ChatClients.Values);

                if(isAdded == false)
                {
                    return null;
                }

                Clients.CallerState.UserName = username;        
                Clients.Others.WhoIsLogin(user);
                Console.WriteLine($"{username} is connected", Console.ForegroundColor = ConsoleColor.Green);

                return users;
            }
            return null;
        }

        public void SendMessage(string whoSendMessageIDApi, string WhoTakeMessage, string IDApi, string message)
        {
            User whoSendMessage = new User();
            ChatClients.TryGetValue(whoSendMessageIDApi, out whoSendMessage);

            if ( WhoTakeMessage != whoSendMessage.Name &&
                string.IsNullOrEmpty(message) == false && ChatClients.ContainsKey(IDApi))
            {
                User client = new User();
                ChatClients.TryGetValue(IDApi, out client);
                Clients.Client(client.ID).TakeMessage(whoSendMessage, message);

                Console.WriteLine($"{whoSendMessage.Name} send message to {client.Name}");
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantDisconnection(userName);
                Console.WriteLine($"{userName} is disconnected", Console.ForegroundColor = ConsoleColor.Red);
            }
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantReconnection(userName);
                Console.WriteLine($"== {userName} reconnected");
            }
            return base.OnReconnected();
        }

        //public void BroadcastTextMessage(string message)
        //{
        //    var name = Clients.CallerState.UserName;
        //    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
        //    {
        //        Clients.Others.BroadcastTextMessage(name, message);
        //    }
        //    Console.WriteLine("Ktoś odbiera");
        //}






    }
}

        

  

