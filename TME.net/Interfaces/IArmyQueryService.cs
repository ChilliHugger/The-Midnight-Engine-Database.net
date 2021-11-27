using System;
using System.Collections.Generic;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IArmyQueryService
    {
        IEnumerable<IArmy> GetLordWarriorsAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<IArmy> GetLordRidersAsArmies(IEnumerable<ICharacter> lords, Func<ICharacter, bool> isFriendlyTo);
        IEnumerable<IArmy> GetRegimentsAsArmies(IEnumerable<IRegiment> regiments, Func<ICharacter?, bool> isFriendlyTo);
        IEnumerable<IArmy> GetStrongholdArmy(IEnumerable<IStronghold> stronghold, Func<IStronghold, bool> isFriendlyTo);
    }
}