using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeThing : IObject
    {
        ObjectPower ObjectPower { get; }
        ObjectType ObjectType { get; }
    }
}