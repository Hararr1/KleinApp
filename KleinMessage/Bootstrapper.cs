﻿using Caliburn.Micro;
using KleinMessage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KleinMessage
{
    public class Bootstrapper : BootstrapperBase
    {
        //IoC conception - depency injection
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewmodelType => _container.RegisterPerRequest(
                    viewmodelType, viewmodelType.ToString(), viewmodelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        /*
         * "normally Caliburn.Micro first finds the location and create new instance for viewmodel
         * instead I'm gonna use container to get that viewmodel" - TimCorey.
        */
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        // all instance with IEnumerable
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}
