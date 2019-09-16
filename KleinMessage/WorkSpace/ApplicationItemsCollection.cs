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
        private static ILoggedInUserModel _logged;

        public static ILoggedInUserModel Logged
        {
            get
            {
                return _logged;

            }
            set
            {
                _logged = value;
                NotifyStaticPropertyChanged(nameof(Logged));
            }
        }


        private static ObservableCollection<MessageStructure> _allMessages ;

        public static ObservableCollection<MessageStructure> AllMessages
        {
            get { return _allMessages; }
            set
            {
                _allMessages = value;
                NotifyStaticPropertyChanged(nameof(AllMessages));
            }
        }


        static ApplicationItemsCollection()
        {
            Logged = new LoggedInUserModel();
   

            AllMessages = new ObservableCollection<MessageStructure>();
            AllMessages.Add(new MessageStructure() { Friend = "Tom", Messages = new ObservableCollection<MessageContentStructure>(){
              new MessageContentStructure() { Flag = true, Content = "Hello My friend!" },
              new MessageContentStructure() { Flag = false, Content = "Hi! Nice to hear you"}
            }});
            AllMessages.Add(new MessageStructure()
            {
                Friend = "Jerry",
              Messages = new ObservableCollection<MessageContentStructure>(){
              new MessageContentStructure() { Flag = true, Content = "Hello This app is awesome!" },
              new MessageContentStructure() { Flag = false, Content = "Thank You!"}
            }
            });
            AllMessages.Add(new MessageStructure()
            {
                Friend = "Lukasz",
                Messages = new ObservableCollection<MessageContentStructure>(){
              new MessageContentStructure() { Flag = true, Content = "Hello!" },
              new MessageContentStructure() { Flag = false, Content = "Thank!"}
            }
            });

        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }

    }
}
