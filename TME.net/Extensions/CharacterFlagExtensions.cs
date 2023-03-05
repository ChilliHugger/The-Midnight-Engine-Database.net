using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Extensions
{
    public static class CharacterFlagExtensions
    {
        public static bool CanDestroyIceCrown(this ICharacter c) => c.IsFlags(LordFlags.CanDestroyIceCrown);
        public static bool HasArmy(this ICharacter c) => c.Warriors + c.Riders > 0;
        public static bool HasFollowers(this ICharacter c) => c.IsFlags(LordFlags.HasFollowers);
        public static bool HasUsedObject(this ICharacter c) => c.IsFlags(LordFlags.UsedObject);
        public static bool HasWonBattle(this ICharacter c) => c.IsFlags(LordFlags.WonBattle);
        public static bool IsAiControlled(this ICharacter c) => c.IsFlags(LordFlags.AI);
        public static bool IsAlive(this ICharacter c) => c.IsFlags(LordFlags.Alive);
        public static bool IsAllowedArmy(this ICharacter c) => c.IsFlags(LordFlags.Army);
        public static bool IsAllowedHide(this ICharacter c) => c.IsFlags(LordFlags.Hide);
        public static bool IsAllowedHorse(this ICharacter c) => c.IsFlags(LordFlags.Horse);
        public static bool IsAllowedIceCrown(this ICharacter c) => c.IsFlags(LordFlags.IceCrown);
        public static bool IsAllowedMoonRing(this ICharacter c) => c.IsFlags(LordFlags.Moonring);
        public static bool IsAllowedRiders(this ICharacter c) => c.IsFlags(LordFlags.AllowedRiders);
        public static bool IsAllowedWarriors(this ICharacter c) => c.IsFlags(LordFlags.AllowedWarriors);
        public static bool IsApproaching(this ICharacter c) => c.IsFlags(LordFlags.Approaching);
        public static bool IsCoward(this ICharacter c) => c.Courage == 0 || c.HasTrait(LordTraits.Coward);
        public static bool IsDead(this ICharacter c) => !c.IsAlive();
        public static bool IsFollowing(this ICharacter c) => c.Following != null;
        public static bool IsHidden(this ICharacter c) => c.IsFlags(LordFlags.Hidden);
        public static bool IsInBattle(this ICharacter c) => c.IsFlags(LordFlags.InBattle);
        public static bool IsInTunnel(this ICharacter c) => c.IsFlags(LordFlags.Tunnel);
        public static bool IsPreparingForBattle(this ICharacter c) => c.IsFlags(LordFlags.PreparesBattle);
        public static bool IsRecruited(this ICharacter c) => c.IsFlags(LordFlags.Recruited);
        public static bool IsResting(this ICharacter c) => c.IsFlags(LordFlags.Resting);
        public static bool IsRiding(this ICharacter c) => c.IsFlags(LordFlags.Riding);
        public static bool KilledFoe(this ICharacter c) => c.IsFlags(LordFlags.KilledFoe);
    }
}