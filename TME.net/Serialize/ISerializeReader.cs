using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public interface ISerializeReader
    {
        int PeekInt32();
        T Enum<T>();

        short Int16();
        ushort UInt16();
        int Int32();
        uint UInt32();
        ulong UInt64();
        string String();
        Size Size();
        Loc Loc();
        Time Time();
        Direction Direction();
        Race Race();
        Gender Gender();
        WaitStatus WaitStatus();
        Orders Orders();
        ArmyType ArmyType();
        UnitType UnitType();
        ThingType ThingType();
        Terrain Terrain();
        MXId MXId();
        MXId MXId(EntityType type);
    }
}
