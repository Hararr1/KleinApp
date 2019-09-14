using Caliburn.Micro;
using KleinAppDesktopUI.Library.ChatServer;
using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
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
        private ObservableCollection<string> _messageListBox;

        public ObservableCollection<string> MessageListBox
        {
            get { return _messageListBox; }
            set
            {
                _messageListBox = value;
                NotifyOfPropertyChange(() => MessageListBox);
            }
        }




        public ChatViewModel ()
            {           
                MessageListBox = new ObservableCollection<string>();
          
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
            
            MessageListBox.Add(x);
            // TODO 
        }

    }
}
