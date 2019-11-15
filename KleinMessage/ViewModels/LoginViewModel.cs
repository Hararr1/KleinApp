using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KleinMessage.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private string _requestMessage;

        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator events, ILoggedInUserModel user, IConnectionToServerModel serverModel)
        {
            _apiHelper = aPIHelper;
            _events = events;

            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];

            if (username != "" && password != "")
            {
                _username = username;
                _password = password;
            }
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

                if (RequestMessage?.Length > 0)
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
                    output = true;

                }
                return output;
            }

        }



        public async Task LogIn()
        {
            try
            {
//#if DEBUG
//                ApplicationItemsCollection.Logged = new LoggedInUserModel() { UserId = "123", CreatedDate = DateTime.Now, EmailAddress = "debug@wp.pl", FirstName = "debug", LastName = "debug" };
//                _events.PublishOnUIThread(new LogOnEvent());
//#endif

                RequestMessage = "";
                var result = await _apiHelper.Authenticate(UsernameTextBox, PasswordTextBox);
                ApplicationItemsCollection.Logged = await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
                _events.PublishOnUIThread(new LogOnEvent(UsernameTextBox, PasswordTextBox));


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
