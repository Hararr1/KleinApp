using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using KleinMessage.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KleinMessage.ViewModels
{
    public class RegisterViewModel : Screen
    {
        private string _emailTextBox;
        private string _passwordTextBox;
        private string _confirmPasswordTextBox;
        private string _firstNameTextBox;
        private string _lastNameTextBox;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private string _requestMessage;
        private Brush _isSuccess;


        #region properties

        public string RequestMessage
        {
            get { return _requestMessage; }
            set
            {
                _requestMessage = value;
                NotifyOfPropertyChange(() => RequestMessage);
                // todo color
                NotifyOfPropertyChange(() => IsErrorVisible);

            }
        }

        public Brush IsSuccess
        {
            get { return _isSuccess; }
            set {
                _isSuccess = value;
                NotifyOfPropertyChange(() => IsSuccess);
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

        public RegisterViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        public string EmailTextBox
        {
            get { return _emailTextBox; }
            set
            {
                _emailTextBox = value;
                NotifyOfPropertyChange(() => EmailTextBox);
            }
        }

            
        public string PasswordTextBox
        {
            get { return _passwordTextBox; }
            set
            {
                _passwordTextBox = value;
                NotifyOfPropertyChange(() => PasswordTextBox);
            }
        }



        public string ConfirmPasswordTextBox
        {
            get { return _confirmPasswordTextBox; }
            set
            {
                _confirmPasswordTextBox = value;
                NotifyOfPropertyChange(() => ConfirmPasswordTextBox);
            }
        }


        public string FirstNameTextBox
        {
            get { return _firstNameTextBox; }
            set
            {
                _firstNameTextBox = value;
                NotifyOfPropertyChange(() => FirstNameTextBox);
            }
        }

        public string LastNameTextBox
        {
            get { return _lastNameTextBox; }
            set
            {
                _lastNameTextBox = value;
                NotifyOfPropertyChange(() => LastNameTextBox);
            }
        }
        #endregion

        public async Task Create()
        {
            try
            {
                IsSuccess = Brushes.Green;
                RequestMessage = "I'm trying to create a new account ..";
                var x = await _apiHelper.Register(EmailTextBox, PasswordTextBox, ConfirmPasswordTextBox, FirstNameTextBox, LastNameTextBox);
                _events.PublishOnUIThread(new RegisterSuccessEvent());           
               
                

            }
            catch (Exception e)
            {
                IsSuccess = Brushes.Red;
                RequestMessage = e.Message;
            }
        }


    }
}
