using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Actions.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.SpecTests.Context;

namespace TME.SpecTests.Mocks
{
    [Binding]
    public class ActionMockBuilder
    {
        private readonly ActionsContext _actionsContext;

        public ActionMockBuilder(
            ActionsContext actionsContext)
        {
            _actionsContext = actionsContext;
        }
        
        public Mock<IObjectDroppedAction> Build(ContainerBuilder builder)
        {
            var mock = new Mock<IObjectDroppedAction>();

            mock
                .Setup(m => m.Execute(It.IsAny<IObject>()))
                .Callback<IObject>(thing => _actionsContext.ObjectDropped = thing)
                .Returns( ()=> _actionsContext.ObjectDroppedActionResult);

            builder.RegisterInstance(mock.Object).SingleInstance();
            
            return mock;
        }
        
    }
}