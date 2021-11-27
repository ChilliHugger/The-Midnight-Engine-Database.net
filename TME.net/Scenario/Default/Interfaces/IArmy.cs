using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IArmyInternal
    {
    }
    
    public interface IArmy
    {
        IItem? Parent { get; }
        ArmyType ArmyType { get; }
        UnitType UnitType { get; }
        Race Race { get; }
        uint Total { get; }
        uint Success { get; }
        uint Killed { get; }
        Race LoyaltyRace { get; }
        bool Friendly { get; }
    }
}
