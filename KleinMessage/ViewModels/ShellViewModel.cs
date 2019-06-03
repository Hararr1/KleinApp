using Caliburn.Micro;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<RegisterOnEvent>, IHandle<RegisterSuccessEvent>
    {

        private IEventAggregator _events;
        private ILoggedInUserModel _logged;
        private ChatViewModel _chatVM;
        private RegisterViewModel _registerVM;
        private SettingsViewModel _settingsVM;
        private SimpleContainer _container;      





        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (_logged!=  null)
                {
                    output = true;
                }

                return output;
            }

        }


        public ShellViewModel(IEventAggregator events, ChatViewModel chatVM, RegisterViewModel regVW, SimpleContainer container, SettingsViewModel settingsVM)
        {
            _events = events;
            _logged = null;
            _chatVM = chatVM;
            _registerVM = regVW;
            _settingsVM = settingsVM;
            _container = container;        

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
            


        }

        public void Handle(LogOnEvent message)
        {
            _logged = message._user;         
            NotifyOfPropertyChange(() => IsErrorVisible);
            ActivateItem(_chatVM);


        }

        public void Handle(RegisterOnEvent message)
        {
            ActivateItem(_registerVM);
        }

        public void Handle(RegisterSuccessEvent message)
        {
            ActivateItem(_container.GetInstance<LoginViewModel>());

        }

      

        public void settingsButton()
        {
           
            ActivateItem(_container.GetInstance<SettingsViewModel>());
        }
    }



}

