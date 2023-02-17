using System;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public partial class Stronghold
    {
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
            Respawn = (uint) Math.Max(ctx.Reader.ReadInt32(),0);
            Occupier = ctx.ReadEntity<ICharacter>();
            Owner = ctx.ReadEntity<ICharacter>();
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