using System.Collections.Generic;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface ICharacterInternal : ICharacter
    {
        void UpdateTime(Time time);
        void RemoveCarriedObject(IObject thing);
        void SetCarrying(IEnumerable<IObject> carried);
    }
    
    public interface ICharacter : IItem
    {
        Direction Looking { get; }
        Time Time { get; }
        Race Race { get; }
        Gender Gender { get; }

        string LongName { get; }
        string ShortName { get; }
        uint Energy { get; }
        uint Reckless { get; }

        BattleInfo BattleInfo { get; }
        Recruitment Recruitment { get; }

        IReadOnlyList<IObject> Carrying { get; }
        IObject? KilledBy { get; }

        WaitStatus WaitStatus { get; }

        MXId LastCommandId { get; }
        Command LastCommand { get; }

        List<IUnit> Units { get; }

        ICharacter? Following { get; }
        uint Followers { get; }

        // lom
        uint Strength { get; }
        uint Cowardly { get; }
        uint Courage { get; }
        uint Fear { get; }

        // These are only initially used for DDR but are
        // supported in default characters
        Orders Orders { get; }
        Race Loyalty { get; }
        ICharacter? Foe { get; }
        ICharacter? Liege { get; }
        uint Despondency { get; }
        LordTraits Traits { get; }
        bool HasTrait(LordTraits mask);

        bool IsFriendlyTo(ICharacter lord);
        bool IsOnSameSide(ICharacter lord);
        ICharacter? CommanderInChief { get; }
        
        #region Flags Helpers
        bool CanDestroyIceCrown { get; }
        bool HasArmy { get; }
        bool HasFollowers { get; }
        bool HasUsedObject { get; }
        bool HasWonBattle { get; }
        bool IsAiControlled { get; }
        bool IsAlive { get; }
        bool IsAllowedArmy { get; }
        bool IsAllowedHide { get; }
        bool IsAllowedHorse { get; }
        bool IsAllowedIceCrown { get; }
        bool IsAllowedMoonRing { get; }
        bool IsAllowedRiders { get; }
        bool IsAllowedWarriors { get; }
        bool IsApproaching { get; }
        bool IsCoward { get; }
        bool IsDead { get; }
        bool IsDawn { get; }
        bool IsFollowing { get; }
        bool IsHidden { get; }
        bool IsInBattle{ get; }
        bool IsInTunnel { get; }
        bool IsNight { get; }
        bool IsPreparingForBattle { get; }
        bool IsRecruited { get; }
        bool IsResting { get; }
        bool IsRiding { get; }
        bool KilledFoe { get; }
        #endregion
        
    }
}
