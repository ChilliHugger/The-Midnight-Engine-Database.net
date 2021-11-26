using Autofac;
using TME.Interfaces;
using TME.Scenario.citadel;
using TME.Scenario.ddr;
using TME.Scenario.lom;

namespace TME.Scenario
{
    public class ScenarioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MidnightScenario>().Keyed<IScenario>(MidnightScenario.Tag);
            builder.RegisterType<CitadelScenario>().Keyed<IScenario>(CitadelScenario.Tag);
            builder.RegisterType<RevengeScenario>().Keyed<IScenario>(RevengeScenario.Tag);
        }
    }
}