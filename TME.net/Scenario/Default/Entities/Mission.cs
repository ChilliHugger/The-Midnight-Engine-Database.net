using System.Diagnostics.CodeAnalysis;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.Entities
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Mission : Entity, IMission
    {
        private const int MaxReferences = 5;
        
        public int Priority { get; set; }
        public MissionObjective Objective { get; set; } = MissionObjective.None;
        public MissionCondition Condition { get; set; } = MissionCondition.None;
        public MXId[] References { get; set; } = new MXId[MaxReferences];
        public int Points { get; set; }
        public MXId Scorer { get; set; } = MXId.None;
        public MissionAction Action { get; set; } = MissionAction.None;
        public MXId ActionId { get; set; } = MXId.None;
        
        public bool IsComplete => HasFlags(MissionFlags.Complete.Raw());
        public bool IsAny => HasFlags(MissionFlags.Any.Raw());

        public Mission() : base(EntityType.Mission)
        {
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Priority = ctx.Reader.ReadInt32();
            Objective = ctx.Reader.ReadEnum<MissionObjective>();
            Condition = ctx.Reader.ReadEnum<MissionCondition>();

            for (var ii = 0; ii < MaxReferences; ii++)
            {
                References[ii] = ctx.Reader.ReadMXId();
            }

            Points = ctx.Reader.ReadInt32();
            Scorer = ctx.Reader.ReadMXId();
            Action = ctx.Reader.ReadEnum<MissionAction>();
            ActionId = ctx.Reader.ReadMXId();
            
            return true;
        }
    }
}