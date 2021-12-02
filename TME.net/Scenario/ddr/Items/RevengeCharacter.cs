using System.Diagnostics.CodeAnalysis;
using Autofac.Features.AttributeFilters;
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
        public IRevengeThing? DesiredObject { get; internal set; }
        public IRevengeStronghold? HomeStronghold { get; internal set; }
        public IRevengeLord? FightingAgainst { get; internal set; }
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

            LastLocation = (ctx.Version > 10 && ctx.IsSaveGame)
                ? ctx.Reader.ReadLoc()
                : Loc.Zero;

            HomeStronghold = ctx.ReadEntity<IRevengeStronghold>();
            DesiredObject = ctx.ReadEntity<IRevengeThing>();
            FightingAgainst = ctx.IsSaveGame
                ? ctx.ReadEntity<IRevengeLord>()
                : null;

            BattleLost = ctx.IsSaveGame
                ? ctx.Reader.ReadUInt32()
                : 0;
            
            return true;
        }
    }
}