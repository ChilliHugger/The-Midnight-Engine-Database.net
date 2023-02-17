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

            Priority = ctx.Reader.ReadInt32();
            Mission = ctx.ReadEntity<IMission>();
            String = ctx.Reader.ReadUInt32();
            return true;
        }
    }
}