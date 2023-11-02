using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;

namespace TME.Scenario.ddr
{
    public class RevengeScenario : IScenario, IScenarioInfo
    {
        private readonly IVariables _variables;
        private readonly IEntityResolver _entityResolver;
        
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
            IEntityResolver entityResolver,
            IVariables variables)
        {
            _variables = variables;
            _entityResolver = entityResolver;
        }
        
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<RevengeCharacter>().As<ICharacter>().WithAttributeFiltering();
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
            
            // 118 | OB_SWORD_THORTHAK > should be a SPEAR ?
            // 124 | OB_HAMMER_TORORTHANE > should be a BOW ?
            var sword = _entityResolver.EntityBySymbol<Object>("OB_SWORD_THORTHAK");
            if (sword != null)
            {
                sword.ObjectType = ObjectType.Spear;
                sword.Symbol = "OB_SPEAR_THORTHAK";
            }

            var hammer = _entityResolver.EntityBySymbol<Object>("OB_HAMMER_TORORTHANE");
            if (hammer != null)
            {
                hammer.ObjectType = ObjectType.Bow;
                hammer.Symbol = "OB_BOW_TORORTHANE";
            }
            
            var imgorarg = _entityResolver.EntityBySymbol<Character>("CH_IMGORARG");
            if (imgorarg != null)
            {
                imgorarg.Loyalty = Race.Dwarf;
            }
            
            var objects = new List<string> { 
                "OB_CROWN_VARENAND",
                "OB_CROWN_CARUDRIUM",
                "OB_SPELL_THIGRORN",
                "OB_RUNES_FINORN",
                "OB_CROWN_IMIRIEL" };

            foreach (var item in objects.Select(name => _entityResolver.EntityBySymbol<Object>(name)))
            {
                item?.SetFlags(ObjectFlags.Recruitment, true);
            }
        }
    }
}