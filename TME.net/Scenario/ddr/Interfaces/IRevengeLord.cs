using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeLord : ICharacter
    {
        UnitType ArmyType { get; }
        IUnit? Unit { get; }
        uint ArmySize { get; }
    }
}