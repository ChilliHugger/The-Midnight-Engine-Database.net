using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
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
        
        public new MissionFlags Flags => (MissionFlags) RawFlags;

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

            Priority = ctx.Reader.Int32();
            Objective = ctx.Reader.Enum<MissionObjective>();
            Condition = ctx.Reader.Enum<MissionCondition>();

            References.Clear();

            References = Enumerable.Range(0, MaxReferences)
                .Select(_ => ctx.ReadEntity())
                .WhereNotNull()
                .ToList();
            
            Points = ctx.Reader.Int32();
            Scorer = ctx.ReadEntity();
            Action = ctx.Reader.Enum<MissionAction>();
            ActionId = ctx.ReadEntity();
            
            return true;
        }

        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            ctx.Writer.Int32(Priority);
            ctx.Writer.Enum(Objective);
            ctx.Writer.Enum(Condition);
            
            for(int ii=0; ii<MaxReferences; ii++)
            {
                var id = (ii < References.Count) ? References[ii].Id : MXId.None;
                ctx.Writer.MXId(id);
            }
            
            ctx.Writer.Int32(Points);
            ctx.Writer.MXId(Scorer);
            ctx.Writer.Enum(Action);
            ctx.Writer.MXId(ActionId);

            
            
            return true;
        }


        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;

            Priority = bundle.Int32(nameof(Priority));
            Objective = bundle.Enum<MissionObjective>(nameof(Objective));
            Condition = bundle.Enum<MissionCondition>(nameof(Condition));
            
            // References.Clear();
            // if (bundle.Raw.TryGetValue(nameof(References), out var value))
            // {
            //     if (value is IEntity[] nodes)
            //     {
            //         References = nodes;
            //     }
            // }
            References = bundle.EntityArray<IEntity>(nameof(References));
            Points = bundle.Int32(nameof(Points));
            Scorer = bundle.Entity<IEntity>(nameof(Scorer));
            Action = bundle.Enum<MissionAction>(nameof(Action));
            ActionId = bundle.Entity<IEntity>(nameof(ActionId));
            
            return true;
        }
    }
}