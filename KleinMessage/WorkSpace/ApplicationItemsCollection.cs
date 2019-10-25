using KleinAppDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel;
using KleinMessage.Models;
using Microsoft.AspNet.SignalR.Client;
using System.Configuration;

namespace KleinMessage
{
    public static class ApplicationItemsCollection
    {
        public static ILoggedInUserModel Logged { get; set; }

    }
}
