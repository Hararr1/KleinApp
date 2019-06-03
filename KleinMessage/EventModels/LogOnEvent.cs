using KleinAppDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.EventModels
{
    public class LogOnEvent
    {
        public ILoggedInUserModel _user;
        public LogOnEvent(ILoggedInUserModel user )
        {
            _user = user;
        }
    }
}
