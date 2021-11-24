namespace TME.Scenario.Default.Flags
{
    public enum LordTraits : uint
    {
// +ve
        Good             = 1 << 0,
        Strong           = 1 << (1),
        Forceful         = 1 << (2),
        Generous         = 1 << (3),
        Stubborn         = 1 << (4),
        Brave            = 1 << (5),
        Swift            = 1 << (6),
        Loyal            = 1 << (7),
// -ve
        Evil             = 1 << (8),
        Weak             = 1 << (9),
        Reticent         = 1 << (10),
        Greedy           = 1 << (11),
        Fawning          = 1 << (12),
        Coward           = 1 << (13),
        Slow             = 1 << (14),
        Treacherous      = 1 << (15),
    }
}
