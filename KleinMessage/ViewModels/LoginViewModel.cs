using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using KleinMessage.EventModels;
using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.ViewModels
{
   public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
 


        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator events)
        {
            _apiHelper = aPIHelper;
            _events = events;
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

        private string _requestMessage;

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
                // capture more infromation about user
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _events.PublishOnUIThread(new LogOnEvent());
               
            }
            catch (Exception ex)
            {

                RequestMessage = ex.Message;
            }
        }

        public void Register()
        {
            _events.PublishOnUIThread(new RegisterOnEvent());
        }

    }
}
