using TechTalk.SpecFlow;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.SpecTests.Context
{
    [Binding]
    public class MapContext
    {
        public MapLoc CurrentLocation { get; set; }

        [BeforeScenario(Order = 10)]
        private void BeforeScenario()
        {
            CurrentLocation = new MapLoc
            {
                Terrain = Terrain.Citadel,
                Flags = 0,
                Thing = ThingType.None,
                HasObject = false
            };
        }
    }
}