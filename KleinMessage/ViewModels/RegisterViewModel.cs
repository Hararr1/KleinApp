using Caliburn.Micro;
using KleinAppDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




       


        public RegisterViewModel(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;          
        }


        public async Task Create()
        {
            var x = await _apiHelper.Register(EmailTextBox, PasswordTextBox, ConfirmPasswordTextBox, FirstNameTextBox, LastNameTextBox);
        }


    }
}
