using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum RegimentFlags : uint
    {
        Direct      = 1 << 0,   // regiment will directly do their command
        InBattle    = 1 << 17,  // currently in battle
        Tunnel      = 1 << 19,	// currently in a tunnel (DDR)
        
        // Entity
        None                = EntityFlags.None,
        Disabled            = EntityFlags.Disabled
    }
}
