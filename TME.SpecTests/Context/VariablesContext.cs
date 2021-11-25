using System.Collections.Generic;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.SpecTests.Context
{
    [Binding]
    public class VariablesContext : IVariables
    {
        public void Initialise()
        {
        }

        public List<KeyValuePair<string, string>> GetValues()
        {
            throw new System.NotImplementedException();
        }

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
        public int sv_time_night { get; set; } = 0;
        public int sv_time_dawn { get; set; } = 16;
        public int sv_time_scale { get; set; } = 2;
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
    }
}