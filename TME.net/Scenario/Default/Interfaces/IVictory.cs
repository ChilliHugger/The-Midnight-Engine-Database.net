using TME.Scenario.Default.Flags;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface IVictory : IEntity
    {
        new VictoryFlags Flags { get; } 
        int Priority { get; }
        uint String { get; }
        IMission? Mission { get; }
        bool IsComplete { get; }
        bool IsGameOver { get; }
    }
}