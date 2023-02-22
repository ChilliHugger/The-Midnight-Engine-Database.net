using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IRegimentInternal : IRegiment, IArmyInternal, IEntityInternal
    {
    }
    
    public interface IRegiment : IItem, IArmy
    {
        new RegimentFlags Flags { get; }
        IEntity? Target { get; }
        Orders Orders { get; }
        uint Delay { get; }
        uint Lost { get; }
        ICharacter? LoyaltyLord { get; }
        Loc LastLocation { get; }
        // night processing and thus not required for storage
        uint Turns { get; }
        Loc TargetLocation { get; }
        
        bool IsInTunnel { get; }
    }
}
