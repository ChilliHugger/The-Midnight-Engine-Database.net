using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Base
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class ArmyTotals : IArmyTotals
    {
        public IDictionary<UnitType, uint> Units { get; internal set; } = new Dictionary<UnitType, uint>();
        public uint Armies { get;  internal set; }
        public uint Adjustment { get;  internal set; }
        public ILord Lord { get; internal set; } = null!;
    }
}