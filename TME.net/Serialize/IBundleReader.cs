using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public interface IBundleReader
    {
        public Bundle Raw { get; }
        
        //int PeekInt32();
        public T Enum<T>(string name);

        public short Int16(string name);
        public ushort UInt16(string name);
        public int Int32(string name);
        public uint UInt32(string name);
        public ulong UInt64(string name);
        public string String(string name);
        public Size Size(string name);
        public Loc Loc(string name);
        public Time Time(string name);
        public Direction Direction(string name);
        public Race Race(string name);
        public Gender Gender(string name);
        public WaitStatus WaitStatus(string name);
        public Orders Orders(string name);
        public ArmyType ArmyType(string name);
        public UnitType UnitType(string name);
        public ThingType ThingType(string name);
        public Terrain Terrain(string name);
        public MXId MXId(string name);
        public MXId MXId(EntityType type, string name);
        
        public MXId Id(string name);
        public T Flags<T>(string name);

    }
}