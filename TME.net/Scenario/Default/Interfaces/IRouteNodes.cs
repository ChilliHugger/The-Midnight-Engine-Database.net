using TME.Serialize;

namespace TME.Scenario.Default.Interfaces
{
    public interface IRouteNodes : ISerializable, IBundle
    {
        IRouteNode?[] Nodes { get; }
    }
}