﻿using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Serialize;
using TME.Types;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class AreaInfo : Info
    {
        public string Prefix { get; internal set; } = "";
        
        public AreaInfo() : base(EntityType.AreaInfo) 
        {
        }
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            Prefix = ctx.Reader.ReadString();
            
            return true;
        }
        
        public override bool Load(Bundle bundle)
        {
            if(!base.Load(bundle)) return false;

            Prefix = bundle.String(nameof(Prefix));
       
            return true;
        }
    }
}
