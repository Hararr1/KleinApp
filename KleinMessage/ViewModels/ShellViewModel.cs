﻿using Caliburn.Micro;
using KleinMessage.EventModels;
using KleinMessage.Models;
using KleinMessage.Views;
using KleinMessage.WorkSpace.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KleinMessage.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<RegisterOnEvent>, IHandle<RegisterSuccessEvent>
    {

        private IEventAggregator events;
        private IService chatservice;
        private ChatViewModel chatVM;
        private RegisterViewModel registerVM;
        private SettingsViewModel settingsVM;
        private SimpleContainer container;
        // private string serverTime;

        public bool IsActiveFriendList { get; set; }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ApplicationItemsCollection.Logged != null)
                {
                    output = true;
                }

                return output;
            }

        }


        private Visibility isVisibleShowHideFriendList;

        public Visibility IsVisibleShowHideFriendList
        {
            get
            {
                return isVisibleShowHideFriendList;
            }

            set
            {
                isVisibleShowHideFriendList = value;
                NotifyOfPropertyChange(() => IsVisibleShowHideFriendList);
            }

        }

        #region UI
        private Brush chatLabelBackground;

        public Brush ChatLabelBackground
        {
            get { return chatLabelBackground; }
            set
            {
                chatLabelBackground = value;
                NotifyOfPropertyChange(() => chatLabelBackground);
            }
        }

        private Brush settingsLabelBackground;

        public Brush SettingsLabelBackground
        {
            get { return settingsLabelBackground; }
            set
            {
                settingsLabelBackground = value;
                NotifyOfPropertyChange(() => SettingsLabelBackground);
            }
        }

        public string SourceChatButton
        {
            get
            {
                return ApplicationItemsCollection.Logged != null ? 
                    "/Assets/telegram-logo.png" : "/Assets/telegram-logo-disabled.png";
            }
        }

        public string SourceSettingsButton
        {
            get
            {
                return ApplicationItemsCollection.Logged != null ?
                    "/Assets/two-settings-cogwheels.png" : "/Assets/two-settings-cogwheels-disabled.png";
            }
        }

        public bool IsEnabledChatButton
        {
            get
            {
                bool output = false;

                if (ApplicationItemsCollection.Logged != null)
                {
                    output = true;
                }

                return output;
            }
               
        }

        public bool IsEnabledSettingsButton
        {
            get
            {
                bool output = false;

                if (ApplicationItemsCollection.Logged != null)
                {
                    output = true;
                }

                return output;
            }
        }

        #endregion


        //public string ServerTime
        //{
        //    get { return _serverTime; }
        //    set
        //    {
        //        _serverTime = value;
        //        NotifyOfPropertyChange(() => ServerTime);
        //    }
        //}

        public ShellViewModel(IEventAggregator events, IService chatservice, ChatViewModel chatVM, RegisterViewModel regVW, SimpleContainer container, SettingsViewModel settingsVM)
        {
            this.events = events;
            this.chatservice = chatservice;
            this.chatVM = chatVM;
            registerVM = regVW;
            this.settingsVM = settingsVM;
            this.container = container;
            this.events.Subscribe(this);

            ApplicationItemsCollection.RegistryMessages = new BindableCollection<MessageStructure>();
            IsVisibleShowHideFriendList = Visibility.Hidden;

            base.ActivateItem(this.container.GetInstance<LoginViewModel>());
        }
        #region Events
        public void Handle(LogOnEvent message)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["username"].Value = message.Username;
            configuration.AppSettings.Settings["password"].Value = message.Password;
            configuration.Save(ConfigurationSaveMode.Modified);

            NotifyOfPropertyChange(() => IsErrorVisible);
            NotifyOfPropertyChange(() => IsEnabledChatButton);
            NotifyOfPropertyChange(() => IsEnabledSettingsButton);
            NotifyOfPropertyChange(() => SourceChatButton);
            NotifyOfPropertyChange(() => SourceSettingsButton);
            ChatButton();
        }

        public void Handle(RegisterOnEvent message)
        {
            ActivateItem(registerVM);
        }

        public void Handle(RegisterSuccessEvent message)
        {
            ActivateItem(container.GetInstance<LoginViewModel>());
        }

        #endregion

        #region Buttons
        public void ShowHideFriendList()
        {
            if ((Application.Current.MainWindow as ShellView).ActiveItem.Content is ChatView)
            {
                if (IsActiveFriendList)
                {
                    GridLengthConverter converter = new GridLengthConverter();
                    ((Application.Current.MainWindow as ShellView).ActiveItem.Content as ChatView).FriendListColumn.Width = (GridLength)converter.ConvertFromString("0");
                    IsActiveFriendList = false;

                }
                else
                {
                    GridLengthConverter converter = new GridLengthConverter();
                    ((Application.Current.MainWindow as ShellView).ActiveItem.Content as ChatView).FriendListColumn.Width = (GridLength)converter.ConvertFromString("250");
                    IsActiveFriendList = true;
                }
            }
        }

        public void SettingsButton()
        {
            ActivateItem(container.GetInstance<SettingsViewModel>());

            SettingsLabelBackground = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            ChatLabelBackground = new SolidColorBrush(Colors.Black);
            IsVisibleShowHideFriendList = Visibility.Hidden;

        }
        public void ChatButton()
        {
            ActivateItem(container.GetInstance<ChatViewModel>());

            ChatLabelBackground = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            SettingsLabelBackground = new SolidColorBrush(Colors.Black);
            IsVisibleShowHideFriendList = Visibility.Visible;
        }

        public async Task exitButton()
        {
            if (ApplicationItemsCollection.Logged != null)
            {
                await chatservice.Disconnected();
            }
            
           Application.Current.Shutdown();
        }
        #endregion
    }



}

