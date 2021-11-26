using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IUnitInternal : IUnit
    {
        void AddLoses(uint value);
        void SetKilled(uint value);
        void SetLost(uint value);
        void SetTotal(uint value);
    }
    
    public interface IUnit: ISerializable
    {
        UnitType Type { get; }
        uint Total { get; }
        uint Energy { get; }
        uint Lost { get; }
        uint Killed { get; }
    }
}
