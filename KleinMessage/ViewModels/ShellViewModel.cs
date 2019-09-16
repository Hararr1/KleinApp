using Caliburn.Micro;
using KleinAppDesktopUI.Library.ChatServer;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.EventModels;
using KleinMessage.Models;
using KleinMessage.WorkSpace.Models;
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
        private IService _chatService;
        private ChatViewModel _chatVM;
        private RegisterViewModel _registerVM;
        private SettingsViewModel _settingsVM;
        private SimpleContainer _container;
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

                if (ApplicationItemsCollection.Logged.UserId != null)
                {
                    output = true;
                }

                return output;
            }

        }


        public ShellViewModel(IEventAggregator events, IService chatservice, ChatViewModel chatVM, RegisterViewModel regVW, SimpleContainer container, SettingsViewModel settingsVM)
        {         
            _events = events;
            _chatService = chatservice;
            _chatVM = chatVM;
            _registerVM = regVW;
            _settingsVM = settingsVM;
            _container = container;        
            _events.Subscribe(this);
            _chatService.Connected();
            _chatService.TimeHandling += TimeHandle;
            ActivateItem(_container.GetInstance<LoginViewModel>());           
        }
        public void Handle(LogOnEvent message)
        {
            
         
            if (_chatService != null)
            {
                try
                {
                 
                    
                    _chatService.LogIn();

                }
                catch (Exception)
                {

                    throw;
                }
            }

            NotifyOfPropertyChange(() => IsErrorVisible);
            ActivateItem(_container.GetInstance<ChatViewModel>());
        }

        private void TimeHandle(string obj)
        {
            ServerTime = obj;
        }

        public void Handle(RegisterOnEvent message)
        {
            ActivateItem(_registerVM);
        }
        public void Handle(RegisterSuccessEvent message)
        {
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }    



        public void SettingsButton()
        {           
            ActivateItem(_container.GetInstance<SettingsViewModel>());
        }
        public void ChatButton()
        {
            ActivateItem(_container.GetInstance<ChatViewModel>());
        }

        public void exitButton()
        {
           
        }
    }



}

