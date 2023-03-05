using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeLord : ICharacter
    {
        UnitType ArmyType { get; }
        IUnit? Unit { get; }
        uint ArmySize { get; }
    }
}