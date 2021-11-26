#nullable enable
using System;
using Autofac;
using TME.Interfaces;

namespace TME.UnitTests
{
    public class TestDependencyContainer : IDependencyContainer
    {
        private readonly ContainerBuilder _containerBuilder;
        private readonly Action<ContainerBuilder> _register;
        public IContainer CurrentContainer { get; set; } = null!;
        public ILifetimeScope? CurrentScope { get; set; }

        public TestDependencyContainer(ContainerBuilder containerBuilder, Action<ContainerBuilder> register)
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