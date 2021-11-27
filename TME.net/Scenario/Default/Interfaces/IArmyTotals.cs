using System.Collections.Generic;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
    public interface IArmyTotals
    {
        IDictionary<UnitType, uint> Units { get; }
        uint Armies { get; }
        uint Adjustment { get; }
        ILord Lord { get; }
    }
}
