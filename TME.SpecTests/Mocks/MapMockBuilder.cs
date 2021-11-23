using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.SpecTests.Context;

namespace TME.SpecTests.Mocks
{
    [Binding]
    public class MapMockBuilder
    {
        private delegate void SetAtDelegate(Loc loc, ref MapLoc mapLoc);

        private readonly MapContext _mapContext;

        public MapMockBuilder(MapContext mapContext)
        {
            _mapContext = mapContext;
        }
    

    public void Build(ContainerBuilder containerBuilder)
        {
            var mock = new Mock<IMap>();
            
            mock
                .Setup(m => m.GetAt(It.IsAny<Loc>()))
                .Returns(() => _mapContext.CurrentLocation);

            mock
                .Setup(m => m.SetAt(It.IsAny<Loc>(), ref It.Ref<MapLoc>.IsAny))
                .Callback(new SetAtDelegate((Loc loc, ref MapLoc mapLoc) =>
                {
                    _mapContext.CurrentLocation = mapLoc;
                }));
            
            containerBuilder.RegisterInstance(mock.Object).SingleInstance();
        }
    }
}