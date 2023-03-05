using System;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.ddr;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public partial class Character
    {
        #region Serialize

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            LongName = ctx.Reader.String();
            ShortName = ctx.Reader.String();
            Looking = ctx.Reader.Direction();
            Time = ctx.Reader.Time();

            Units.FirstOrDefault()?.Load(ctx);
            Units.LastOrDefault()?.Load(ctx);

            BattleInfo.Load(ctx);

            Reckless = ctx.Reader.UInt32();
            Energy = (uint) Math.Max(ctx.Reader.Int32(),0); // temp fix
            Strength = ctx.Reader.UInt32();
            Cowardly = ctx.Reader.UInt32();
            Courage = ctx.Reader.UInt32();
            Fear = ctx.Reader.UInt32();

            Recruitment.Load(ctx);

            Race = ctx.Reader.Race();

            var carried = ctx.ReadEntity<IObject>();
            Carrying = carried == null
                ? new List<IObject>().AsReadOnly()
                : new List<IObject> {carried}.AsReadOnly();
            
            KilledBy = ctx.ReadEntity<IObject>();

            Gender = ctx.Reader.Gender();

            Loyalty = ctx.Reader.Race();

            Liege = ctx.ReadEntity<ICharacter>();

            Foe = ctx.ReadEntity<ICharacter>();

            WaitStatus = ctx.Reader.WaitStatus();

            Orders = ctx.Reader.Orders();

            Despondency = ctx.Reader.UInt32();

            Traits = ctx.Reader.Enum<LordTraits>();

            Following = ctx.Version > 2
                ? ctx.ReadEntity<ICharacter>()
                : null;

            Followers = ctx.Version > 4
                ? ctx.Reader.UInt32()
                : 0;
            
            // from Revenge
            if (ctx.Scenario?.Info.Symbol == RevengeScenario.Tag || ctx.Version > 19)
            {
                LastLocation = ctx is {Version: > 10, IsSaveGame: true}
                    ? ctx.Reader.Loc()
                    : Loc.Zero;

                HomeStronghold = ctx.ReadEntity<IStronghold>();
                DesiredObject = ctx.ReadEntity<IObject>();
                FightingAgainst = ctx.IsSaveGame
                    ? ctx.ReadEntity<ICharacter>()
                    : null;

                BattleLost = ctx.IsSaveGame
                    ? ctx.Reader.UInt32()
                    : 0;
            }

            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.String(LongName);
            ctx.Writer.String(ShortName);
            ctx.Writer.Enum(Looking);
            ctx.Writer.Time(Time);
            
            Units.FirstOrDefault()?.Save(ctx);
            Units.LastOrDefault()?.Save(ctx);

            BattleInfo.Save(ctx);

            ctx.Writer.UInt32(Reckless);
            ctx.Writer.UInt32(Energy); 
            ctx.Writer.UInt32(Strength);
            ctx.Writer.UInt32(Cowardly);
            ctx.Writer.UInt32(Courage);
            ctx.Writer.UInt32(Fear);

            Recruitment.Save(ctx);
            
            ctx.Writer.Enum(Race);

            ctx.WriteEntity(Carrying.FirstOrDefault());
            
            ctx.WriteEntity(KilledBy);
            ctx.Writer.Enum(Gender);
            ctx.Writer.Enum(Loyalty);
            ctx.WriteEntity(Liege);
            ctx.WriteEntity(Foe);
            ctx.Writer.Enum(WaitStatus);
            ctx.Writer.Enum(Orders);
            ctx.Writer.UInt32(Despondency);
            ctx.Writer.Enum(Traits);
            ctx.WriteEntity(Following);
            ctx.Writer.UInt32(Followers);
            
            // revenge
            if (ctx.Scenario?.Info.Symbol == RevengeScenario.Tag || ctx.Version > 19)
            {
                if (ctx.IsSaveGame)
                {
                    ctx.Writer.Loc(LastLocation);
                }

                ctx.WriteEntity(HomeStronghold);
                ctx.WriteEntity(DesiredObject);

                if (!ctx.IsSaveGame) return true;

                ctx.WriteEntity(FightingAgainst);
                ctx.Writer.UInt32(BattleLost);
            }

            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;

            LongName = bundle.String(nameof(LongName));
            ShortName = bundle.String(nameof(ShortName));
            Looking = bundle.Direction(nameof(Looking));
            Time = bundle.Time(nameof(Time));

            Units[0] = new Warriors { Total = bundle.UInt32(nameof(Scenario.Warriors)) };
            Units[1] = new Riders{ Total = bundle.UInt32(nameof(Scenario.Riders)) };
            
            Reckless = bundle.UInt32(nameof(Reckless));
            Energy = bundle.UInt32(nameof(Energy));
            Strength = bundle.UInt32(nameof(Strength));
            Cowardly = bundle.UInt32(nameof(Cowardly));
            Courage = bundle.UInt32(nameof(Courage));
            Fear = bundle.UInt32(nameof(Fear));

            Recruitment.Load(bundle);
            
            Race = bundle.Race(nameof(Race));
            Carrying = bundle.EntityArray<IObject>(nameof(Carrying));
            KilledBy = bundle.Entity<IObject>(nameof(KilledBy));
            Gender = bundle.Gender(nameof(Gender));
            Loyalty = bundle.Race(nameof(Loyalty));
            Liege = bundle.Entity<ICharacter>(nameof(Liege));
            Foe = bundle.Entity<ICharacter>(nameof(Foe));
            WaitStatus = bundle.WaitStatus(nameof(WaitStatus));
            Orders = bundle.Orders(nameof(Orders));
            Despondency = bundle.UInt32(nameof(Despondency));
            Traits = bundle.Enum<LordTraits>(nameof(Traits));
            Following = bundle.Entity<ICharacter>(nameof(Following));
            Followers = 0;
            
            // revenge
            HomeStronghold = bundle.Entity<IStronghold>(nameof(HomeStronghold));
            DesiredObject = bundle.Entity<IObject>(nameof(DesiredObject));

            
            return true;
        }

        #endregion
    }
}