using Microsoft.AspNet.SignalR.Client;

namespace KleinAppDesktopUI.Library.Models
{
    public interface IConnectionToServerModel
    {
        HubConnection _connection { get; set; }
        IHubProxy _proxy { get; set; }
    }
}