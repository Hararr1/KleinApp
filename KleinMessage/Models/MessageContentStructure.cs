using System;
using System.Windows;
using System.Windows.Media;

namespace KleinMessage.Models
{
    public class MessageContentStructure
    {
        /*
         Flag is using to define directons of message.
         Default: false - client message
                  true - friend message
        */
        public SolidColorBrush Background { get; set; }

        public Thickness Margin { get; set; }

        public bool Flag { get; set; }

        public string Content { get; set; }

        public string Time { get; set; }

    }
}
