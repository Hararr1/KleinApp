using Caliburn.Micro;
using KleinMessage.Models;
using KleinMessage.WorkSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KleinMessage.ViewModels
{
    public class ChatViewModel : Screen
    {
        #region Variables & Properties

        public bool previousShiftClick;

        private IService chatService;

        private MessageStructure currentMessage;

        private string messageContent;

        public MessageStructure CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                currentMessage = value;
                NotifyOfPropertyChange(() => CurrentMessage);
            }
        }

        public string MessageContent
        {
            get { return messageContent; }
            set
            {
                messageContent = value;
                NotifyOfPropertyChange(() => MessageContent);
            }
        }
        #endregion

        #region Constructor and method invoke in start

        public ChatViewModel(IService chatService)
        {
            this.chatService = chatService;
        }

        public async Task LoadData()
        {
            if (ApplicationItemsCollection.IsActive == false)
            {
                await chatService.Connected();
                List<User> users = await chatService.LogOnServer();

                foreach (User user in users)
                {
                    if (user.IDApi != ApplicationItemsCollection.Logged.UserId)
                    {
                        AddNewFriend(user);
                    }
                }
                LoadDataUser();
                ApplicationItemsCollection.IsActive = true;
            }
        }

        private void LoadDataUser()
        {
            CurrentMessage = new MessageStructure();

            this.chatService.IsSomebodyLoggedHandler += chatService_IsSomebodyLoggedHandler;
            this.chatService.TakeTextMessageHandler += ChatService_TakeTextMessageHandler;
        }

        #endregion

        #region Events and Methods

        public async Task SendMessageButton()
        {
            string message = MessageContent;
            string IDApi = CurrentMessage.Friend.IDApi;
            string username = CurrentMessage.Friend.Name;

            try
            {
                await chatService.SendMessage(ApplicationItemsCollection.Logged.UserId, username, IDApi, message);
            }
            catch (Exception e)
            {
                MessageBox.Show($"It's problem to send message! This is a code: {e.HResult.ToString()}");
            }
            finally
            {
                MessageContentStructure msg = new MessageContentStructure()
                {
                    Background = new SolidColorBrush(Color.FromRgb(51, 153, 255)),
                    Margin = new Thickness(60,0,0,0),
                    Flag = false,
                    Content = message,
                    Time = DateTime.Now.ToString("HH:mm:ss")
                   
                };
                CurrentMessage.Messages.Add(msg);
                MessageContent = "";
            }
        } 

        // --- Event Handlers --- //
        private void ChatService_TakeTextMessageHandler(object sender, EventArgs e)
        {
            User user = (User)((object[])sender)[0];
            string message = ((object[])sender)[1].ToString();

            Application.Current.Dispatcher.Invoke(() =>
            {
                ApplicationItemsCollection.RegistryMessages
                .First(x => x.Friend.IDApi == user.IDApi)
                .Messages
                .Add(new MessageContentStructure
                {
                    Background = new SolidColorBrush(Color.FromRgb(111, 194, 233)),
                    Margin = new Thickness(0,0,60,0),
                    Content = message,
                    Flag = true,
                    Time = DateTime.Now.ToString("HH:mm:ss")
                });
            });
        }

        private void chatService_IsSomebodyLoggedHandler(object sender, EventArgs e)
        {
            User userConnected = sender as User;

            if (ApplicationItemsCollection
                .RegistryMessages
                .FirstOrDefault(x => x.Friend == userConnected) == null)
            {
                AddNewFriend(userConnected);
            }
        }

        //--- Helper Methods ---//
        private void AddNewFriend(User user)
        {
            MessageStructure messageStructure = new MessageStructure()
            {
                Friend = user,
                Messages = new BindableCollection<MessageContentStructure>()
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                ApplicationItemsCollection.RegistryMessages.Add(messageStructure);
            });
        }

        public void ChangeCurrentMessage(User user)
        {
            MessageStructure changeCurrentMessage = ApplicationItemsCollection.RegistryMessages.FirstOrDefault(f => f.Friend == user);
            CurrentMessage = changeCurrentMessage;
        }

        public async Task IsShiftClick(KeyEventArgs e)
        {
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                await SendMessageButton();
                e.Handled = true;
            }
        }


        #endregion

    }
}
