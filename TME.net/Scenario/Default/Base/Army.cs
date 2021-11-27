using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Base
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Army : IArmy
    {
        public IItem? Parent { get; internal set; }
        public ArmyType ArmyType { get; internal set; } = ArmyType.None;
        public UnitType UnitType { get; internal set; } = UnitType.None;
        public Race Race { get; internal set; } = Race.None;
        public uint Total { get; internal set; }
        public uint Success { get; internal set; }
        public uint Killed { get; internal set; }
        public Race LoyaltyRace { get; internal set; } = Race.None;
        public bool Friendly { get; internal set; }
    }
}