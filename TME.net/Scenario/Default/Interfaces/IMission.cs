using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface IMission : IEntity
    {
        int Priority { get; }
        MissionObjective Objective { get; }
        MissionCondition Condition { get; } 
        MXId[] References { get; }
        int Points { get; }
        MXId Scorer { get; } 
        MissionAction Action { get; }
        MXId ActionId { get; set; }
        bool IsComplete { get; }
        bool IsAny{ get; }
    }
}