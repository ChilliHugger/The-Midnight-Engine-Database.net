using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum TerrainInfoFlags : uint
    {
        Plural      = 1 << 0,   // terrain has plural
        Block       = 1 << 1,   // terrain blocks movement? ( not implemented )
        Interesting = 1 << 2,   // terrain is interesting
        Army        = 1 << 3,   // army is visible at terrain
        
        // Entity
        None                = EntityFlags.None,
        Disabled            = EntityFlags.Disabled
    }
}
