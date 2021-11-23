using Moq;
using TechTalk.SpecFlow;
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
        
        public Mock<IAction> Build()
        {
            var mock = new Mock<IAction>();

            mock
                .Setup(m => m.Execute(It.IsAny<object[]>()))
                .Callback<object[]>((args)=> _actionsContext.ObjectDropped = args[0] as IThing)
                .ReturnsAsync( ()=>
                {
                    return _actionsContext.ObjectDroppedActionResult;
                });
            
            return mock;
        }
        
    }
}