using System;
using TME.Scenario.ddr;
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

            OccupyingRace = ctx.Reader.Race();
            Race = ctx.Reader.Race();
            UnitType = ctx.Reader.UnitType();
            Total = ctx.Reader.UInt32();
            Min = ctx.Reader.UInt32();
            Max = ctx.Reader.UInt32();
            StrategicalSuccess = ctx.Reader.UInt32();
            OwnerSuccess = ctx.Reader.UInt32();
            EnemySuccess = ctx.Reader.UInt32();
            Influence = ctx.Reader.UInt32();
            Respawn = (uint) Math.Max(ctx.Reader.Int32(),0);
            Occupier = ctx.ReadEntity<ICharacter>();
            Owner = ctx.ReadEntity<ICharacter>();
            Terrain = ctx.Reader.Terrain();
            Killed = ctx.Reader.UInt32();
            Lost = 0;

            if (ctx.Scenario?.Info.Symbol == RevengeScenario.Tag || ctx.Version > 19)
            {
                Energy = ctx.Version > 9
                    ? ctx.Reader.UInt32()
                    : 180;
            }

            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.Enum(OccupyingRace);
            ctx.Writer.Enum(Race);
            ctx.Writer.Enum(UnitType);
            ctx.Writer.UInt32(Total);
            ctx.Writer.UInt32(Min);
            ctx.Writer.UInt32(Max);
            ctx.Writer.UInt32(StrategicalSuccess);
            ctx.Writer.UInt32(OwnerSuccess);
            ctx.Writer.UInt32(EnemySuccess);
            ctx.Writer.UInt32(Influence);
            ctx.Writer.UInt32(Respawn);
            ctx.WriteEntity(Occupier);
            ctx.WriteEntity(Owner);
            ctx.Writer.Enum(Terrain);
            ctx.Writer.UInt32(Killed);
            
            // Revenge / Version 20
            ctx.Writer.UInt32(Energy);
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;
            
            OccupyingRace = bundle.Race(nameof(OccupyingRace));
            Race = bundle.Race(nameof(Race));
            UnitType = bundle.UnitType(nameof(Enums.UnitType));
            Total = bundle.UInt32(nameof(Total));
            Min = bundle.UInt32(nameof(Min));
            Max = bundle.UInt32(nameof(Max));
            StrategicalSuccess = bundle.UInt32(nameof(StrategicalSuccess));
            OwnerSuccess = bundle.UInt32(nameof(OwnerSuccess));
            EnemySuccess = bundle.UInt32(nameof(EnemySuccess));
            Influence = bundle.UInt32(nameof(Influence));
            Respawn = bundle.UInt32(nameof(Respawn));
            Occupier = bundle.Entity<ICharacter>(nameof(Occupier));
            Owner = bundle.Entity<ICharacter>(nameof(Owner));
            Terrain = bundle.Terrain(nameof(Terrain));
            Killed = 0;
            Lost = 0;
            Energy = bundle.UInt32(nameof(Energy));
            
            return true;
        }

        #endregion
    }
}