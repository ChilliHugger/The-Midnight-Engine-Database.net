using System.Collections.Generic;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
    public interface IMission : IEntity
    {
        int Priority { get; }
        MissionObjective Objective { get; }
        MissionCondition Condition { get; } 
        IList<IEntity> References { get; }
        int Points { get; }
        IEntity? Scorer { get; } 
        MissionAction Action { get; }
        IEntity? ActionId { get; }
        bool IsComplete { get; }
        bool IsAny{ get; }
    }
}