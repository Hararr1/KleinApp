using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KleinMessage.WorkSpace.Models
{
    public interface IService
    {
        event EventHandler IsSomebodyLoggedHandler;
        //event EventHandler SendTextMessageHandler;
        event EventHandler TakeTextMessageHandler;
        //event EventHandler TimeHandler;

        Task Connected();
        Task<List<User>> LogOnServer();
        Task SendMessage(string whoSendMessageIDApi, string WhoTakeMessage, string IDApi, string message);


    }
}