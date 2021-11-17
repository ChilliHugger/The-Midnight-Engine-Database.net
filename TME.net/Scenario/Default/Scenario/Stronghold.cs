using System;
using System.Diagnostics.CodeAnalysis;
using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Stronghold : Item, IStronghold
    {
        public Race OccupyingRace { get; internal set; } = Race.None;
        public Race Race { get; internal set; } = Race.None;
        public UnitType UnitType { get; internal set; } = UnitType.None;
        public uint Total { get; internal set; }
        public uint Min { get; private set; }
        public uint Max { get; private set; }
        public uint StrategicalSuccess { get; private set; }
        public uint OwnerSuccess { get; private set; }
        public uint EnemySuccess { get; private set; }
        public uint Influence { get; private set; }
        public uint Respawn { get; private set; }
        public ILord? Occupier { get; internal set; }
        public ILord? Owner { get; internal set; }
        public Terrain Terrain { get; internal set; } = Terrain.None;
        public uint Killed { get; internal set; }
        public uint Lost { get; internal set; }
        
        #region Serialize
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            OccupyingRace = ctx.Reader.ReadRace();
            Race = ctx.Reader.ReadRace();
            UnitType = ctx.Reader.ReadUnitType();
            Total = ctx.Reader.ReadUInt32();
            Min = ctx.Reader.ReadUInt32();
            Max = ctx.Reader.ReadUInt32();
            StrategicalSuccess = ctx.Reader.ReadUInt32();
            OwnerSuccess = ctx.Reader.ReadUInt32();
            EnemySuccess = ctx.Reader.ReadUInt32();
            Influence = ctx.Reader.ReadUInt32();
            Respawn = ctx.Reader.ReadUInt32();
            Occupier = ctx.ReadEntity<ILord>();
            Owner = ctx.ReadEntity<ILord>();
            Terrain = ctx.Reader.ReadTerrain();
            Killed = ctx.Reader.ReadUInt32();
            Lost = 0;
            
            return true;
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}