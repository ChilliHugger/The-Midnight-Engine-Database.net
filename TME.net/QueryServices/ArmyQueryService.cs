using System;
using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.QueryServices
{
    public class ArmyQueryService : IArmyQueryService
    {
        private int CountLordUnits(ICharacter character)
        {
            return (character.Units[0].Total > 0 ? 1 : 0) +
                   (character.Units[1].Total > 0 ? 1 : 0);
        }
        
        public uint CountLordArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo ) =>
            (uint)lords.Where(isFriendlyTo).Sum(CountLordUnits);

        public uint CountRegimentsArmies(
            IEnumerable<IRegiment> regiments, Func<IRegiment,bool> isFriendlyTo ) =>
            (uint)regiments.Where(r=>r.Total > 0).Count(isFriendlyTo);
        
        public uint CountStrongholdArmies(
            IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo) =>
            (uint)strongholds.Where(s=>s.Total > 0).Count(isFriendlyTo);
        
        public IEnumerable<Army> GetLordWarriorsAsArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo )
        {
            return lords
                .Where( l=> l.Units[0].Total > 0)
                .Select( l => new Army
            {
                Parent = l,
                ArmyType = ArmyType.Character,
                Race = l.Race,
                UnitType = UnitType.Warrior,
                Total = l.Units[0].Total,
                Success = 0, // warriors->battleSuccess
                Killed = 0,
                LoyaltyRace = l.Loyalty,
                Friendly = isFriendlyTo(l)
            });
        }
        
        public IEnumerable<Army> GetLordRidersAsArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo )
        {
            return lords
                .Where( l=> l.Units[1].Total > 0)
                .Select( l => new Army
            {
                Parent = l,
                ArmyType = ArmyType.Character,
                Race = l.Race,
                UnitType = UnitType.Rider,
                Total = l.Units[1].Total,
                Success = 0, // riders->battleSuccess
                Killed = 0,
                LoyaltyRace = l.Loyalty,
                Friendly = isFriendlyTo(l)
            });
        }
        
        public IEnumerable<Army> GetRegimentsAsArmies(
            IEnumerable<IRegiment> regiments, Func<IRegiment,bool> isFriendlyTo )
        {
            return regiments
                .Where(r=>r.Total>0)
                .Select( r => new Army
            {
                Parent = r,
                ArmyType = ArmyType.Regiment,
                Race = r.Race,
                UnitType = r.UnitType,
                Total = r.Total,
                Success = 0, // regiment->battleSuccess
                Killed = 0,
                LoyaltyRace = r.LoyaltyRace,
                Friendly = isFriendlyTo(r)
            });
        }
        
        public IEnumerable<Army> GetStrongholdArmy(IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo)
        {
            return strongholds
                .Where(r=>r.Total>0)
                .Select( s => new Army
            {
                Parent = s,
                ArmyType = ArmyType.Stronghold,
                Race = s.OccupyingRace,
                UnitType = s.UnitType,
                Total = s.Total,
                Success = 0, // stronghold->battleSuccess
                Killed = 0,
                LoyaltyRace = s.LoyaltyRace,
                Friendly = isFriendlyTo(s)
            });
        }

    }
}