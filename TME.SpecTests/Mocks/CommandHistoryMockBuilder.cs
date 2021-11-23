using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.SpecTests.Fakes;
using TME.Types;

namespace TME.SpecTests.Mocks
{
    [Binding]
    public class CommandHistoryMockBuilder
    {
        private readonly CommandHistoryFake _commandHistoryFake;

        public CommandHistoryMockBuilder(
            CommandHistoryFake commandHistoryFake)
        {
            _commandHistoryFake = commandHistoryFake;
        }
        
        public void Build(ContainerBuilder containerBuilder)
        {
            var mock = new Mock<ICommandHistory>();
        
            // Task<bool> Save(Command command, Time duration, params object[] args);
            mock
                .Setup(m => m.Save(
                    It.IsAny<Command>(),
                    It.IsAny<Time>(),
                    It.IsAny<object[]>()))
                .Callback<Command, Time, object[]>((c, t, a) => _commandHistoryFake.Save(c, t, a))
                .ReturnsAsync(true);
            
            
            containerBuilder.RegisterInstance(mock.Object).SingleInstance();
        }
    }
}