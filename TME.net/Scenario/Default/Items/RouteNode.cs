﻿using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public class RouteNode : Item, IRouteNode
    {
        public IRouteNodes RouteNodes { get; internal set; }

        public RouteNode(IRouteNodes routeNodes) : base(EntityType.RouteNode)
        {
            RouteNodes = routeNodes;
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            RouteNodes.Load(ctx);
            
            return true;
        }
        
        public override bool Save(ISerializeContext ctx)
        {
            if (!base.Save(ctx)) return false;

            RouteNodes.Save(ctx);
            
            return true;
        }
        
        public override bool Load(IBundleReader bundle)
        {
            if (!base.Load(bundle)) return false;
            
            return RouteNodes.Load(bundle);
        }
    }
}
