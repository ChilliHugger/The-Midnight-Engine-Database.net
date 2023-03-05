using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.Default.Interfaces
{
    public interface IObject : IItem
    {
        new ObjectFlags Flags { get; }
        ThingType Kills { get; }
        string Name { get; }
        string Description { get; }
        uint UseDescription { get; }
        IItem? CarriedBy { get; }
        
        ObjectPower ObjectPower { get; }
        ObjectType ObjectType { get; }
    }
}
