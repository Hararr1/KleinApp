using System;

namespace KleinMessage.Models
{
    public class MessageContentStructure
    {
        /*
         Flag is using to define directons of message.
         Default: false - client message
                  true - friend message
        */
        public bool Flag { get; set; }
        public string Content { get; set; }

        public string Time { get; set; }
    }
}
