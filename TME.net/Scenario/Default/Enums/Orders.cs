namespace TME.Scenario.Default.Enums
{
    public enum Orders : uint
    {
        None = 0,
        Regiment,
        Goto = 1,
        Wander = 2,
        Follow = 3,
        Route = 4,
        DelayedWander = 5,
        Hold = 6,
        // character
        FollowLiege             =   7,  // 0
        FollowFoe               =   8,  // 1
        FindObject              =   9,  // 2
        Home                     =   10, // 3
    }
}
