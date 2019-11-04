using Caliburn.Micro;
using KleinMessage.Models;
using KleinMessage.Views;
using KleinMessage.WorkSpace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KleinMessage.ViewModels
{
    public class ChatViewModel : Screen
    {

        private string messageContent;
        private IService chatService;
        private BindableCollection<MessageStructure> registryMessages;




        public BindableCollection<MessageStructure> RegistryMessages
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
            RegistryMessages = new BindableCollection<MessageStructure>();
            CurrentMessage = new MessageStructure();

            this.chatService = chatService;
            this.chatService.IsSomebodyLoggedHandler += chatService_IsSomebodyLoggedHandler;
            this.chatService.TakeTextMessageHandler += ChatService_TakeTextMessageHandler;
        }

        private void ChatService_TakeTextMessageHandler(object sender, EventArgs e)
        {
            User user = (User)((object[])sender)[0];
            string message = ((object[])sender)[1].ToString();

            Application.Current.Dispatcher.Invoke(() =>
            {
                RegistryMessages
                .First(x => x.Friend.IDApi == user.IDApi)
                .Messages
                .Add(new MessageContentStructure
                {
                    Content = message,
                    Flag = true,
                    Time = DateTime.Now.ToString("HH:mm:ss")
                });
            });
        }

        private void chatService_IsSomebodyLoggedHandler(object sender, EventArgs e)
        {
            User userConnected = sender as User;

            if (RegistryMessages
                .FirstOrDefault(x => x.Friend == userConnected) == null)
            {
                AddNewFriend(userConnected);
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
                    Flag = false,
                    Content = message,
                    Time = DateTime.Now.ToString("HH:mm:ss")
                };
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
                Messages = new BindableCollection<MessageContentStructure>()
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                RegistryMessages.Add(messageStructure);
            });
        }





    }
}
