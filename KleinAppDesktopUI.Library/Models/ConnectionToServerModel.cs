using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinAppDesktopUI.Library.Models
{
    public class ConnectionToServerModel : IConnectionToServerModel
    {
        public HubConnection _connection { get; set; }
        public IHubProxy _proxy { get; set; }

    }
}
