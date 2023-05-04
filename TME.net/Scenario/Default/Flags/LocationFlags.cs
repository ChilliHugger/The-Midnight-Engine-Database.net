using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum LocationFlags : uint
    {
        None            = 0,
        Mist            = 1 << 0,   // mist at location
        Tunnel          = 1 << 1,   // tunnel at location
        Creature        = 1 << 2,   // current thing at location
        Lord            = 1 << 3,   // character in location
        Army            = 1 << 4,   // army at location
        Seen            = 1 << 5,   // has been seen by the mapper
        Domain          = 1 << 6,   // special domain
        Special         = 1 << 7,   // is interesting
        Impassable      = 1 << 8,   // location is impassable
        Stronghold      = 1 << 9,   // stronghold at location
        RouteNode       = 1 << 10,  // route node at location
        Respawn         = 1 << 11,  // location can respawn its thing
        Unused13        = 1 << 12,  // ** currently not used **
        TunnelSmall     = 1 << 13,  // tunnel is confined space
        TunnelExit      = 1 << 14,  // can exit tunnel here
        TunnelEntrance  = 1 << 15,  // can enter tunnel here
        LookedAt        = 1 << 16,  // has been stood in front of
        Visited         = 1 << 17,  // player has visited this location
        TunnelLookedAt  = 1 << 18,  // player has seen the tunnel
        TunnelVisited   = 1 << 19,  // played has visited the tunnel location
        TunnelPassageway = 1 << 20, // tunnel is a passageway NOTE: this is technically ~(lt_tunnel_exit|lf_tunnel_entrance)
        Object          = 1 << 21,  // object to take
        Unused23        = 1 << 22,  // ** currently not used **
        Unused24        = 1 << 23,  // ** currently not used **
        Unused25        = 1 << 24,  // ** currently not used **
        Unused26        = 1 << 25,  // ** currently not used **
        Unused27        = 1 << 26,  // ** currently not used **
        Unused28        = 1 << 27,  // ** currently not used **
    }
}
