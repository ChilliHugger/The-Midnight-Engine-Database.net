using System;
using System.Collections.Generic;
using TME.Scenario.Default.Interfaces;

namespace TME.Interfaces
{
    public interface IArmyQueryService
    {
        IEnumerable<IArmy> GetLordWarriorsAsArmies(IEnumerable<ILord> lords, Func<ILord, bool> isFriendlyTo);
        IEnumerable<IArmy> GetLordRidersAsArmies(IEnumerable<ILord> lords, Func<ILord, bool> isFriendlyTo);
        IEnumerable<IArmy> GetRegimentsAsArmies(IEnumerable<IRegiment> regiments, Func<ILord?, bool> isFriendlyTo);
        IEnumerable<IArmy> GetStrongholdArmy(IEnumerable<IStronghold> stronghold, Func<IStronghold, bool> isFriendlyTo);
    }
}