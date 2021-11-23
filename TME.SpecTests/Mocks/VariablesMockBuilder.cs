using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.SpecTests.Context;

namespace TME.SpecTests.Mocks
{
    [Binding]
    public class VariablesMockBuilder
    {
        private readonly VariablesContext _variablesContext;

        public VariablesMockBuilder(VariablesContext variablesContext)
        {
            _variablesContext = variablesContext;
        }
    

        public void Build(ContainerBuilder containerBuilder)
        {
            var mock = new Mock<IVariables>();
            
            mock
                .Setup(m => m.sv_time_dawn )
                .Returns(() => _variablesContext.sv_time_dawn);
            
            mock
                .Setup(m => m.sv_time_dawn )
                .Returns(() => _variablesContext.sv_time_night);
            
            mock
                .Setup(m => m.sv_time_scale )
                .Returns(() => _variablesContext.sv_time_scale);
            
            containerBuilder.RegisterInstance(mock.Object).SingleInstance();
        }
    }
}