using System;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Default.Interfaces
{
    public interface IUnit: ISerializable
    {
        UnitType Type { get; }
        uint Total { get; }
        uint Energy { get; }
        uint Lost { get; }
        uint Slew { get; }
    }
}
