using TechTalk.SpecFlow;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.SpecTests.Context
{
    [Binding]
    public class ActionsContext
    {
        public bool ObjectDroppedActionIsMocked { get; set; } = false;
        public IResult ObjectDroppedActionResult { get; set; } = Success.Default;
        public IThing? ObjectDropped { get; set; }

    }
}