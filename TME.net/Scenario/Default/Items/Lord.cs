using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac.Features.AttributeFilters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Lord : Item, ILordInternal
    {

        #region "DI"

        public IBattleInfo BattleInfo { get; internal set; }
        public IRecruitment Recruitment { get; private set; }

        #endregion

        public Direction Looking { get; internal set; } = Direction.None;
        public Time Time { get; private set; } = Time.None;
        public Race Race { get; private set; } = Race.None;
        public Gender Gender { get; private set; } = Gender.None;
        public string LongName { get; private set; } = "";
        public string ShortName { get; private set; } = "";
        public IReadOnlyList<IThing> Carrying { get; internal set; }
        public IThing? KilledBy { get; internal set; }
        public WaitStatus WaitStatus { get; internal set; } = WaitStatus.None;
        public MXId LastCommandId { get; internal set; } = MXId.None;
        public Command LastCommand { get; internal set; } = Command.None;
        public List<IUnit> Units { get; private set; }
        public ILord? Following { get; internal set; }
        public uint Energy { get; internal set; }
        public uint Reckless { get; internal set; }
        public uint Followers { get; internal set; }
        public uint Strength { get; internal set; }
        public uint Cowardly { get; internal set; }
        public uint Courage { get; internal set; }
        public uint Fear { get; internal set; }
        public Orders Orders { get; internal set; } = Orders.None;
        public Race Loyalty { get; internal set; } = Race.None;
        public ILord? Foe { get; internal set; }
        public ILord? Liege { get; internal set; }
        public uint Despondency { get; internal set; }
        public uint Traits { get; private set; }

        public bool IsAlive => IsFlags(LordFlags.Alive);
        public bool IsHidden => IsFlags(LordFlags.Hidden);
        public bool IsRecruited => IsFlags(LordFlags.Recruited);
        public bool IsInTunnel => IsFlags(LordFlags.Tunnel);
        
        public bool IsFriendlyTo( ILord lord ) => Loyalty == lord.Loyalty;

        public bool IsOnSameSide ( ILord lord ) 
        {
            return CommanderInChief == lord.CommanderInChief ;
        }

        public ILord? CommanderInChief => Liege?.CommanderInChief;
        
        public Lord(
            IBattleInfo battleInfo,
            IRecruitment recruitment,
            [KeyFilter(UnitType.Warrior)] IUnit warriors,
            [KeyFilter(UnitType.Rider)] IUnit riders) : base(EntityType.Character)
        {
            BattleInfo = battleInfo;
            Recruitment = recruitment;
            Units = new List<IUnit>() {warriors, riders};
            Carrying = new List<IThing>();
        }

        #region Internal Helpers
        void ILordInternal.UpdateTime(Time time)
        {
            Time = time;
        }

        void ILordInternal.RemoveCarriedObject(IThing thing)
        {
            var oldObjects = new List<IThing>(Carrying);
            oldObjects.Remove(thing);
            Carrying = oldObjects.AsReadOnly();
        }

        void ILordInternal.SetCarrying(IEnumerable<IThing> carried)
        {
            Carrying = carried.ToList().AsReadOnly();
        }
        #endregion
    }


}
