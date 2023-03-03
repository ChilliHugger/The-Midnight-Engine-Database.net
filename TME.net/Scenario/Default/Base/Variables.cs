using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Logging;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public class Variables : IVariables, ISerializableLoad
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, string> _propertyMapping;
        private readonly IContainer _container;
        
        private IEntityResolver _entityResolver = new NoopEntityResolver();
        private IStrings _strings = new NoopStrings();
        
        public double sv_database_version { get; set; }
        public double sv_battle_default_energy_drain { get; set; }
        public double sv_battle_default_char_energy_drain { get; set; }
        public double sv_battle_success_regiment_warriors { get; set; }
        public double sv_battle_success_regiment_riders { get; set; }
        public double sv_character_default_energy_inc { get; set; }
        public double sv_character_recruit_amount { get; set; }
        public double sv_character_guard_amount { get; set; }
        public double sv_character_max_riders { get; set; }
        public double sv_character_max_warriors { get; set; }
        public double sv_character_max_energy { get; set; }
        public double sv_warriors_max_energy { get; set; }
        public double sv_warriors_default_energy_inc { get; set; }
        public double sv_riders_default_energy_inc { get; set; }
        public double sv_riders_max_energy { get; set; }
        public double sv_object_energy_shelter { get; set; }
        public double sv_object_energy_shadowsofdeath { get; set; }
        public double sv_object_energy_watersoflife { get; set; }
        public double sv_look_forward_distance { get; set; }
        public double sv_regiment_default_moves { get; set; }
        public double sv_stronghold_default_empty { get; set; }
        public double sv_stronghold_default_min { get; set; }
        public double sv_stronghold_default_max { get; set; }
        public bool sv_stronghold_default_allow_respawn { get; set; }
        public double sv_stronghold_default_respawn_amount { get; set; }
        public double sv_success_fey_on_horse_adjuster { get; set; }
        public double sv_success_base_level { get; set; }
        public double sv_success_riders_energy_mountain { get; set; }
        public double sv_success_riders_energy_other { get; set; }
        public bool sv_always_attempt_recruit { get; set; }

        public string sv_map_file { get; set; } = "";
        public double sv_map_width { get; set; }
        public double sv_map_height { get; set; }
        public bool sv_auto_unhide { get; set; }
        public bool sv_auto_seek { get; set; }
        public double sv_character_default_memory_age { get; set; }

        public int sv_time_night { get; set; }
        public int sv_time_dawn { get; set; }
        public int sv_time_scale { get; set; }

        public int sv_despondent_scale { get; set; }
        public int sv_fear_scale { get; set; }
        public int sv_courage_scale { get; set; }
        public int sv_energy_scale { get; set; }
        public int sv_reckless_scale { get; set; }

        public bool sv_collate_battle_areas { get; set; }

        public MXId sv_character_friend { get; set; } = MXId.None;
        public MXId sv_character_foe { get; set; } = MXId.None;
        public IEnumerable<MXId> sv_character_default { get; set; } = new List<MXId>();
        public IEnumerable<MXId> sv_guidance { get; set; } = new List<MXId>();
        
        public int sv_days { get; set; }
        public int sv_variables { get; set; }
        public int sv_stronghold_adjuster { get; set; }
        public int sv_controlled_character { get; set; }
        
        public int sv_energy_max { get; set; }
        public int sv_strength_max { get; set; }

        public bool sv_cheat_army_no_block { get; set; }
        public bool sv_cheat_nasty_no_block { get; set; }
        public bool sv_cheat_movement_free { get; set; }
        public bool sv_cheat_movement_cheap { get; set; }
        public bool sv_cheat_commands_free { get; set; }
        public bool sv_cheat_always_win_fight { get; set; }

        public int sv_energy_cannot_continue { get; set; }
        
        public int sv_max_character_following { get; set; }
        public int sv_max_characters_in_location { get; set; }
        public int sv_max_regiments_in_location { get; set; }
        public int sv_max_foe_armies_in_location { get; set; }
        public int sv_max_friend_armies_in_location { get; set; }
        public int sv_max_strongholds_in_location { get; set; }
        public int sv_max_routenodes_in_location { get; set; }
        public int sv_max_armies_in_location { get; set; }

        private static readonly VariableDefinition[] VariableDefinitions = new[]
        {
            new VariableDefinition(nameof(sv_database_version),                 "DATABASE_VERSION",                 "1.00" ),
            new VariableDefinition(nameof(sv_battle_default_energy_drain),      "BATTLE_DEFAULT_ENERGY_DRAIN",      "24" ),
            new VariableDefinition(nameof(sv_battle_default_char_energy_drain), "BATTLE_DEFAULT_CHAR_ENERGY_DRAIN", "20" ),
            new VariableDefinition(nameof(sv_battle_success_regiment_warriors), "BATTLE_SUCCESS_REGIMENT_WARRIORS", "5" ),
            new VariableDefinition(nameof(sv_battle_success_regiment_riders),   "BATTLE_SUCCESS_REGIMENT_RIDERS",   "4" ),
            new VariableDefinition(nameof(sv_character_default_energy_inc),     "CHARACTER_DEFAULT_ENERGY_INC",     "9" ),
            new VariableDefinition(nameof(sv_character_recruit_amount),         "CHARACTER_RECRUIT_AMOUNT",         "100" ),
            new VariableDefinition(nameof(sv_character_guard_amount),           "CHARACTER_GUARD_AMOUNT",           "100" ),
            new VariableDefinition(nameof(sv_character_max_riders),             "CHARACTER_MAX_RIDERS",             "1200" ),
            new VariableDefinition(nameof(sv_character_max_warriors),           "CHARACTER_MAX_WARRIORS",           "1200" ),
            new VariableDefinition(nameof(sv_character_max_energy),             "CHARACTER_MAX_ENERGY",             "127" ),
            new VariableDefinition(nameof(sv_warriors_max_energy),              "WARRIORS_MAX_ENERGY",              "127" ),
            new VariableDefinition(nameof(sv_warriors_default_energy_inc),      "WARRIORS_DEFAULT_ENERGY_INC",      "4" ),
            new VariableDefinition(nameof(sv_riders_default_energy_inc),        "RIDERS_DEFAULT_ENERGY_INC",        "6" ),
            new VariableDefinition(nameof(sv_riders_max_energy),                "RIDERS_MAX_ENERGY",                "127" ),
            new VariableDefinition(nameof(sv_object_energy_shelter),            "OBJECT_ENERGY_SHELTER",            "10" ),
            new VariableDefinition(nameof(sv_object_energy_shadowsofdeath),     "OBJECT_ENERGY_SHADOWSOFDEATH",     "0" ),
            new VariableDefinition(nameof(sv_object_energy_watersoflife),       "OBJECT_ENERGY_WATERSOFLIFE",       "120" ),
            // ReSharper disable once StringLiteralTypo
            new VariableDefinition(nameof(sv_look_forward_distance),            "LOOKFORWARDDISTANCE",              "3" ),
            new VariableDefinition(nameof(sv_regiment_default_moves),           "REGIMENT_DEFAULT_MOVES",           "6" ),
            new VariableDefinition(nameof(sv_stronghold_default_empty),         "STRONGHOLD_DEFAULT_EMPTY",         "20" ),
            new VariableDefinition(nameof(sv_stronghold_default_min),           "STRONGHOLD_DEFAULT_MIN",           "100" ),
            new VariableDefinition(nameof(sv_stronghold_default_max),           "STRONGHOLD_DEFAULT_MAX",           "1200" ),
            new VariableDefinition(nameof(sv_stronghold_default_allow_respawn), "STRONGHOLD_DEFAULT_ALLOW_RESPAWN", "NO" ),
            new VariableDefinition(nameof(sv_stronghold_default_respawn_amount),"STRONGHOLD_DEFAULT_RESPAWN_AMOUNT","50" ),
            new VariableDefinition(nameof(sv_success_fey_on_horse_adjuster),    "SUCCESS_FEY_ON_HORSE_ADJUSTER",    "64" ),
            new VariableDefinition(nameof(sv_success_base_level),               "SUCCESS_BASE_LEVEL",               "24" ),
            new VariableDefinition(nameof(sv_success_riders_energy_mountain),   "SUCCESS_RIDERS_ENERGY_MOUNTAIN",   "32" ),
            new VariableDefinition(nameof(sv_success_riders_energy_other),      "SUCCESS_RIDERS_ENERGY_OTHER",      "64" ),
            new VariableDefinition(nameof(sv_always_attempt_recruit),           "ALWAYS_ATTEMPT_RECRUIT",           "FALSE" ),

            new VariableDefinition(nameof(sv_map_file),                         "MAP_FILE",                         "map" ),
            new VariableDefinition(nameof(sv_map_width),                        "MAP_WIDTH",                        "66" ),
            new VariableDefinition(nameof(sv_map_height),                       "MAP_HEIGHT",                       "62" ),

            new VariableDefinition(nameof(sv_auto_unhide),                      "CHARACTER_AUTO_UNHIDE",            "NO" ),
            new VariableDefinition(nameof(sv_auto_seek),                        "CHARACTER_AUTO_SEEK",              "NO" ),
            new VariableDefinition(nameof(sv_character_default_memory_age),     "CHARACTER_DEFAULT_MEMORY_AGE",     "10" ),

            new VariableDefinition(nameof(sv_time_night),                       "TIME_NIGHT",                       "0" ),
            new VariableDefinition(nameof(sv_time_dawn),                        "TIME_DAWN",                        "16" ), // lords 16 ddr 31
			new VariableDefinition(nameof(sv_time_scale),                       "TIME_SCALE",                       "2" ), // lords 2 ddr 4

			new VariableDefinition(nameof(sv_energy_max),                       "ENERGY_MAX",                       "127" ),
            new VariableDefinition(nameof(sv_strength_max),                     "STRENGTH_MAX",                     "100" ),

            new VariableDefinition(nameof(sv_fear_scale),                       "FEAR_SCALE",                       "64" ),
            new VariableDefinition(nameof(sv_courage_scale),                    "COURAGE_SCALE",                    "1" ),
            new VariableDefinition(nameof(sv_energy_scale),                     "ENERGY_SCALE",                     "16" ), // lom  16 ddr 32
			new VariableDefinition(nameof(sv_despondent_scale),                 "DESPONDENT_SCALE",                 "32" ),
            new VariableDefinition(nameof(sv_reckless_scale),                   "RECKLESS_SCALE",                   "32" ),

            new VariableDefinition(nameof(sv_collate_battle_areas),             "COLLATE_BATTLE_AREAS",             "NO" ),

            new VariableDefinition(nameof(sv_character_friend),                 "CHARACTER_FRIEND",                 "CH_LUXOR" ),
            new VariableDefinition(nameof(sv_character_foe),                    "CHARACTER_FOE",                    "CH_DOOMDARK" ),
            new VariableDefinition(nameof(sv_character_default),                "CHARACTER_DEFAULT",                "CH_LUXOR|CH_MORKIN|CH_CORLETH|CH_RORTHRON" ),
            new VariableDefinition(nameof(sv_guidance),                         "SEEK_MESSAGES" ),

            new VariableDefinition(nameof(sv_days),                             "DAYS",                             "0" ),
            new VariableDefinition(nameof(sv_stronghold_adjuster),              "STRONGHOLD_ADJUSTER",              "0" ),
            new VariableDefinition(nameof(sv_controlled_character),             "CONTROLLED_CHARACTER",             "0" ),
            new VariableDefinition(nameof(sv_energy_cannot_continue),           "ENERGY_CANNOT_CONTINUE",           "0" ),
            
            new VariableDefinition(nameof(sv_max_character_following),           "MAX_CHARACTERS_FOLLOWING",        "32" ),
            new VariableDefinition(nameof(sv_max_characters_in_location),        "MAX_CHARACTERS_IN_LOCATION",      "128" ),
            new VariableDefinition(nameof(sv_max_regiments_in_location),         "MAX_REGIMENTS_IN_LOCATION",       "128" ),
            new VariableDefinition(nameof(sv_max_foe_armies_in_location),        "MAX_FOE_ARMIES_IN_LOCATION",      "128" ),
            new VariableDefinition(nameof(sv_max_friend_armies_in_location),     "MAX_FRIEND_ARMIES_IN_LOCATION",   "128" ),
            new VariableDefinition(nameof(sv_max_strongholds_in_location),       "MAX_STRONGHOLDS_IN_LOCATION",     "1" ),
            new VariableDefinition(nameof(sv_max_routenodes_in_location),        "MAX_ROUTENODES_IN_LOCATION",      "1" ),
            new VariableDefinition(nameof(sv_max_armies_in_location),            "MAX_ARMIES_IN_LOCATION",          "256" ),

        };

        public Variables(ILogger<Variables> logger, IDependencyContainer dependencyContainer)
        {
            _logger = logger;
            _container = dependencyContainer.CurrentContainer;
            _propertyMapping = VariableDefinitions
                .ToDictionary(v => v.VariableName, v => v.PropertyName);
        }
        
        public void Initialise()
        {
            _entityResolver = _container.Resolve<IEntityResolver>();
            _strings = _container.Resolve<IStrings>();
            foreach (var (propertyName, _, defaultValue) in VariableDefinitions)
            {
                SetValue(propertyName, defaultValue);
            }
        }

        private string ResolveMxId(MXId id)
        {
            if (id.Type == EntityType.String)
            {
                return _strings.GetById(id)?.Symbol ?? "";
            }
            return _entityResolver.EntityById(id)?.Symbol ?? "";
        }

        private MXId ResolveBySymbol(string symbol)
        {
            var stringItem = _strings.GetBySymbol(symbol);
            if (stringItem == null)
            {
                return _entityResolver.EntityBySymbol(symbol)?.Id ?? MXId.None;
            }
            return stringItem.Id;
        }
        
        private string GetValue(string propertyName)
        {
            var t = GetType();

            var p = t.GetProperty(propertyName);

            var valueType = p?.PropertyType;
            var value = p?.GetValue(this);

            if (value == null)
            {
                if (valueType == typeof(bool))
                {
                    return "No";
                }
                if (valueType == typeof(string))
                {
                    return "";
                }
                return "0";
            }
            
            if (valueType == typeof(MXId))
            {
                if ( value is MXId mxId )
                {
                    return ResolveMxId(mxId);
                }
                return "";
            }

            if (valueType == typeof(bool) && value is bool yesNo)
            {
                return yesNo ? "Yes" : "No" ;
            }

            if (valueType == typeof(IEnumerable<MXId>) && value is IEnumerable<MXId>)
            {
                if (value is IEnumerable<MXId> values)
                {
                    var result = values
                        .Select(ResolveMxId)
                        .Where(s => !string.IsNullOrEmpty(s));
                    return string.Join("|", result);
                }
                return "";
            }

            return value.ToString() ?? "";

        }

        private void SetValue(string propertyName, string propertyValue)
        {
            var t = this.GetType();

            var p = t.GetProperty(propertyName);
            if (p == null)
            {
                return;
            }
            
            var valueType = p.PropertyType;

            if (valueType == typeof(bool))
            {
                if (propertyValue.Equals("YES",StringComparison.InvariantCultureIgnoreCase))
                {
                    p.SetValue(this, true);
                }
                else if (bool.TryParse(propertyValue, out var value))
                {
                    p.SetValue(this, value);
                }
                else
                {
                    p.SetValue(this, default(bool));
                }
            }
            else if (valueType == typeof(double))
            {
                p.SetValue(this, double.TryParse(propertyValue, out var value) ? value : default);
            }
            else if (valueType == typeof(int))
            {
                p.SetValue(this, int.TryParse(propertyValue, out var value) ? value : default);
            }
            else if (valueType == typeof(string))
            {
                p.SetValue(this, propertyValue);
            }
            else if (valueType == typeof(MXId))
            {
                var entity = _entityResolver.EntityBySymbol(propertyValue);
                if (entity != null)
                {
                    p.SetValue(this,entity.Id);
                }
                //_logger.LogWarning($"Unsupported Variable['{propertyName}']={propertyValue}");
            }
            else if (valueType == typeof(IEnumerable<MXId>))
            {
                var items = propertyValue
                    .Split('|')
                    .Select(ResolveBySymbol)
                    .Where(e => e != MXId.None);

                if (items.Any())
                {
                    p.SetValue(this,items);
                }
                //_logger.LogWarning($"Unsupported Variable['{propertyName}']={propertyValue}");
            }

        }

        public bool Load(ISerializeContext context)
        {
            if (context.Section == DataSection.VariableCount)
            {
                sv_variables = context.Reader.Int32();
                Initialise();
                return true;
            }
            
            if (context.Section != DataSection.Variables)
            {
                return true;
            }
            
            var variable = new DatabaseVariable();
            
            for ( var ii=0; ii<sv_variables; ii++ )
            {
                if (!variable.Load(context))
                {
                    _logger.LogError("Error loading variable: {index}", ii);
                }
                
                if (_propertyMapping.TryGetValue(variable.Symbol, out var propertyName))
                {
                    Console.WriteLine($"Variable['{variable.Symbol}']={variable.Value}");
                    SetValue(propertyName,variable.Value);
                }
                else
                {
                    _logger.LogWarning("Unsupported Variable['{name}']={value}", variable.Symbol, variable.Value);
                }
            }

            return true;
        }
        
        public List<DatabaseVariable> GetValues()
        {
            return (
                from v in VariableDefinitions 
                let value = GetValue(v.PropertyName) 
                select new DatabaseVariable { Symbol = v.VariableName, Value = value }
                ).ToList();
        }
    }
}
