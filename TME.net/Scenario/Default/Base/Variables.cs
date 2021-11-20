using System;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Base
{
    public class Variables : IVariables, ISerializable
    {
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
        public double sv_lookforwarddistance { get; set; }
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

        public string sv_map_file { get; set; }
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

        public MXId sv_character_friend { get; set; }
        public MXId sv_character_foe { get; set; }
        public IEnumerable<MXId> sv_character_default { get; set; }
        public IEnumerable<MXId> sv_guidance { get; set; }

        public int sv_characters { get; set; }
        public int sv_routenodes { get; set; }
        public int sv_strongholds { get; set; }
        public int sv_regiments { get; set; }
        public int sv_places { get; set; }
        public int sv_directions { get; set; }
        public int sv_objects { get; set; }
        public int sv_units { get; set; }
        public int sv_races { get; set; }
        public int sv_genders { get; set; }
        public int sv_terrains { get; set; }
        public int sv_areas { get; set; }
        public int sv_commands { get; set; }
        public int sv_missions { get; set; }
        public int sv_victories { get; set; }

        public int sv_object_powers { get; set; }
        public int sv_object_types { get; set; }

        public int sv_days { get; set; }
        public int sv_attributes { get; set; }
        public int sv_variables { get; set; }
        public int sv_strongholdadjuster { get; set; }
        public int sv_controlled_character { get; set; }

        //		bool	sv_failed_approach_battle  {get;set;}
        //		bool	sv_use_cowardess  {get;set;}
        //		bool	sv_use_despondency  {get;set;}
        public int sv_energy_max { get; set; }
        public int sv_strength_max { get; set; }

        public bool sv_cheat_armies_noblock { get; set; }
        public bool sv_cheat_nasties_noblock { get; set; }
        public bool sv_cheat_movement_free { get; set; }
        public bool sv_cheat_movement_cheap { get; set; }
        public bool sv_cheat_commands_free { get; set; }
        public bool sv_cheat_always_win_fight { get; set; }

        public int sv_energy_cannot_continue { get; set; }

        static readonly VariableDefinition[] VariableDefinitions = new VariableDefinition[]
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
            new VariableDefinition(nameof(sv_lookforwarddistance),              "LOOKFORWARDDISTANCE",              "3" ),
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
            new VariableDefinition(nameof(sv_guidance),                         "SEEK_MESSAGES",                    "" ),

            // Not stored in the database
            new VariableDefinition(nameof(sv_characters),                       "CHARACTERS",                       "0" ),
            new VariableDefinition(nameof(sv_routenodes),                       "ROUTENODES",                       "0" ),
            new VariableDefinition(nameof(sv_strongholds),                      "STRONGHOLDS",                      "0" ),
            new VariableDefinition(nameof(sv_regiments),                        "REGIMENTS",                        "0" ),
            new VariableDefinition(nameof(sv_places),                           "PLACES",                           "0" ),
            new VariableDefinition(nameof(sv_directions),                       "DIRECTIONS",                       "0" ),
            new VariableDefinition(nameof(sv_objects),                          "OBJECTS",                          "0" ),
            new VariableDefinition(nameof(sv_units),                            "UNITS",                            "0" ),
            new VariableDefinition(nameof(sv_races),                            "RACES",                            "0" ),
            new VariableDefinition(nameof(sv_genders),                          "GENDERS",                          "0" ),
            new VariableDefinition(nameof(sv_terrains),                         "TERRAINS",                         "0" ),
            new VariableDefinition(nameof(sv_areas),                            "AREAS",                            "0" ),
            new VariableDefinition(nameof(sv_commands),                         "COMMANDS",                         "0" ),
            new VariableDefinition(nameof(sv_missions),                         "MISSIONS",                         "0" ),
            new VariableDefinition(nameof(sv_victories),                        "VICTORIES",                        "0" ),
            new VariableDefinition(nameof(sv_attributes),                       "ATTRIBUTES",                       "0" ),
            new VariableDefinition(nameof(sv_variables),                        "VARIABLES",                        "0" ),
            new VariableDefinition(nameof(sv_days),                             "DAYS",                             "0" ),
            new VariableDefinition(nameof(sv_strongholdadjuster),               "STRONGHOLD_ADJUSTER",              "0" ),
            new VariableDefinition(nameof(sv_controlled_character),             "CONTROLLED_CHARACTER",             "0" ),
            new VariableDefinition(nameof(sv_energy_cannot_continue),           "ENERGY_CANNOT_CONTINUE",           "0" ),
            new VariableDefinition(nameof(sv_object_powers),                    "OBJECT_POWERS",                    "0" ),
            new VariableDefinition(nameof(sv_object_types),                     "OBJECT_TYPES",                     "0" ),

        };

        public void Initialise()
        {
            foreach (var v in VariableDefinitions)
            {
                SetValue(v.PropertyName, v.DefaultValue);
            }
        }

        private string GetValue(string propertyName)
        {
            var t = this.GetType();

            var p = t.GetProperty(propertyName);

            var valueType = p.PropertyType;
            var value = p.GetValue(this);

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
                return value.ToString();
            }
            else if (valueType == typeof(bool) && value is bool yesNo)
            {
                return yesNo ? "Yes" : "No" ;
            }
            else if (valueType == typeof(IEnumerable<MXId>) && value is IEnumerable<MXId> values)
            {
                return string.Join("|",value);
            }

            return value.ToString();

        }

        private void SetValue(string propertyName, string propertyValue)
        {
            var t = this.GetType();

            var p = t.GetProperty(propertyName);

            //object valueType = p.GetValue(this, null);

            var valueType = p.PropertyType;

            if (valueType == typeof(bool))
            {
                if (propertyValue.Equals("YES",StringComparison.InvariantCultureIgnoreCase))
                {
                    p.SetValue(this, true);
                }
                else if (bool.TryParse(propertyValue, out bool value))
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
                if (double.TryParse(propertyValue, out double value))
                {
                    p.SetValue(this, value);
                }
                else
                {
                    p.SetValue(this, default(double));
                }
            }
            else if (valueType == typeof(int))
            {
                if (int.TryParse(propertyValue, out int value))
                {
                    p.SetValue(this, value);
                }
                else
                {
                    p.SetValue(this, default(int));
                }
            }
            else if (valueType == typeof(string))
            {
                p.SetValue(this, propertyValue);
            }
            else if (valueType == typeof(MXId))
            {
                //p.SetValue(this, 0);
            }
            else if (valueType == typeof(IEnumerable<MXId>))
            {
                //var values = new List<MXId>();
                //p.SetValue(this, values);
            }

        }


        public bool Load(ISerializeContext context)
        {
            sv_characters = context.Reader.ReadInt32();
            sv_regiments = context.Reader.ReadInt32();
            sv_routenodes = context.Reader.ReadInt32();
            sv_strongholds = context.Reader.ReadInt32();
            sv_places = context.Reader.ReadInt32();
            sv_objects = context.Reader.ReadInt32();
            sv_missions = context.Reader.ReadInt32();
            sv_victories = context.Reader.ReadInt32();
            sv_directions = context.Reader.ReadInt32();
            sv_units = context.Reader.ReadInt32();
            sv_races = context.Reader.ReadInt32();
            sv_genders = context.Reader.ReadInt32();
            sv_terrains = context.Reader.ReadInt32();
            sv_areas = context.Reader.ReadInt32();
            sv_commands = context.Reader.ReadInt32();
            sv_variables = context.Reader.ReadInt32();
            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string>> GetValues()
        {
            var vars = new VariableDefinition[]
            {
                new VariableDefinition(nameof(sv_character_friend), "CHARACTER_FRIEND", "CH_LUXOR"),
                new VariableDefinition(nameof(sv_character_foe), "CHARACTER_FOE", "CH_DOOMDARK"),
                new VariableDefinition(nameof(sv_character_default), "CHARACTER_DEFAULT",
                    "CH_LUXOR|CH_MORKIN|CH_CORLETH|CH_RORTHRON"),
            };
            
            return (
                from v in vars 
                let value = GetValue(v.PropertyName) 
                select new KeyValuePair<string, string>(v.VariableName, value)
                ).ToList();
        }
    }
}
