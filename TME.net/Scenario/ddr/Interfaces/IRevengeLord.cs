using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeLord : ILord
    {
        Loc LastLocation { get; }
        IRevengeThing? DesiredObject { get; }
        IRevengeStronghold? HomeStronghold { get; }
        IRevengeLord? FightingAgainst { get; }
        uint BattleLost { get; }
        MXId TargetId { get; }
        Loc TargetLocation { get; }
        
        UnitType ArmyType { get; }
        IUnit? Unit { get; }
        uint ArmySize { get; }
    }
}