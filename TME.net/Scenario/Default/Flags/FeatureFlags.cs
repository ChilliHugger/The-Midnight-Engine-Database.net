using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum FeatureFlags : uint
    {
        None            = 0,
        MoonRing        = 1 << 0,
        IceFear         = 1 << 1,
        Approach        = 1 << 2, // Use Approach for recruitment
        Recruit         = 1 << 3, 
        RecruitTime     = 1 << 4, // recruitment takes time (overnight)
        Tunnels         = 1 << 5,
        Mist            = 1 << 6,
        Take            = 1 << 7,
        Give            = 1 << 8
    }
}