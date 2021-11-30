using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac.Features.AttributeFilters;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Lord : Item, ICharacterInternal
    {
        private readonly IVariables _variables;

        #region "DI"

        public IBattleInfo BattleInfo { get; internal set; }
        public IRecruitment Recruitment { get; internal set; }

        #endregion

        public Direction Looking { get; internal set; } = Direction.None;
        public Time Time { get; internal set; } = Time.None;
        public Race Race { get; internal set; } = Race.None;
        public Gender Gender { get; internal set; } = Gender.None;
        public string LongName { get; internal set; } = "";
        public string ShortName { get; internal set; } = "";
        public IReadOnlyList<IObject> Carrying { get; internal set; }
        public IObject? KilledBy { get; internal set; }
        public WaitStatus WaitStatus { get; internal set; } = WaitStatus.None;
        public MXId LastCommandId { get; internal set; } = MXId.None;
        public Command LastCommand { get; internal set; } = Command.None;
        public List<IUnit> Units { get; internal set; }
        public ICharacter? Following { get; internal set; }
        public uint Energy { get; internal set; }
        public uint Reckless { get; internal set; }
        public uint Followers { get; internal set; }
        public uint Strength { get; internal set; }
        public uint Cowardly { get; internal set; }
        public uint Courage { get; internal set; }
        public uint Fear { get; internal set; }
        public Orders Orders { get; internal set; } = Orders.None;
        public Race Loyalty { get; internal set; } = Race.None;
        public ICharacter? Foe { get; internal set; }
        public ICharacter? Liege { get; internal set; }
        public uint Despondency { get; internal set; }
        public LordTraits Traits { get; internal set; }

        #region Flags Helpers
        public bool CanDestroyIceCrown => IsFlags(LordFlags.CanDestroyIceCrown);
        public bool HasArmy => Units[0].Total + Units[1].Total > 0;
        public bool HasFollowers => IsFlags(LordFlags.HasFollowers);
        public bool HasUsedObject => IsFlags(LordFlags.UsedObject);
        public bool HasWonBattle => IsFlags(LordFlags.WonBattle);
        public bool IsAiControlled => IsFlags(LordFlags.AI);
        public bool IsAlive => IsFlags(LordFlags.Alive);
        public bool IsAllowedArmy => IsFlags(LordFlags.Army);
        public bool IsAllowedHide => IsFlags(LordFlags.Hide);
        public bool IsAllowedHorse => IsFlags(LordFlags.Horse);
        public bool IsAllowedIceCrown => IsFlags(LordFlags.IceCrown);
        public bool IsAllowedMoonRing => IsFlags(LordFlags.Moonring);
        public bool IsAllowedRiders => IsFlags(LordFlags.AllowedRiders);
        public bool IsAllowedWarriors => IsFlags(LordFlags.AllowedWarriors);
        public bool IsApproaching => IsFlags(LordFlags.Approaching);
        public bool IsCoward => Courage == 0 || HasTrait(LordTraits.Coward);
        public bool IsDead => !IsAlive;
        public bool IsDawn => Time == _variables.sv_time_dawn && !IsResting;
        public bool IsFollowing => Following != null;
        public bool IsHidden => IsFlags(LordFlags.Hidden);
        public bool IsInBattle => IsFlags(LordFlags.InBattle);
        public bool IsInTunnel => IsFlags(LordFlags.Tunnel);
        public bool IsNight => Time == _variables.sv_time_night || IsResting;
        public bool IsPreparingForBattle => IsFlags(LordFlags.PreparesBattle);
        public bool IsRecruited => IsFlags(LordFlags.Recruited);
        public bool IsResting => IsFlags(LordFlags.Resting);
        public bool IsRiding => IsFlags(LordFlags.Riding);
        public bool KilledFoe => IsFlags(LordFlags.KilledFoe);
        #endregion
        
        public bool IsFriendlyTo( ICharacter lord ) => Loyalty == lord.Loyalty;
        public bool IsOnSameSide ( ICharacter lord ) => CommanderInChief == lord.CommanderInChief;
        public bool HasTrait(LordTraits mask) => (Traits.Raw() & mask.Raw()) != 0;
        public ICharacter? CommanderInChief => Liege?.CommanderInChief;
        
        public Lord(
            IVariables variables,
            IBattleInfo battleInfo,
            IRecruitment recruitment,
            [KeyFilter(UnitType.Warrior)] IUnit warriors,
            [KeyFilter(UnitType.Rider)] IUnit riders) : base(EntityType.Character)
        {
            _variables = variables;
            BattleInfo = battleInfo;
            Recruitment = recruitment;
            Units = new List<IUnit>() {warriors, riders};
            Carrying = new List<IObject>();
        }

        #region Internal Helpers
        void ICharacterInternal.UpdateTime(Time time)
        {
            Time = time;
        }

        void ICharacterInternal.RemoveCarriedObject(IObject thing)
        {
            var oldObjects = new List<IObject>(Carrying);
            oldObjects.Remove(thing);
            Carrying = oldObjects.AsReadOnly();
        }

        void ICharacterInternal.SetCarrying(IEnumerable<IObject> carried)
        {
            Carrying = carried.ToList().AsReadOnly();
        }
        #endregion
    }


}
