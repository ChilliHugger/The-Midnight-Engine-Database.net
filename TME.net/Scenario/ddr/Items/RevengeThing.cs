using System.Diagnostics.CodeAnalysis;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RevengeThing : Thing, IRevengeThing
    {
        public ObjectPower ObjectPower { get; internal set; }
        public ObjectType ObjectType { get; internal set; }
        public bool IsSpecial => IsFlags(ThingFlags.Special);
        public bool IsRandomStart => IsFlags(ThingFlags.RandomStart);
        public bool CanHelpRecruitment => IsFlags(ThingFlags.Recruitment);

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            ObjectType = (ctx.Version > 10)
                ? ctx.Reader.ReadEnum<ObjectType>()
                : ObjectType.None;
            ObjectPower = (ctx.Version > 10)
                ? ctx.Reader.ReadEnum<ObjectPower>()
                : ObjectPower.None;
            
            return true;
        }
    }
}