using Caliburn.Micro;
using KleinAppDesktopUI.Library.ChatServer;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
using KleinMessage.WorkSpace.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Action = System.Action;

namespace KleinMessage.ViewModels
{
    public class ChatViewModel : Screen
    {

        private string _messageContent;
        private IService _chatService;

        private ObservableCollection<MessageContentStructure> _message2;

        public ObservableCollection<MessageContentStructure> Message2
        {
            get { return _message2; }
            set {
              
                    _message2 = value;
                   // ApplicationItemsCollection.AllMessages[0].Messages = value;
                    NotifyOfPropertyChange(() => Message2);              
            }
        }

        private ObservableCollection<MessageStructure> _friendsList;

        public ObservableCollection<MessageStructure> FirendsList
        {
            get
            {
                return _friendsList;
            }
            set
            {
                _friendsList = value;
                NotifyOfPropertyChange(() => _friendsList);

            }
        }

        private string _friend;

        public string Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                var searchFriend = ApplicationItemsCollection.AllMessages.Where(x => x.Friend == value).SingleOrDefault();
                Message2 = searchFriend.Messages;
                NotifyOfPropertyChange(() => Friend);
            }
        }



        public ChatViewModel (IService chatService)
            {
            /* My first idea is when client choosed friend to conversation with him
             * constructor should searched from ApplicationItems.Collections.AllMessages.. friends
             * with argument submitted and then create ChatView.xaml
             *  public static ObservableCollection<MessageStructure> AllMessages
             *  TODO: IHandle<xxxx> when somebody send a message
             *  
             */
            _chatService = chatService;
           FirendsList = ApplicationItemsCollection.AllMessages;
             Message2 = ApplicationItemsCollection.AllMessages[0].Messages;
  

         
  
        }

        private void NewMessageResult(string message, string friend)
        {
            var searchFriend = ApplicationItemsCollection.AllMessages.Where(x => x.Friend == friend).SingleOrDefault();
            searchFriend.Messages.Add(new MessageContentStructure { Flag = false, Content = message });
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


        public void SendMessageButton()
        {
            string x =  MessageContent;
            var d = new MessageContentStructure() {Flag= true, Content= x };
            Message2.Add(d);


            //   ApplicationItemsCollection.AllMessages[0].Messages.Add(d);

             


            // TODO 
        }

        public void IsFriendChoosen(string text)
        {
            Friend = text;
            //TODO When client choosen friend set Message2 to appropriate in AppItemsColl.. 
         
        }

     

    }
}
