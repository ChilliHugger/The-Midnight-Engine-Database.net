using System;
using System.Collections.Generic;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IArmyQueryService
    {
        uint CountLordArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        uint CountRegimentsArmies(IEnumerable<IRegiment> regiments, Func<IRegiment, bool> isFriendlyTo);
        uint CountStrongholdArmies(IEnumerable<IStronghold> strongholds, Func<IStronghold, bool> isFriendlyTo);
        
        IEnumerable<IArmy> GetLordWarriorsAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<IArmy> GetLordRidersAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<IArmy> GetRegimentsAsArmies(IEnumerable<IRegiment> regiments, Func<ICharacter?, bool> isFriendlyTo);
        IEnumerable<IArmy> GetStrongholdArmy(IEnumerable<IStronghold> stronghold, Func<IStronghold, bool> isFriendlyTo);
    }
}