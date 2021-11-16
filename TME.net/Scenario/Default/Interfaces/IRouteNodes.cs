using TME.Serialize;

namespace TME.Scenario.Default.Interfaces
{
    public interface IRouteNodes : ISerializable
    {
        IRouteNode?[] Nodes { get; }
    }
}