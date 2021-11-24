using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum MissionFlags : uint
    {
        Complete = 1 << 0, // Mission Completed
        Enabled  = 1 << 1, // Mission Enabled
        Any      = 1 << 2, // Any Reference will do!
    }
}