using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Entities
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Victory : Entity, IVictory
    {
        public new VictoryFlags Flags => (VictoryFlags) RawFlags;
        public int Priority { get; internal set; }
        public uint String { get; internal set; }
        public IMission? Mission { get; internal set; }

        public bool IsComplete => IsFlags(VictoryFlags.Complete);
        public bool IsGameOver => IsFlags(VictoryFlags.GameOver);
        
        public Victory() : base(EntityType.Victory)
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if( !base.Load(ctx) ) return false;

            Priority = ctx.Reader.Int32();
            Mission = ctx.ReadEntity<IMission>();
            String = ctx.Reader.UInt32();
            return true;
        }

        public override bool Load(IBundleReader bundle)
        {
            if( !base.Load(bundle) ) return false;

            Priority = bundle.Int32(nameof(Priority));
            Mission = bundle.Entity<IMission>(nameof(Mission));
            String = bundle.UInt32(nameof(String));
            return true;
        }
    }
}