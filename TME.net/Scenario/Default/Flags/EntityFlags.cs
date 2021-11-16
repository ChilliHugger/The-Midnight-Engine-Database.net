using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum EntityFlags : UInt32
    {
        Disabled = (UInt32)1 << 31
    }
}
