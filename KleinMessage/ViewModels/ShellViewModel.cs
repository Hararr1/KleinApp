using Caliburn.Micro;
using KleinAppDesktopUI.Library.ChatServer;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using KleinMessage.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private IConnectionToServerModel _connection;
        private string _serverTime;


        public string ServerTime
        {
            get { return _serverTime; }
            set
            {
                _serverTime = value;
                NotifyOfPropertyChange(() => ServerTime);
            }
        }



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
            _logged = null;
            _connection = null;           
            _events = events;
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
            _connection = message._serverModel;
            if (_connection._connection != null)
            {
                try
                {
                    _connection._proxy.Invoke("LogOnSever", _logged.FirstName);
                    _connection._proxy.On<string>("displayTime", value =>
                    {
                        ServerTime = value;
                    });
                    _connection._proxy.Invoke("ServerTime");

                }
                catch (Exception)
                {

                    throw;
                }
            }

            NotifyOfPropertyChange(() => IsErrorVisible);
            ActivateItem(_container.GetInstance<ChatViewModel>());
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
        public void chat()
        {
            ActivateItem(_container.GetInstance<ChatViewModel>());
        }
    }



}

