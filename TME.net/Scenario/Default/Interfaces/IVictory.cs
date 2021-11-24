using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface IVictory : IEntity
    {
        int Priority { get; }
        uint String { get; }
        MXId Mission { get; }
        bool IsComplete { get; }
        bool IsGameOver { get; }
    }
}