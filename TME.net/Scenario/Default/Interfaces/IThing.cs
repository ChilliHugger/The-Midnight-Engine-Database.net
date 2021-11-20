using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IThingInternal : IThing, IItemInternal
    {
        void UpdateCarriedBy(IItem? carriedBy);
    }
    
    public interface IThing : IItem
    {
        ThingType Kills { get; }
        string Name { get; }
        string Description { get; }
        uint UseDescription { get; }
        IItem? CarriedBy { get; }
        bool IsUnique { get; }
    }
}
