using Caliburn.Micro;
using KleinMessage.Models;
using KleinMessage.WorkSpace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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


        private MessageStructure currentMessage = new MessageStructure();

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
            this.chatService = chatService;
            this.chatService.IsSomebodyLoggedHandler += chatService_IsSomebodyLoggedHandler;
        }

        private void chatService_IsSomebodyLoggedHandler(object sender, EventArgs e)
        {
            User userConnected = sender as User;

            if (RegistryMessages.FirstOrDefault(x => x.Friend == userConnected) == null)
            {
                AddNewFriend(userConnected);
            }
        }

        //private async Task NewTextMessage(string friend, string message)
        //{

        //    var sender = RegistryMessages.Where(x => x.Friend == friend).FirstOrDefault();
        //    IsFriendChoosen(sender.Friend);

        //    MessageContentStructure msg = new MessageContentStructure() { Flag = true, Content = message };



        //           await Task.Run(()=> CurrentMessage.Messages.Add(msg));





        //}


        public string MessageContent
        {
            get { return _messageContent; }
            set
            {
                _messageContent = value;
                NotifyOfPropertyChange(() => MessageContent);
            }
        }


        


        //public async Task<bool> SendMessageButton()
        //{
        //    string message = MessageContent;
        //    string friend = CurrentMessage.Friend;

        //    try
        //    {

        //        await Task.Run(() => _chatService.SendUnicastMessageAsync(friend, message));
        //        return true;

        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //    finally
        //    {
        //        MessageContentStructure msg = new MessageContentStructure() { Flag = true, Content = message };
        //        CurrentMessage.Messages.Add(msg);
        //        MessageContent = "";
        //    }
        //}


        //   ApplicationItemsCollection.AllMessages[0].Messages.Add(d);




        // TODO        

        public void IsFriendChoosen(string text)
        {

           // CurrentMessage = RegistryMessages.Where(x => x.Friend == text).SingleOrDefault();

            //TODO When client choosen friend set Message2 to appropriate in AppItemsColl.. 

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
