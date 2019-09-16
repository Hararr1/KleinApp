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
        public string Friend { get; set; }
        public ObservableCollection<MessageContentStructure> Messages { get; set; }


    }
}
