using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using KleinMessage.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KleinMessage.ViewModels
{
   public class LoginViewModel : Screen
    {
        private string _username= "halo@wp.pl";
        private string _password = "Herbatka15!";
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        private IConnectionToServerModel _serverModel;
        private string _requestMessage;
       
        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator events, ILoggedInUserModel user, IConnectionToServerModel serverModel)
        {
            _apiHelper = aPIHelper;
            _events = events;
            _user = user;
            _serverModel = serverModel;
        }
        public string PasswordTextBox
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => PasswordTextBox);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        public string UsernameTextBox
        {
            get { return _username; }

            set
            {
                _username = value;
                NotifyOfPropertyChange(() => UsernameTextBox);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if(RequestMessage?.Length > 0 )
                {
                    output = true;
                }

                return output;
            }
           
        }
        public string RequestMessage
        {
            get { return _requestMessage; }
            set
            {
                _requestMessage = value;
                NotifyOfPropertyChange(() => RequestMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);

            }
        }
        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (UsernameTextBox?.Length > 0 && PasswordTextBox?.Length > 0)
                {
                    output= true;

                }
                return output;
            }
               
        }



        public async Task LogIn()
        {
            try
            {
                RequestMessage = "";
                var result = await _apiHelper.Authenticate(UsernameTextBox, PasswordTextBox);
                _user = await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                string url = ConfigurationManager.AppSettings["server"];
                _serverModel._connection = new HubConnection(url);
                _serverModel._proxy = _serverModel._connection.CreateHubProxy("ChatHub");
                _serverModel._connection.Start().Wait();

                _events.PublishOnUIThread(new LogOnEvent(_user, _serverModel));             

            }
            catch (Exception)
            {

                RequestMessage = "Niepoprawne dane";
            }
        }
        public async Task IsEnterClick(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await LogIn();
            }
        }

        public void Register()
        {
            _events.PublishOnUIThread(new RegisterOnEvent());
        }

    }
}
