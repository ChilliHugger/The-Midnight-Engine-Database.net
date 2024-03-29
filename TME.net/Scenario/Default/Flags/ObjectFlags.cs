﻿using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum ObjectFlags : uint
    {
        Fight       = 1 << 0,   // object can be fought
        Pickup      = 1 << 1,   // object can be picked up
        Drop        = 1 << 2,   // object can be dropped
        Weapon      = 1 << 3,   // object is a weapon
        See         = 1 << 4,   // object can be seen
        Remove      = 1 << 5,   // object is removed from map when found
        Unique      = 1 << 6,   // object is unique
        Enabled     = 1 << 7,   // object is enabled
        Recruitment = 1 << 8,   // object can help with recruitment
        RandomStart = 1 << 9,   // object starts randomly on map
        Special     = 1 << 10,  // object is special (Extra code processing)
        
        // Entity
        None                = EntityFlags.None,
        Disabled            = EntityFlags.Disabled
    }
}
