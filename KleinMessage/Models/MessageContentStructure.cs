using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.Models
{
    public class MessageContentStructure
    {
        /*
         Flag is using to define directons of message.
         Default: 0 - client message
                  1 - friend message
        */
        public bool Flag { get; set; }
        public string Content { get; set; } 
    }
}
