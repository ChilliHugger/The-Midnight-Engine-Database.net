using System;

namespace TME.Scenario.Default.Flags
{
    [Flags]
    public enum LocationInfoFlags : uint
    {
        None            = 0,
        MoveForward     = 1 << 0,   // can we move into the location we are looking at?
        Seek            = 1 << 1,   // can we do a search - always true at present
        Hide            = 1 << 2,   // can the character hide here?
        Fight           = 1 << 3,   // is there anything to fight, and can the <character> do it?
        RecruitLord     = 1 << 4,   // is there a lord to fight?
        RecruitMen      = 1 << 5,   // are there any soldiers to recruit, and can the <character> do it
        GuardMen        = 1 << 6,   // could the <character> place and men on guard
        EnterBattle     = 1 << 7,   // can we enter into battle?
        EnterTunnel     = 1 << 8,   // can we enter tunnel
        ExitTunnel      = 1 << 9,   // can we exit tunnel
        Rest            = 1 << 10,  // can we rest
        Give            = 1 << 11,  // can we give
        Take            = 1 << 12,  // can we take
        Use             = 1 << 13,  // can we use
        Blocked         = 1 << 14,  // way forward is blocked
    }
}
