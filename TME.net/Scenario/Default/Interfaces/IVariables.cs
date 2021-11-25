using System.Collections.Generic;
using TME.Types;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable InconsistentNaming
namespace TME.Scenario.Default.Interfaces
{
    public interface IVariables
    {
        void Initialise();
        List<KeyValuePair<string, string>> GetValues();
        
        double sv_database_version { get; set; }
        double sv_battle_default_energy_drain { get; set; }
        double sv_battle_default_char_energy_drain { get; set; }
        double sv_battle_success_regiment_warriors { get; set; }
        double sv_battle_success_regiment_riders { get; set; }
        double sv_character_default_energy_inc { get; set; }
        double sv_character_recruit_amount { get; set; }
        double sv_character_guard_amount { get; set; }
        double sv_character_max_riders { get; set; }
        double sv_character_max_warriors { get; set; }
        double sv_character_max_energy { get; set; }
        double sv_warriors_max_energy { get; set; }
        double sv_warriors_default_energy_inc { get; set; }
        double sv_riders_default_energy_inc { get; set; }
        double sv_riders_max_energy { get; set; }
        double sv_object_energy_shelter { get; set; }
        double sv_object_energy_shadowsofdeath { get; set; }
        double sv_object_energy_watersoflife { get; set; }
        double sv_look_forward_distance { get; set; }
        double sv_regiment_default_moves { get; set; }
        double sv_stronghold_default_empty { get; set; }
        double sv_stronghold_default_min { get; set; }
        double sv_stronghold_default_max { get; set; }
        bool sv_stronghold_default_allow_respawn { get; set; }
        double sv_stronghold_default_respawn_amount { get; set; }
        double sv_success_fey_on_horse_adjuster { get; set; }
        double sv_success_base_level { get; set; }
        double sv_success_riders_energy_mountain { get; set; }
        double sv_success_riders_energy_other { get; set; }
        bool sv_always_attempt_recruit { get; set; }

        string sv_map_file { get; set; }
        double sv_map_width { get; set; }
        double sv_map_height { get; set; }
        bool sv_auto_unhide { get; set; }
        bool sv_auto_seek { get; set; }
        double sv_character_default_memory_age { get; set; }

        int sv_time_night { get; set; }
        int sv_time_dawn { get; set; }
        int sv_time_scale { get; set; }

        int sv_despondent_scale { get; set; }
        int sv_fear_scale { get; set; }
        int sv_courage_scale { get; set; }
        int sv_energy_scale { get; set; }
        int sv_reckless_scale { get; set; }

        bool sv_collate_battle_areas { get; set; }

        MXId sv_character_friend { get; set; }
        MXId sv_character_foe { get; set; }
        IEnumerable<MXId> sv_character_default { get; set; }
        IEnumerable<MXId> sv_guidance { get; set; }

        int sv_characters { get; set; }
        int sv_routenodes { get; set; }
        int sv_strongholds { get; set; }
        int sv_regiments { get; set; }
        int sv_places { get; set; }
        int sv_directions { get; set; }
        int sv_objects { get; set; }
        int sv_units { get; set; }
        int sv_races { get; set; }
        int sv_genders { get; set; }
        int sv_terrains { get; set; }
        int sv_areas { get; set; }
        int sv_commands { get; set; }
        int sv_missions { get; set; }
        int sv_victories { get; set; }

        int sv_object_powers { get; set; }
        int sv_object_types { get; set; }

        int sv_days { get; set; }
        int sv_attributes { get; set; }
        int sv_variables { get; set; }
        int sv_stronghold_adjuster { get; set; }
        int sv_controlled_character { get; set; }
        
        int sv_energy_max { get; set; }
        int sv_strength_max { get; set; }

        bool sv_cheat_army_no_block { get; set; }
        bool sv_cheat_nasty_no_block { get; set; }
        bool sv_cheat_movement_free { get; set; }
        bool sv_cheat_movement_cheap { get; set; }
        bool sv_cheat_commands_free { get; set; }
        bool sv_cheat_always_win_fight { get; set; }

        int sv_energy_cannot_continue { get; set; }
    }
}
