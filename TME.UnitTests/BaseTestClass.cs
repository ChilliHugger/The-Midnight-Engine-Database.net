using Autofac;
using Moq;
using NUnit.Framework;
using TME;
using TME.Interfaces;
using TME.Scenario.Default.Base;

namespace TME.UnitTests
{
    public class BaseTestClass
    {   
        protected IContainer Container;

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

        protected virtual void RegisterMocks(ContainerBuilder containerBuilder)
        {
        }
        
        protected virtual void AfterSetup()
        {
        }
    }
}