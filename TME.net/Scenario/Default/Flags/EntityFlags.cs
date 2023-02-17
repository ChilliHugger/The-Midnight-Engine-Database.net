using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum EntityFlags : uint
    {
        None = 0,
        Disabled = (uint)1 << 31
    }
}
