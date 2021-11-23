using System;
using TME.Interfaces;
using Autofac;
using IContainer = Autofac.IContainer;
using ContainerBuilder = Autofac.ContainerBuilder;

namespace TME.SpecTests
{

    public class SpecTestDependencyContainer : IDependencyContainer
    {
        private readonly ContainerBuilder _containerBuilder;
        private readonly Action<ContainerBuilder> _register;
        public IContainer CurrentContainer { get; private set; } = null!;
        
        public SpecTestDependencyContainer(
            ContainerBuilder containerBuilder, Action<ContainerBuilder> register)
        {
            _containerBuilder = containerBuilder;
            _register = register;
        }

        public IDependencyContainer Build()
        {
            _register(_containerBuilder);
            RegisterModules();
            _containerBuilder.RegisterInstance<IDependencyContainer>(this).SingleInstance();
            CurrentContainer = _containerBuilder.Build();
            return this;
        }


        public IDependencyContainer RegisterModules()
        {
            return this;
        }
    }
}