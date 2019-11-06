using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace KleinMessage.Models
{
    public class MessageStructure : INotifyPropertyChanged
    {
        public User Friend { get; set; }

        private BindableCollection<MessageContentStructure> messages;

        public BindableCollection<MessageContentStructure> Messages
        {
            get { return messages; }
            set
            {
                messages = value;

                if (value != null)
                {
                    Messages.CollectionChanged += Messages_CollectionChanged;
                }
            }
        }

        private string lastMessageFromFriend;

        public string LastMessageFromFriend
        {
            get { return lastMessageFromFriend; }
            set
            {
                lastMessageFromFriend = value;

                OnPropertyRaised("LastMessageFromFriend");
            }
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BindableCollection<MessageContentStructure> messages = sender as BindableCollection<MessageContentStructure>;
            int lastMessageIndex = messages.Count - 1;

            if (messages[lastMessageIndex].Flag == true)
            {
                if (messages[lastMessageIndex].Content.Length > 10)
                {
                    LastMessageFromFriend = $" {messages[lastMessageIndex].Content.Substring(0, 10)} ...";
                }
                else
                {
                    LastMessageFromFriend = messages[lastMessageIndex].Content;
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
