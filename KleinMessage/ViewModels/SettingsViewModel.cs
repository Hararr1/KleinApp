using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KleinMessage.ViewModels
{
    public class SettingsViewModel : Screen
    { 
        private ILoggedInUserModel _user;
        private IAPIHelper _helper;
        private string _firstNameText;
        private string _lasttNameText;
        private string _emailText;
     
        private string _oldPasswordBox;
        private string _createText;
        private string _newPasswordBox;
        private string _confirmPasswordBox;
        private string _requestMessage;
        private Brush _isSuccess;


        public SettingsViewModel(ILoggedInUserModel user, IAPIHelper helper)
        {
            _user = user;
            _helper = helper;
            FirstNameText = _user.FirstName;
            LastNameText = _user.LastName;
            EmailText = _user.EmailAddress;
            CreateText = _user.CreatedDate.ToLongDateString();
     
        }


        public string FirstNameText
        {
            get { return _firstNameText; }
            set
            {
                _firstNameText = value;
                NotifyOfPropertyChange(() => FirstNameText);
            }
        }
        public string LastNameText
        {
            get { return _lasttNameText; }
            set
            {
                _lasttNameText = value;
                NotifyOfPropertyChange(() => LastNameText);
            }
        }
        public string EmailText
        {
            get { return _emailText; }
            set
            {
                _emailText = value;
                NotifyOfPropertyChange(() => EmailText);
            }
        }
        public string CreateText
        {
            get { return _createText; }
            set
            {
                _createText = value;
                NotifyOfPropertyChange(() => CreateText);
            }
        }

        public string OldPasswordBox
        {
            get { return _oldPasswordBox; }
            set {
                _oldPasswordBox = value;
                NotifyOfPropertyChange(() => OldPasswordBox);
            }
        }
        public string NewPasswordBox
        {
            get { return _newPasswordBox; }
            set
            {
                _newPasswordBox = value;
                NotifyOfPropertyChange(() => NewPasswordBox);
            }
        }
        public string ConfirmPasswordBox
        {
            get { return _confirmPasswordBox; }
            set
            {
                _confirmPasswordBox = value;
                NotifyOfPropertyChange(() => ConfirmPasswordBox);
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
        public Brush IsSuccess
        {
            get { return _isSuccess; }
            set
            {
                _isSuccess = value;
                NotifyOfPropertyChange(() => IsSuccess);
            }
        }


        public async Task ChangePassword()
        {
            RequestMessage = "";
            var request = await _helper.ChangePassword(_user.Token, OldPasswordBox , NewPasswordBox, ConfirmPasswordBox);
            if(request == true)
            {
                IsSuccess = Brushes.Green;
                RequestMessage = "Success!";
            }
            else
            {
                IsSuccess = Brushes.Red;
                RequestMessage = "Try again!";
            }
        }





    }
}
