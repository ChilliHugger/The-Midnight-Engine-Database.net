using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario.Actions;
using TME.SpecTests.Context;
using TME.SpecTests.Mocks;

//using ContainerBuilder = TechTalk.SpecFlow.Infrastructure.ContainerBuilder;

namespace TME.SpecTests.Hooks
{
    [Binding]
    public class MainHooks
    {
        private readonly MapMockBuilder _mapMockBuilder;
        private readonly VariablesMockBuilder _variablesMockBuilder;
        private readonly CommandHistoryMockBuilder _commandHistoryMockBuilder;
        //private readonly ActionMockBuilder _actionMockBuilder;

        //public Mock<IAction> MockObjectDropped { get; set; } = null!;

        public IContainer Container { get; private set; } = null!;

        public MainHooks(
            //ActionMockBuilder actionMockBuilder,
            CommandHistoryMockBuilder commandHistoryMockBuilder,
            VariablesMockBuilder variablesMockBuilder,
            MapMockBuilder mapMockBuilder)
        {
            //_actionMockBuilder = actionMockBuilder;
            _commandHistoryMockBuilder = commandHistoryMockBuilder;
            _variablesMockBuilder = variablesMockBuilder; 
            _mapMockBuilder = mapMockBuilder;
        }

        [BeforeScenario()]
        private void BeforeScenario()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var dependencyContainer = new SpecTestDependencyContainer(builder, containerBuilder =>
            {
                var tmeDependencyContainer = new TMEDependencyContainer(builder);
                tmeDependencyContainer.RegisterModules();
                RegisterMocks(builder);
            } );
            
            dependencyContainer.Build();
            Container = dependencyContainer.CurrentContainer!;
        }

        private void RegisterMocks(ContainerBuilder containerBuilder)
        {
            _mapMockBuilder.Build(containerBuilder);
            _variablesMockBuilder.Build(containerBuilder);
            _commandHistoryMockBuilder.Build(containerBuilder);
            //MockObjectDropped = _actionMockBuilder.Build(containerBuilder);
        }
        
    }
}