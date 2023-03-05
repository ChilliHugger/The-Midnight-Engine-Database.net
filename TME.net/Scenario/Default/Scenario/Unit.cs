using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class Unit : IUnit
    {

        #region Properties
        public UnitType Type { get; internal init; }

        public uint Total { get; internal set; }

        public uint Energy { get; internal set; }

        public uint Lost { get; internal set; }

        public uint Killed { get; internal set; }
        #endregion

        public Unit(UnitType type)
        {
            Type = type;
        }

        public Unit(UnitType type, uint total)
        {
            Type = type;
            Total = total;
        }
        
        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Total = ctx.Reader.UInt32();
            Energy = ctx.Reader.UInt32();
            Lost = ctx.Reader.UInt32();
            Killed = ctx.Reader.UInt32();
            return true;
        }

        public bool Save(ISerializeContext ctx)
        {
            ctx.Writer.UInt32(Total);
            ctx.Writer.UInt32(Energy);
            ctx.Writer.UInt32(Lost);
            ctx.Writer.UInt32(Killed);
            return true;
        }
        #endregion

    }
}
