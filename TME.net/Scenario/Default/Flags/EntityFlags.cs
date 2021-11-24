using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum EntityFlags : uint
    {
        Disabled = (uint)1 << 31
    }
}
