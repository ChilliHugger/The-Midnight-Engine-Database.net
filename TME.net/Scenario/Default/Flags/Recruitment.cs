using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum RecruitGroup : uint
    {
        None    = 0,
        Free    = 1 << 0,   // 1
        Fey     = 1 << 1,   // 2
        Targ    = 1 << 2,   // 4
        HighFey = 1 << 3,   // 8
        Wise    = 1 << 4,   // 16
        Skulkrin = 1 << 5,  // 32
        Dragon  = 1 << 6,   // 64
    }
}