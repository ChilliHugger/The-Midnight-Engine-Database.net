using System;
using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Scenario
{
    public class Regiment : Item, IRegiment
    {
        public IItem? Parent { get; set; } 
        public ArmyType ArmyType { get; set; } = ArmyType.Regiment;
        public UnitType UnitType { get; set; } = UnitType.None;
        public Race Race { get; set; } = Race.None;
        public Race LoyaltyRace { get; set; } = Race.None;
        public MXId TargetId { get; set; } = MXId.None;
        public Orders Orders { get; set; } = Orders.None;
        public ILord? LoyaltyLord { get; set; }
        public uint Total { get; set; }
        public uint Success { get; set; }
        public uint Killed { get; set; }
        public uint Delay { get; set; }
        public uint Lost { get; set; }
        public uint Turns { get; set; }
        public Loc LastLocation { get; set; } = Loc.Zero;
        public Loc TargetLocation { get; set; } = Loc.Zero;

        public Regiment()
        {
        }
        
        #region Serialize

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;
            
            Race = ctx.Reader.ReadRace();
            UnitType = ctx.Reader.ReadUnitType();
            Total = ctx.Reader.ReadUInt32();
            TargetId = ctx.Reader.ReadMXId();
            Orders = ctx.Reader.ReadOrders();
            Success = ctx.Reader.ReadUInt32();
            LoyaltyLord = ctx.ReadEntity<ILord>();
            Killed = ctx.Reader.ReadUInt32();
            LastLocation = (ctx.Version > 3)
                ? ctx.Reader.ReadLoc()
                : Loc.Zero;
            Delay = (ctx.Version > 6)
                ? ctx.Reader.ReadUInt32()
                : 0;
            
            Lost = 0;
            TargetLocation = Loc.Zero;

            return true;
        }
        
        public override bool Save()
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}