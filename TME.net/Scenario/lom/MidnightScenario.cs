using Autofac;
using TME.Interfaces;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.lom
{
    public class MidnightScenario : IScenario, IScenarioInfo
    {
        private readonly IVariables _variables;

        public static string Tag => "lom";
        public uint Id => 16;
        public uint Version => 200;
        public int DatabaseVersion => 7;
        public string Symbol => Tag;
        public string Name => "The Lords of Midnight";
        public string Website => "https://www.thelordsofmidnight.com";
        public string Support => "rorthron@thelordsofmidnight.com";
        public string Author => "Chris Wild";
        public string Copyright => "Copyright 1984 - 2021 Mike Singleton & Chris Wild";

        private const FeatureFlags Features = FeatureFlags.MoonRing |
                                              FeatureFlags.IceFear;

        public bool IsFeature(FeatureFlags flags) => (Features & flags) == flags;

        public IScenarioInfo Info => this;

        public MidnightScenario(
            IVariables variables)
        {
            _variables = variables;
        }

        public void Register(ContainerBuilder containerBuilder)
        {
        }
        
        public void InitialiseAfterGameLoad()
        {
            // TODO: Should be in database
            _variables.sv_max_character_following = 32;
            _variables.sv_max_characters_in_location = 128;
            _variables.sv_max_regiments_in_location = 128;
            _variables.sv_max_foe_armies_in_location = 128;
            _variables.sv_max_friend_armies_in_location = 128;
            _variables.sv_max_strongholds_in_location = 1;
            _variables.sv_max_routenodes_in_location = 1;
            _variables.sv_max_armies_in_location =
                _variables.sv_max_foe_armies_in_location + _variables.sv_max_friend_armies_in_location;
        }
    }
}