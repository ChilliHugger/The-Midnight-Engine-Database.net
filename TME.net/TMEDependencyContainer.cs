using System;
using Autofac;
using TME.Interfaces;

namespace TME
{
    public class TMEDependencyContainer : IDependencyContainer
    {
        public IContainer CurrentContainer { get; set; }

        public TMEDependencyContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new TMEModule());
            containerBuilder.RegisterInstance<IDependencyContainer>(this).SingleInstance();

            CurrentContainer = containerBuilder.Build();
        }

    }
}
