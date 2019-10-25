using Caliburn.Micro;
using KleinMessage.EventModels;
using KleinMessage.WorkSpace.Models;
using System.Windows.Media;

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

        private Brush searchLabelBackground;

        public Brush SearchLabelBackground
        {
            get { return searchLabelBackground; }
            set
            {
                searchLabelBackground = value;
                NotifyOfPropertyChange(() => SearchLabelBackground);
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
            _events = events;
            _chatService = chatservice;
            _chatVM = chatVM;
            _registerVM = regVW;
            _settingsVM = settingsVM;
            _container = container;
            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }
        #region Events
        public void Handle(LogOnEvent message)
        {
            NotifyOfPropertyChange(() => IsErrorVisible);
            ChatButton();
        }

        public void Handle(RegisterOnEvent message)
        {
            ActivateItem(_registerVM);
        }

        public void Handle(RegisterSuccessEvent message)
        {
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        #endregion

        #region Buttons
        public void SettingsButton()
        {
            ActivateItem(_container.GetInstance<SettingsViewModel>());

            SettingsLabelBackground = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            ChatLabelBackground = new SolidColorBrush(Colors.Black);
            SearchLabelBackground = new SolidColorBrush(Colors.Black);
        }
        public void ChatButton()
        {
            ActivateItem(_container.GetInstance<ChatViewModel>());

            ChatLabelBackground = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            SearchLabelBackground = new SolidColorBrush(Colors.Black);
            SettingsLabelBackground = new SolidColorBrush(Colors.Black);
        }

        public void exitButton()
        {

        }
        #endregion
    }



}

