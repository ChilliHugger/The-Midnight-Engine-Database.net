using Autofac;
using NUnit.Framework;

namespace TME.UnitTests
{
    public sealed class BaseTestClass
    {
        private IContainer Container;

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            var dependencyContainer = new TestDependencyContainer(builder, containerBuilder =>
            {
                var tmeDependencyContainer = new TMEDependencyContainer(builder);
                tmeDependencyContainer.RegisterModules();
                RegisterMocks(builder);
            } );
            
            dependencyContainer.Build();
            Container = dependencyContainer.CurrentContainer;

            AfterSetup();
        }

        private void RegisterMocks(ContainerBuilder containerBuilder)
        {
        }

        private void AfterSetup()
        {
        }
    }
}