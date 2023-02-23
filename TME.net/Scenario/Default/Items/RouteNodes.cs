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
        
        public bool Load(Bundle bundle)
        {
            if (bundle.TryGetValue(nameof(Nodes), out var value))
            {
                if (value is IRouteNode?[] nodes)
                {
                    Nodes = nodes;
                }
                return true;
            }
            return false;
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }
    }
}