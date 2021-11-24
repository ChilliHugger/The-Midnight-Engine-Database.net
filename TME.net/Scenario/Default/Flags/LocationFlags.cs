using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum LocationFlags : uint
    {
        Mist            = 1 << (0),     // DDR Scenario, currently not used
        Tunnel          = 1 << (1),     // DDR Scenario, currently not used
        Creature        = 1 << (2),     // DDR Scenario, currently not used
        Lord            = 1 << (3),     // character in location
        Army            = 1 << (4),     // army at location
        Seen            = 1 << (5),     // has been seen by the mapper
        Domain          = 1 << (6),     // special domain
        Special         = 1 << (7),     // is interesting
        Building        = 1 << (8),     // currently not used
        Stronghold      = 1 << (9),     // stronghold at location
        Routenode       = 1 << (10),    // routenode at location
        TerrainCustom   = 1 << (11),    // terrain is custom stored
        ObjectCustom    = 1 << (12),    // object is custom stored
        AreaCustom      = 1 << (13),    // area is custom stored

        // DDR
        TunnelExit      = 1 << (14),    // can exit tunnel here
        TunnelEntrance  = 1 << (15),    // can enter tunnel here

        LookedAt        = 1 << (16),    // has been stood in front of
        Visited         = 1 << (17),    // player has visited this location

        // DDR
        TunnelLookedAt  = 1 << (18),    // player has seen the tunnel
        TunnelVisited   = 1 << (19),    // played has visited the tunnel location
        TunnelPassageway = 1 << (20),   // tunnel is a pssageway NOTE: this is technicall ~(lt_tunnel_exit|lf_tunnel_entrance)
        Object          = 1 << (21),    // object to take
        ObjectSpecial   = 1 << (22),    // the object to take is one of the special object

    }
}
