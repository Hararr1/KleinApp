﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ServerKleinApp.Models;
using System;
using System.Collections.Concurrent;
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
        private static ConcurrentDictionary<string, User> ChatClients = new ConcurrentDictionary<string, User>();

        public void ServerTime()
        {
            do
            {
                Clients.All.displayTime($"{DateTime.Now.ToString("H:mm:ss")}");
                Thread.Sleep(TimeSpan.FromSeconds(1));


            } while (true);
        }


        public void LogOnSever(string username)
        {
            if (ChatClients.ContainsKey(username) == false)
            {
                User user = new User { ID = Context.ConnectionId, Name = username };
                bool isAdded = ChatClients.TryAdd(username, user);
                if(isAdded == false)
                {
                    // return "nie udało się połączyć z serwerem!";
                    return;
                }
                Clients.CallerState.UserName = username;
                
            Console.WriteLine($"{username} is connected", Console.ForegroundColor = ConsoleColor.Green);
            }
            return;
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

        public void UnicastTextMessage(string recepient, string message)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender) && recepient != sender &&
                !string.IsNullOrEmpty(message) && ChatClients.ContainsKey(recepient))
            {
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).UnicastTextMessage(sender, message);
            }
        }
        public void BroadcastTextMessage(string message)
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
                Clients.Others.BroadcastTextMessage(name, message);
            }
        }





    }
}

        

  

