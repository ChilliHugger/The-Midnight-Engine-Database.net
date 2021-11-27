using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IObjectInternal : IObject, IItemInternal
    {
        void UpdateCarriedBy(IItem? carriedBy);
    }
    
    public interface IObject : IItem
    {
        ThingType Kills { get; }
        string Name { get; }
        string Description { get; }
        uint UseDescription { get; }
        IItem? CarriedBy { get; }
        bool IsUnique { get; }
        bool IsCarried { get; }
    }
}
