using System;
using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Default.Interfaces
{
    public interface ILocationInfo
    {
        ILord Owner { get; }                        // the <character> from whom this info block was created for
        SelectionFlags SelectionFlags { get; }      // selection flags

        Direction Looking { get; }                  // for what direction? If DR_NONE then the next two vars are not valid
        Loc LocationLookingAt { get; }              // where is the item of interest in the distance?
        Loc LocationInfront { get; }                // location directly infront

        ILocationInfo Ahead { get; }                // info block for the location in front

        MapLoc MapLoc { get; }                      // a copy of the location we are on

        // <character> conditionals
        // these are only valid when the <character> != NULL
        LocationInfoFlags LocationFlags { get; }

        // character info
        IThing FightThing { get; }                  // what object is available for fighting

        // scenario specific adjusters
        int FearAdjuster { get; }                   // general fear adjuster
        int MoralAdjuster { get; }                  // general moral adjuster
        int StrongholdAdjuster { get; }             // general stronghold adjuster
        uint Recruited { get; }                     // total number of characters in this location already recruited

        IArmyTotals Friends { get; }                // info block for friend. ie: Currently NOT doomdark
        IArmyTotals Foe { get; }                    // info block for foes ie: Currently IS doomdark
        IArmyTotals Doomdark { get; }               // info block for doomdark's regiments, does not include strongolds

        IEnumerable<IArmy> Armies{ get; }           // list of armies here
        //uint Armies { get; }                      // total armies in this location

        List<IStronghold> Strongholds { get; }      // list of strongholds here

        List<IStronghold> Routenodes { get; }       // list of routenodes here
        List<IStronghold> Lords { get; }            // list of characters here
        List<IStronghold> Regiments { get; }        // list of regiments here
        List<IStronghold> RecruitedLords { get; }   // list of characters that <character> can recruit

        ILord StubbornFollowerMove { get; }         // which character is stopping us moving
        ILord StubbornFollowerBattle { get; }       // which character is stopping us attacking

        IThing ObjectToTake { get; }
        ILord SomeoneToGiveTo { get; }

    }
}
