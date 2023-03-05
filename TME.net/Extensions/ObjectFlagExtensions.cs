using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Extensions
{
    public static class ObjectFlagExtensions
    {
        public static bool CanDrop(this IObject c) => c.IsFlags(ObjectFlags.Drop);
        public static bool CanFight(this IObject c) => c.IsFlags(ObjectFlags.Fight);
        public static bool CanPickup(this IObject c) => c.IsFlags(ObjectFlags.Pickup);
        public static bool CanRemove(this IObject c) => c.IsFlags(ObjectFlags.Remove);
        public static bool CanSee(this IObject c) => c.IsFlags(ObjectFlags.See);
        public static bool HelpsRecruitment(this IObject c) => c.IsFlags(ObjectFlags.Recruitment);
        public static bool IsCarried(this IObject c) => c.CarriedBy != null;
        public static bool IsRandomStart(this IObject c) => c.IsFlags(ObjectFlags.RandomStart);
        public static bool IsSpecial(this IObject c) => c.IsFlags(ObjectFlags.Special);
        public static bool IsUnique(this IObject c) => c.IsFlags(ObjectFlags.Unique);
        public static bool IsWeapon(this IObject c) => c.IsFlags(ObjectFlags.Weapon);
    }
}