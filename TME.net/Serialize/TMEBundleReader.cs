using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public class TMEBundleReader : IBundleReader
    {
        public TMEBundleReader(Bundle bundle)
        {
            Raw = bundle;
        }

        public Bundle Raw { get; }


        public ulong UInt64(string name)
        {
            throw new System.NotImplementedException();
        }

        public string String(string name) => (string) (Raw[name] ?? "");
        public Size Size(string name)
        {
            throw new System.NotImplementedException();
        }

        //public MXId Id(string name) => (MXId) (Raw[name] ?? MXId.None);

        public MXId Id(string name)
        {
            throw new System.NotImplementedException();
        }

        public T Flags<T>(string name) => (T) (Raw[name] ?? 0);
        
        public Loc Loc(string name) => (Loc) (Raw[name] ?? Scenario.Default.Base.Loc.Zero);
        
        public Time Time(string name)
        {
            throw new System.NotImplementedException();
        }

        public Direction Direction(string name)
        {
            throw new System.NotImplementedException();
        }

        public Race Race(string name)
        {
            throw new System.NotImplementedException();
        }

        public Gender Gender(string name)
        {
            throw new System.NotImplementedException();
        }

        public WaitStatus WaitStatus(string name)
        {
            throw new System.NotImplementedException();
        }

        public Orders Orders(string name)
        {
            throw new System.NotImplementedException();
        }

        public ArmyType ArmyType(string name)
        {
            throw new System.NotImplementedException();
        }

        public UnitType UnitType(string name)
        {
            throw new System.NotImplementedException();
        }

        public ThingType ThingType(string name)
        {
            throw new System.NotImplementedException();
        }

        public Terrain Terrain(string name)
        {
            throw new System.NotImplementedException();
        }

        public MXId MXId(string name)
        {
            throw new System.NotImplementedException();
        }

        public MXId MXId(EntityType type, string name)
        {
            throw new System.NotImplementedException();
        }

        public uint UInt32(string name) => (uint) (Raw[name] ?? 0);
        
        public T Enum<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public short Int16(string name)
        {
            throw new System.NotImplementedException();
        }

        public ushort UInt16(string name)
        {
            throw new System.NotImplementedException();
        }

        public int Int32(string name)=> (int) (Raw[name] ?? 0);
        
 
    }
}