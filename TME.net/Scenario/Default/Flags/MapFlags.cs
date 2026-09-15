using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum MapFlags : uint
    {
        None = 0,
        TunnelEndpoints = (uint)1 << 0,
        ImpassableLocations = (uint)2 << 0
    }
}