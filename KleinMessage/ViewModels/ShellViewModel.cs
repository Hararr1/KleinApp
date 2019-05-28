using Caliburn.Micro;
using KleinMessage.EventModels;
using KleinMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinMessage.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {

        private IEventAggregator _events;
        private ChatViewModel _chatVM;
        private SimpleContainer _container;
        


        public ShellViewModel( IEventAggregator events, ChatViewModel chatVM, SimpleContainer container)
        {
            _events = events;          
            _chatVM = chatVM;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
       
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_chatVM);
            
        }

       



    }



}

