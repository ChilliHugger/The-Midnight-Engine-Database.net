using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public interface ISerializeReader
    {
        int PeekInt32();
        short ReadInt16();
        ushort ReadUInt16();
        int ReadInt32();
        uint ReadUInt32();
        ulong ReadUInt64();
        string ReadString();
        Size ReadSize();
        Loc ReadLoc();
        Time ReadTime();
        Direction ReadDirection();
        Race ReadRace();
        Gender ReadGender();
        WaitStatus ReadWaitStatus();
        Orders ReadOrders();
        ArmyType ReadArmyType();
        UnitType ReadUnitType();
        ThingType ReadThingType();
        Terrain ReadTerrain();
        MXId ReadMXId();
    }
}
