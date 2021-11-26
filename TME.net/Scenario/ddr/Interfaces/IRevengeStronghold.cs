using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeStronghold : IStronghold
    {
        uint Energy { get; }
        Race Loyalty { get; }
    }
}