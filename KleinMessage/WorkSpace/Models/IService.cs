using System;
using System.Threading.Tasks;

namespace KleinMessage.WorkSpace.Models
{
    public interface IService
    {
        event Action<string, string> MessageHandling;
        event Action<string> TimeHandling;
        Task Connected();
        Task SendBroadcastMessageAsync(string msg);
        Task SendUnicastMessageAsync(string friend, string message);
        Task LogIn();
        Task GetTime();
    }
}