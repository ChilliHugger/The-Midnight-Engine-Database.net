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
        public uint CountLordArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo ) =>
            (uint)lords.Count(isFriendlyTo);

        public uint CountRegimentsArmies(
            IEnumerable<IRegiment> regiments, Func<IRegiment,bool> isFriendlyTo ) =>
            (uint)regiments.Count(isFriendlyTo);
        
        public uint CountStrongholdArmies(
            IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo) =>
            (uint)strongholds.Count(isFriendlyTo);
        
        public IEnumerable<IArmy> GetLordWarriorsAsArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo )
        {
            return lords.Select( l => new Army
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
        
        public IEnumerable<IArmy> GetLordRidersAsArmies(
            IEnumerable<ICharacter> lords, Func<ICharacter,bool> isFriendlyTo )
        {
            return lords.Select( l => new Army
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
        
        public IEnumerable<IArmy> GetRegimentsAsArmies(
            IEnumerable<IRegiment> regiments, Func<ICharacter?,bool> isFriendlyTo )
        {
            return regiments.Select( r => new Army
            {
                Parent = r,
                ArmyType = ArmyType.Regiment,
                Race = r.Race,
                UnitType = r.UnitType,
                Total = r.Total,
                Success = 0, // regiment->battleSuccess
                Killed = 0,
                LoyaltyRace = r.LoyaltyRace,
                Friendly = isFriendlyTo(r.LoyaltyLord)
            });
        }
        
        public IEnumerable<IArmy> GetStrongholdArmy(IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo)
        {
            return strongholds.Select( s => new Army
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