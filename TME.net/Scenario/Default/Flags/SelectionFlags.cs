using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum SelectionFlags : UInt32
    {
        None        = 1 << 0,
        All         = 1 << 1,
        Tunnel      = 1 << 2,
    }
}
