using KleinAppDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel;

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

        private static IConnectionToServerModel _connection;

        public static IConnectionToServerModel Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
                NotifyStaticPropertyChanged(nameof(Connection));
            }
        }



        static ApplicationItemsCollection()
        {
            Logged = new LoggedInUserModel();
            Connection = new ConnectionToServerModel();
          
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }

    }
}
