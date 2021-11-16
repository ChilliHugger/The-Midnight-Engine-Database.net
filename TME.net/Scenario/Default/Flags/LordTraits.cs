namespace TME.Scenario.Default.Flags
{
    public enum LordTraits
    {
        ct_good = 1 << 0,
        ct_strong = 1 << (1),
        ct_forceful = 1 << (2),
        ct_generous = 1 << (3),
        ct_stubborn = 1 << (4),
        ct_brave = 1 << (5),
        ct_swift = 1 << (6),
        ct_loyal = 1 << (7),

        ct_evil = 1 << (8),
        ct_weak = 1 << (9),
        ct_reticent = 1 << (10),
        ct_greedy = 1 << (11),
        ct_fawning = 1 << (12),
        ct_coward = 1 << (13),
        ct_slow = 1 << (14),
        ct_treacherous = 1 << (15),
    }
}
