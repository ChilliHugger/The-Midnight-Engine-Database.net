using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface IRegiment : IArmy, IEntity
    {
        MXId TargetId { get; }
        Orders Orders { get; }
        uint Delay { get; }
        uint Lost { get; }
        ILord? LoyaltyLord { get; }
        Loc LastLocation { get; }
        // night processing and thus not required for storage
        uint Turns { get; }
        Loc TargetLocation { get; }
    }
}
