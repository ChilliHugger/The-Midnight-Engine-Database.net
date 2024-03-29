using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Scenario.Default.Items
{
    public class RouteNodes : IRouteNodes
    {
        public IRouteNode?[] Nodes { get; internal set; }

        public RouteNodes()
        {
            Nodes = new IRouteNode[2];
        }
        
        public bool Load(ISerializeContext ctx)
        {
            Nodes[0] = ctx.ReadEntity<IRouteNode>();
            Nodes[1] = ctx.ReadEntity<IRouteNode>();
            return true;
        }
        
        public bool Save(ISerializeContext ctx)
        {
            ctx.WriteEntity(Nodes[0]);
            ctx.WriteEntity(Nodes[1]);
            
            return true;
        }
        
        public bool Load(IBundleReader bundle)
        {
            Nodes = bundle.EntityArray<IRouteNode>(nameof(Nodes));
            return true;
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }
    }
}