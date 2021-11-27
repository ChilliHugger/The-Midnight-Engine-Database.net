using Autofac;
using TME.Interfaces;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.citadel
{
    public class CitadelScenario : IScenario, IScenarioInfo
    {
        public static string Tag => "citadel";
        public uint Id =>  18;
        public uint Version => 200;
        public int DatabaseVersion => 100;
        public string Symbol => Tag;
        public string Name => "The Lords of Midnight 3 : The Dark Citadel";
        public string Website => "https://www.thelordsofmidnight.com";
        public string Support => "rorthron@thelordsofmidnight.com";
        public string Author => "Chris Wild";
        public string Copyright => "Copyright 1994 - 2021 Mike Singleton & Chris Wild";

        private const FeatureFlags Features = FeatureFlags.None;
        public bool IsFeature(FeatureFlags flags) => (Features & flags) == flags;
        
        public IScenarioInfo Info => this;
        
        public void Register(ContainerBuilder containerBuilder)
        {
        }
        
        public void InitialiseAfterGameLoad()
        {
        }
    }
}