using System;
using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IArmyQueryService
    {
        uint CountLordArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        uint CountRegimentsArmies(IEnumerable<IRegiment> regiments, Func<IRegiment, bool> isFriendlyTo);
        uint CountStrongholdArmies(IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo);
        
        IEnumerable<Army> GetLordWarriorsAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<Army> GetLordRidersAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<Army> GetRegimentsAsArmies(IEnumerable<IRegiment> regiments, Func<IRegiment, bool> isFriendlyTo);
        IEnumerable<Army> GetStrongholdArmy(IEnumerable<IStronghold> stronghold, Func<IStronghold, bool> isFriendlyTo);
    }
}