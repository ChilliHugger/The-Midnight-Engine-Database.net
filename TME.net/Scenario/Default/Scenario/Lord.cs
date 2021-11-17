using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac.Features.AttributeFilters;
using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Scenario
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Lord : Item, ILord
    {
    #region "DI"
        public IBattleInfo BattleInfo { get; private set; }
        public IRecruitment Recruitment { get; private set; }
    #endregion
        
        public Direction Looking { get; internal set; } = Direction.None;
        public Time Time { get; internal set; } = Time.None;
        public Race Race { get; private set; } = Race.None;
        public Gender Gender { get; private set; } = Gender.None;
        public string LongName { get; private set; } = "";
        public string ShortName { get; private set; } = "";
        public IList<IThing> Carrying { get; internal set; }
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

        public Lord(
            IBattleInfo battleInfo,
            IRecruitment recruitment,
            [KeyFilter(UnitType.Warrior)] IUnit warriors,
            [KeyFilter(UnitType.Rider)] IUnit riders)
        {
            BattleInfo = battleInfo;
            Recruitment = recruitment;
            Units = new List<IUnit>() {warriors,riders};
            Carrying = new List<IThing>();
            Type = EntityType.Character;
        }

        #region Serialize
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            
            LongName = ctx.Reader.ReadString();
            ShortName = ctx.Reader.ReadString();
            Looking = ctx.Reader.ReadDirection();
            Time = ctx.Reader.ReadTime();

            Units.FirstOrDefault()?.Load(ctx);
            Units.LastOrDefault()?.Load(ctx);

            BattleInfo.Load(ctx);

            Reckless = ctx.Reader.ReadUInt32();
            Energy = ctx.Reader.ReadUInt32();
            Strength = ctx.Reader.ReadUInt32();
            Cowardly = ctx.Reader.ReadUInt32();
            Courage = ctx.Reader.ReadUInt32();
            Fear = ctx.Reader.ReadUInt32();

            Recruitment.Load(ctx);

            Race = ctx.Reader.ReadRace();

            var carried = ctx.ReadEntity<IThing>();
            Carrying = carried == null
                ? new List<IThing>()
                : new List<IThing> {carried};
                
            KilledBy = ctx.ReadEntity<IThing>();

            Gender = ctx.Reader.ReadGender();

            Loyalty = ctx.Reader.ReadRace();

            Liege = ctx.ReadEntity<ILord>();

            Foe = ctx.ReadEntity<ILord>();

            WaitStatus = ctx.Reader.ReadWaitStatus();

            Orders = ctx.Reader.ReadOrders();

            Despondency = ctx.Reader.ReadUInt32();

            Traits = ctx.Reader.ReadUInt32();

            Following = ctx.Version > 2
                ? ctx.ReadEntity<ILord>()
                : null;

            Followers = ctx.Version > 4
                ? ctx.Reader.ReadUInt32()
                : 0;

            // temp bug fix
            if ((int)Energy < 0)
                Energy = 0;

            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
