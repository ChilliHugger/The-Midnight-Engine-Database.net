using System;
using System.Collections.Generic;
using TME.Default;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Scenario
{
    public class RouteNode : Item, IRouteNode
    {
        public IRouteNodes RouteNodes { get; }

        public RouteNode(IRouteNodes routeNodes)
        {
            Type = EntityType.RouteNode;
            RouteNodes = routeNodes;
        }

        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            RouteNodes.Load(ctx);
            
            return true;

        }

    }
}
