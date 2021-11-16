using TME.Default.Interfaces;

namespace TME.Scenario.Default.Interfaces
{
    public interface IRouteNode : IItem
    {
        IRouteNodes RouteNodes { get; }
    }
}
