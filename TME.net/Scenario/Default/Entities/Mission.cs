using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        
        public int Priority { get; internal set; }
        public MissionObjective Objective { get; internal set; } = MissionObjective.None;
        public MissionCondition Condition { get; internal set; } = MissionCondition.None;
        public IList<IEntity> References { get; internal set; } = new List<IEntity>();
        public int Points { get; internal set; }
        public IEntity? Scorer { get; internal set; }
        public MissionAction Action { get; internal set; } = MissionAction.None;
        public IEntity? ActionId { get; internal set; } 
        
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

            References.Clear();

            References = Enumerable.Range(0, MaxReferences)
                .Select(_ => ctx.ReadEntity())
                .WhereNotNull()
                .ToList();
            
            Points = ctx.Reader.ReadInt32();
            Scorer = ctx.ReadEntity();
            Action = ctx.Reader.ReadEnum<MissionAction>();
            ActionId = ctx.ReadEntity();
            
            return true;
        }
    }
}