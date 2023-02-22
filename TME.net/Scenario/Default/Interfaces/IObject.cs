using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IObjectInternal : IObject, IItemInternal
    {
        void UpdateCarriedBy(IItem? carriedBy);
    }
    
    public interface IObject : IItem
    {
        new ThingFlags Flags { get; }
        ThingType Kills { get; }
        string Name { get; }
        string Description { get; }
        uint UseDescription { get; }
        IItem? CarriedBy { get; }

        #region Flags
        bool CanDrop { get; }
        bool CanFight { get; }
        bool CanPickup { get; }
        bool CanRemove { get; }
        bool CanSee { get; }
        bool HelpsRecruitment { get; }
        bool IsCarried { get; }
        bool IsRandomStart { get; }
        bool IsSpecial { get; }
        bool IsUnique { get; }
        bool IsWeapon { get; }
        #endregion
    }
}
