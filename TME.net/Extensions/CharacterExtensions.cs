using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Extensions
{
    public static class CharacterExtensions
    {
        public static bool IsFriendlyTo( this ICharacter c, ICharacter lord ) => c.Loyalty == lord.Loyalty;
        public static bool IsOnSameSide ( this ICharacter c, ICharacter lord ) => c.CommanderInChief() == lord.CommanderInChief();
        public static bool HasTrait(this ICharacter c, LordTraits mask) => (c.Traits.Raw() & mask.Raw()) != 0;
        public static ICharacter? CommanderInChief(this ICharacter c) => c.Liege?.CommanderInChief();
    }
}