using System;
using TME.Default.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class Unit : IUnit
    {
        public Unit(UnitType type)
        {
            Type = type;
        }

        public UnitType Type { get; protected set; }

        public uint Total { get; protected set; }

        public uint Energy { get; protected set; }

        public uint Lost { get; protected set; }

        public uint Slew { get; protected set; }

        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Total = ctx.Reader.ReadUInt32();
            Energy = ctx.Reader.ReadUInt32();
            Lost = ctx.Reader.ReadUInt32();
            Slew = ctx.Reader.ReadUInt32();

            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
