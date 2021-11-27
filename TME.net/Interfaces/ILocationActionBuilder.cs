using TME.Scenario.Default.Flags;
using TME.Scenario.Default.LocationInfoBuilders;

namespace TME.Interfaces
{
    public interface ILocationActionBuilder
    {
        ILocationActionBuilder Tunnel(bool tunnel);
        ILocationActionBuilder Here(LocationInfo locationInfo);
        ILocationActionBuilder Ahead(LocationInfo locationInfo);
        LocationInfoFlags Build();
    }
}