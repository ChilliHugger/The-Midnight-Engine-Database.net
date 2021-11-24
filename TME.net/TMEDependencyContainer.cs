using Autofac;
using TME.Interfaces;

namespace TME
{
    public class TMEDependencyContainer : IDependencyContainer
    {
        private readonly ContainerBuilder _containerBuilder;
        public IContainer CurrentContainer { get; private set; } = null!;


        public TMEDependencyContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public IDependencyContainer Build()
        {
            RegisterModules();
            _containerBuilder.RegisterInstance<IDependencyContainer>(this).SingleInstance();
            CurrentContainer = _containerBuilder.Build();
            return this;
        }

        public IDependencyContainer RegisterModules()
        {
            _containerBuilder.RegisterModule(new TMEModule());
            return this;
        }

    }
}
