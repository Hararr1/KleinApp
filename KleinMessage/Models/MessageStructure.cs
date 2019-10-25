using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.Models
{
   public class MessageStructure
    {
        public User Friend { get; set; }

        public List<MessageContentStructure> Messages { get; set; }      
    }
}
