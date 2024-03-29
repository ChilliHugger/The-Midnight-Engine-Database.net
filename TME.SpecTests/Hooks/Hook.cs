using System;
using System.Collections.Generic;
using Autofac;
using BoDi;
using TechTalk.SpecFlow;
using TME.SpecTests.Mapping;
using TME.SpecTests.Mocks;

namespace TME.SpecTests.Hooks
{
    public delegate void RegisterMock(ContainerBuilder builder);
        
    [Binding]
    public class MainHooks
    {
        private readonly MapMockBuilder _mapMockBuilder;
        private readonly VariablesMockBuilder _variablesMockBuilder;
        private readonly StringsMockBuilder _stringsMockBuilder;
        private readonly IObjectContainer _objectContainer;
        private readonly IScenarioContext _scenarioContext;
        private readonly CommandHistoryMockBuilder _commandHistoryMockBuilder;


        private readonly IList<RegisterMock> _registerMocksList = new List<RegisterMock>();
        
        public IContainer Container { get; private set; } = null!;
        private ILifetimeScope? _lifetimeScope;
        private readonly Lazy<ILifetimeScope> _lazyLifetimeScope;
        public ILifetimeScope TestsContainer => _lazyLifetimeScope.Value;
        
        private bool _whenExecuted;
        
        public MainHooks(
            IObjectContainer objectContainer,
            ScenarioContext scenarioContext,
            CommandHistoryMockBuilder commandHistoryMockBuilder,
            VariablesMockBuilder variablesMockBuilder,
            StringsMockBuilder stringsMockBuilder,
            MapMockBuilder mapMockBuilder)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _commandHistoryMockBuilder = commandHistoryMockBuilder;
            _variablesMockBuilder = variablesMockBuilder;
            _stringsMockBuilder = stringsMockBuilder;
            _mapMockBuilder = mapMockBuilder;
                
            _lazyLifetimeScope = new Lazy<ILifetimeScope>( () => _lifetimeScope! );
        }

        public void RegisterMockHandler(RegisterMock mockHandler)
        {
            _registerMocksList.Add(mockHandler);
        }
        
        [BeforeScenario]
        private void BeforeScenario()
        {
            RegisterDependencies();
            RegisterMapping();
        }
        
        [BeforeScenarioBlock]
        private void BeforeScenarioBlock()
        {
            if (_scenarioContext.CurrentScenarioBlock != ScenarioBlock.When &&
                (_scenarioContext.CurrentScenarioBlock != ScenarioBlock.Then || _whenExecuted))
            {
                return;
            }
            
            _lifetimeScope = Container.BeginLifetimeScope( "MainHooks", builder =>
            {
                foreach (var handler in _registerMocksList)
                {
                    handler(builder);
                }
            });
            _whenExecuted = true;
        }

        [AfterScenario]
        private void AfterScenario()
        {
            _lifetimeScope?.Dispose();
            _lifetimeScope = null;
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
            _stringsMockBuilder.Build(containerBuilder);
        }

        private void RegisterMapping()
        {
            _objectContainer.RegisterInstanceAs(MapperProvider.GetMapper(
                new MappingConfiguration()
            ));
        }
        
    }
}