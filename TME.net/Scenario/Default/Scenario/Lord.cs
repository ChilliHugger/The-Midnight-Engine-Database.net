using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Scenario
{
    public class Lord : Item, ILord
    {
    #region "DI"
        public IBattleInfo BattleInfo { get; protected set; }
        public IRecruitment Recruitment { get; protected set; }
    #endregion
        
        public Direction Looking { get; private set; } = Direction.None;
        public Time Time { get; set; } = Time.None;
        public Race Race { get; private set; } = Race.None;
        public Gender Gender { get; private set; } = Gender.None;
        public string LongName { get; private set; } = "";
        public string ShortName { get; private set; } = "";
        public IList<IThing> Carrying { get; private set; }
        public IThing? KilledBy { get; private set; }
        public WaitStatus WaitStatus { get; private set; } = WaitStatus.None;
        public MXId LastCommandId { get; protected set; } = MXId.None;
        public Command LastCommand { get; protected set; } = Command.None;
        public List<IUnit> Units { get; protected set; }
        public ILord? Following { get; private set; }
        public uint Energy { get; private set; } 
        public uint Reckless { get; private set; } 
        public uint Followers { get; private set; }
        public uint Strength { get; private set; }
        public uint Cowardly { get; private set; }
        public uint Courage { get; private set; }
        public uint Fear { get; private set; }
        public Orders Orders { get; private set; } = Orders.None;
        public Race Loyalty { get; private set; } = Race.None;
        public ILord? Foe { get; private set; }
        public ILord? Liege { get; private set; }
        public uint Despondency { get; private set; }
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
