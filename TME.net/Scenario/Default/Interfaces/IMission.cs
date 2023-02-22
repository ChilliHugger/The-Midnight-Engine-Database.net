using System.Collections.Generic;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.Default.Interfaces
{
    public interface IMission : IEntity
    {
        new MissionFlags Flags { get; }
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