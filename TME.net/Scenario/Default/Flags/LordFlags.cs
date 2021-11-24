namespace TME.Scenario.Default.Flags
{
    public enum LordFlags : uint
    {
        Hidden              = 1 << 0,   // currently hiding
        Riding              = 1 << 1,   // currently riding
        Alive               = 1 << 2,   // alive
        Recruited           = 1 << 3,   // currently recruited
        Army                = 1 << 4,   // allowed an army
        Hide                = 1 << 5,   // allowed to hide
        Horse               = 1 << 6,   // allowed a horse
        Moonring            = 1 << 7,   // allowed the moonring

        // these should be user defined for the scenario
        // lom
        IceCrown            = 1 << 8,   // allowed the icecrown
        CanDestroyIceCrown  = 1 << 9,   // can destroy the ice crown

        // ddr
        AllowedWarriors     = 1 << 10,  // can have warriors
        AllowedRiders       = 1 << 11,  // can have riders
                                     
        AI                  = 1 << 12,  // character is an AI character
        KilledFoe           = 1 << 13,  // character killed his foe

        // TODO: Resolve these
        // Do we need to be backward compatible?
        Resting             = 1 << 16,  // currently resting
        InBattle            = 1 << 17,  // currently in battle
        WonBattle           = 1 << 18,  // just won a battle
        Tunnel              = 1 << 19,  // currently in a tunnel (DDR)
        UsedObject          = 1 << 20,  // has used his special object (DDR)
        HasFollowers        = 1 << 21,  // has followers
        PreparesBattle      = 1 << 22,  // prepares to do battle
        Approaching         = 1 << 23,  // we are approaching a lord (DDR)
    }
}
