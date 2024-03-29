using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public class NullReader : ISerializeReader
    {
        public int PeekInt32()
        {
            throw new System.NotImplementedException();
        }

        public T Enum<T>()
        {
            throw new System.NotImplementedException();
        }

        public short Int16()
        {
            throw new System.NotImplementedException();
        }

        public ushort UInt16()
        {
            throw new System.NotImplementedException();
        }

        public int Int32()
        {
            throw new System.NotImplementedException();
        }

        public uint UInt32()
        {
            throw new System.NotImplementedException();
        }

        public ulong UInt64()
        {
            throw new System.NotImplementedException();
        }

        public string String()
        {
            throw new System.NotImplementedException();
        }

        public Size Size()
        {
            throw new System.NotImplementedException();
        }

        public Loc Loc()
        {
            throw new System.NotImplementedException();
        }

        public Time Time()
        {
            throw new System.NotImplementedException();
        }

        public Direction Direction()
        {
            throw new System.NotImplementedException();
        }

        public Race Race()
        {
            throw new System.NotImplementedException();
        }

        public Gender Gender()
        {
            throw new System.NotImplementedException();
        }

        public WaitStatus WaitStatus()
        {
            throw new System.NotImplementedException();
        }

        public Orders Orders()
        {
            throw new System.NotImplementedException();
        }

        public ArmyType ArmyType()
        {
            throw new System.NotImplementedException();
        }

        public UnitType UnitType()
        {
            throw new System.NotImplementedException();
        }

        public ThingType ThingType()
        {
            throw new System.NotImplementedException();
        }

        public Terrain Terrain()
        {
            throw new System.NotImplementedException();
        }

        public MXId MXId()
        {
            throw new System.NotImplementedException();
        }

        public MXId MXId(EntityType type)
        {
            throw new System.NotImplementedException();
        }
    }
}