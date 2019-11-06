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
using KleinMessage.WorkSpace.Models;

namespace KleinMessage
{
    public static class ApplicationItemsCollection
    {
        public static bool IsActive { get; set;  }
        public static ILoggedInUserModel Logged { get; set; }

        private  static BindableCollection<MessageStructure> registryMessages;
        public static BindableCollection<MessageStructure> RegistryMessages
        {
            get { return registryMessages; }
            set
            {
                registryMessages = value;
                NotifyStaticPropertyChanged(nameof(RegistryMessages));
            }
        } 

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
