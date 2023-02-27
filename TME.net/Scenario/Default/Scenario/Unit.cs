using System;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public class Unit : IUnitInternal
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
        
        #region Internal Helpers
        void IUnitInternal.AddLoses(uint value)
        {
            Lost += value;
            Total = Math.Max(Total - value, 0);
        }

        void IUnitInternal.SetKilled(uint value)
        {
            Killed = value;
        }

        void IUnitInternal.SetLost(uint value)
        {
            Lost = value;
        }

        void IUnitInternal.SetTotal(uint value)
        {
            Total = value;
        }
        #endregion
        
        #region Serialize
        public bool Load(ISerializeContext ctx)
        {
            Total = ctx.Reader.UInt32();
            Energy = ctx.Reader.UInt32();
            Lost = ctx.Reader.UInt32();
            Killed = ctx.Reader.UInt32();
            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
