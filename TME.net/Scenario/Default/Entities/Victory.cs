using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Entities
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Victory : Entity, IVictory
    {
        public int Priority { get; internal set; }
        public uint String { get; internal set; }
        public MXId Mission { get; internal set; } = MXId.None;

        public bool IsComplete => IsFlags(VictoryFlags.Complete);
        public bool IsGameOver => IsFlags(VictoryFlags.GameOver);
        
        public override bool Load(ISerializeContext ctx)
        {
            if( !base.Load(ctx) ) return false;

            Priority = ctx.Reader.ReadInt32();
            Mission = ctx.Reader.ReadMXId();
            String = ctx.Reader.ReadUInt32();

            return true;
        }
    }
}