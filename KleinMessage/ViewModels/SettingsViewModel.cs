using Caliburn.Micro;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.ViewModels
{
    public class SettingsViewModel : Screen
    { 
        public ILoggedInUserModel _user;
        

        public SettingsViewModel(ILoggedInUserModel user)
        {
            _user = user;
            FirstNameText = _user.FirstName;
            LastNameText = _user.LastName;
            EmailText = _user.EmailAddress;
            CreateText = _user.CreatedDate.ToLongDateString();
     
        }

        private string _firstNameText;

        public string FirstNameText
        {
            get { return _firstNameText; }
            set
            {
                _firstNameText = value;
                NotifyOfPropertyChange(() => FirstNameText);
            }
        }
        private string _lasttNameText;

        public string LastNameText
        {
            get { return _lasttNameText; }
            set
            {
                _lasttNameText = value;
                NotifyOfPropertyChange(() => LastNameText);
            }
        }

        private string _emailText;

        public string EmailText
        {
            get { return _emailText; }
            set
            {
                _emailText = value;
                NotifyOfPropertyChange(() => EmailText);
            }
        }
        private string _createText;

        public string CreateText
        {
            get { return _createText; }
            set
            {
                _createText = value;
                NotifyOfPropertyChange(() => CreateText);
            }
        }





    }
}
