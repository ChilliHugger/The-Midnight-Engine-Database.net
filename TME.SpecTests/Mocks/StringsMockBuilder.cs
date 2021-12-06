using Autofac;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.SpecTests.Context;
using TME.SpecTests.Fakes;

namespace TME.SpecTests.Mocks
{
    [Binding]
    public class StringsMockBuilder
    {
        private readonly StringsFake _stringsFake;

        public StringsMockBuilder(StringsFake stringsFake)
        {
            _stringsFake = stringsFake;
        }
    

        public void Build(ContainerBuilder containerBuilder)
        {
            var mock = new Mock<IStrings>();
            
            mock
                .Setup(m => m.GetBySymbol(It.IsAny<string>()) )
                .Returns<string>((symbol) => _stringsFake.GetBySymbol(symbol));

            containerBuilder.RegisterInstance(mock.Object).SingleInstance();
        }
    }
}