using System.Diagnostics.CodeAnalysis;
using Autofac.Features.AttributeFilters;
using TME.Interfaces;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class RevengeCharacter : Character, IRevengeLord
    {
        public Loc LastLocation { get; internal set; } = Loc.Zero;
        public IObject? DesiredObject { get; internal set; }
        public IStronghold? HomeStronghold { get; internal set; }
        public ICharacter? FightingAgainst { get; internal set; }
        public uint BattleLost { get; internal set; }
        public MXId TargetId { get; internal set; } = MXId.None;
        public Loc TargetLocation { get; internal set; } = Loc.Zero;
        
        public IUnit? Unit =>
            ArmyType switch
            {
                UnitType.Rider => Units[1],
                UnitType.Warrior => Units[0],
                _ => null
            };

        public UnitType ArmyType =>
            IsFlags(LordFlags.AllowedRiders) 
                ? UnitType.Rider : IsFlags(LordFlags.AllowedWarriors) 
                    ? UnitType.Warrior 
                    : UnitType.None;

        public uint ArmySize => Unit?.Total ?? 0;
        
        public RevengeCharacter(
            IVariables variables) : base(variables)
        {
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

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
            
            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            if (ctx.IsSaveGame)
            {
                ctx.Writer.Loc(LastLocation);
            }

            ctx.WriteEntity(HomeStronghold);
            ctx.WriteEntity(DesiredObject);

            if (!ctx.IsSaveGame) return true;
            
            ctx.WriteEntity(FightingAgainst); 
            ctx.Writer.UInt32(BattleLost);
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;
            
            HomeStronghold = bundle.Entity<IStronghold>(nameof(HomeStronghold));
            DesiredObject = bundle.Entity<IObject>(nameof(DesiredObject));
            
            return true;
        }
    }
}