using Caliburn.Micro;
using KleinMessage.Models;
using KleinMessage.WorkSpace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace KleinMessage.ViewModels
{
    public class ChatViewModel : Screen
    {

        private string _messageContent;
        private IService chatService;
        private ObservableCollection<MessageStructure> registryMessages;


        public ObservableCollection<MessageStructure> RegistryMessages
        {
            get { return registryMessages; }
            set
            {
                registryMessages = value;
                NotifyOfPropertyChange(() => RegistryMessages);
            }
        }


        private MessageStructure currentMessage;

        public MessageStructure CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                currentMessage = value;
                NotifyOfPropertyChange(() => CurrentMessage);
            }
        }

        public ChatViewModel(IService chatService)
        {
            RegistryMessages = new ObservableCollection<MessageStructure>();
            CurrentMessage = new MessageStructure();

            this.chatService = chatService;
            this.chatService.IsSomebodyLoggedHandler += chatService_IsSomebodyLoggedHandler;
            this.chatService.TakeTextMessageHandler += ChatService_TakeTextMessageHandler;
        }

        private void ChatService_TakeTextMessageHandler(object sender, EventArgs e)
        {
            User user = (User)((object[])sender)[0];
            string message = ((object[])sender)[1].ToString();
            //ToDo
        }

        private void chatService_IsSomebodyLoggedHandler(object sender, EventArgs e)
        {
            User userConnected = sender as User;

            if (RegistryMessages.FirstOrDefault(x => x.Friend == userConnected) == null)
            {
                AddNewFriend(userConnected);
            }
        }
        public string MessageContent
        {
            get { return _messageContent; }
            set
            {
                _messageContent = value;
                NotifyOfPropertyChange(() => MessageContent);
            }
        }

        public async Task SendMessageButton()
        {
            string message = MessageContent;
            string IDApi = CurrentMessage.Friend.IDApi;
            string username = CurrentMessage.Friend.Name;

            try
            {
                await chatService.SendMessage(ApplicationItemsCollection.Logged.UserId, username, IDApi, message);

                CurrentMessage.Messages.Add(new MessageContentStructure()
                {
                    Content = message,
                    Flag = false
                });


            }
            catch (Exception e)
            {
                MessageBox.Show(e.HResult.ToString());
            }
            finally
            {
                MessageContentStructure msg = new MessageContentStructure() { Flag = true, Content = message };
                CurrentMessage.Messages.Add(msg);
                MessageContent = "";
            }
        }    

        public async Task LoadData()
        {
            if (ApplicationItemsCollection.Logged != null)
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
            }
        }

        private void AddNewFriend(User user)
        {
            MessageStructure messageStructure = new MessageStructure()
            {
                Friend = user,
                Messages = new List<MessageContentStructure>()
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                RegistryMessages.Add(messageStructure);
            });
        }





    }
}
