using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Interfaces;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr
{
    public class RevengeScenario : IScenario, IScenarioInfo
    {
        private readonly IVariables _variables;
        public static string Tag => "ddr";
        public uint Id =>  17;
        public uint Version => 200;
        public int DatabaseVersion => 11;
        public string Symbol => Tag;
        public string Name => "Doomdark's Revenge";
        public string Website => "https://www.doomdarksrevenge.com";
        public string Support => "rorthron@doomdarksrevenge.com";
        public string Author => "Chris Wild";
        public string Copyright => "Copyright 1985 - 2021 Mike Singleton & Chris Wild";

        private const FeatureFlags Features = FeatureFlags.Approach |
                                              FeatureFlags.Recruit |
                                              FeatureFlags.RecruitTime |
                                              FeatureFlags.Take |
                                              FeatureFlags.Give;
        public bool IsFeature(FeatureFlags flags) => (Features & flags) == flags;
        
        public IScenarioInfo Info => this;
        
        public RevengeScenario(
            IVariables variables)
        {
            _variables = variables;
        }
        
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<RevengeLord>().As<ICharacter>().WithAttributeFiltering();
            containerBuilder.RegisterType<RevengeStronghold>().As<IStronghold>();
            containerBuilder.RegisterType<RevengeObject>().As<IObject>();
        }
        
        public void InitialiseAfterGameLoad()
        {
            // TODO: Should be in database
            _variables.sv_max_character_following = 32;
            _variables.sv_max_characters_in_location = 32;
            _variables.sv_max_regiments_in_location = 64;
            _variables.sv_max_foe_armies_in_location = 32;
            _variables.sv_max_friend_armies_in_location = 32;
            _variables.sv_max_strongholds_in_location = 1;
            _variables.sv_max_routenodes_in_location = 1;
            _variables.sv_max_armies_in_location = 
                _variables.sv_max_foe_armies_in_location + _variables.sv_max_friend_armies_in_location ;
        }
    }
}