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

        public T ReadEnum<T>()
        {
            throw new System.NotImplementedException();
        }

        public short ReadInt16()
        {
            throw new System.NotImplementedException();
        }

        public ushort ReadUInt16()
        {
            throw new System.NotImplementedException();
        }

        public int ReadInt32()
        {
            throw new System.NotImplementedException();
        }

        public uint ReadUInt32()
        {
            throw new System.NotImplementedException();
        }

        public ulong ReadUInt64()
        {
            throw new System.NotImplementedException();
        }

        public string ReadString()
        {
            throw new System.NotImplementedException();
        }

        public Size ReadSize()
        {
            throw new System.NotImplementedException();
        }

        public Loc ReadLoc()
        {
            throw new System.NotImplementedException();
        }

        public Time ReadTime()
        {
            throw new System.NotImplementedException();
        }

        public Direction ReadDirection()
        {
            throw new System.NotImplementedException();
        }

        public Race ReadRace()
        {
            throw new System.NotImplementedException();
        }

        public Gender ReadGender()
        {
            throw new System.NotImplementedException();
        }

        public WaitStatus ReadWaitStatus()
        {
            throw new System.NotImplementedException();
        }

        public Orders ReadOrders()
        {
            throw new System.NotImplementedException();
        }

        public ArmyType ReadArmyType()
        {
            throw new System.NotImplementedException();
        }

        public UnitType ReadUnitType()
        {
            throw new System.NotImplementedException();
        }

        public ThingType ReadThingType()
        {
            throw new System.NotImplementedException();
        }

        public Terrain ReadTerrain()
        {
            throw new System.NotImplementedException();
        }

        public MXId ReadMXId()
        {
            throw new System.NotImplementedException();
        }

        public MXId ReadMXId(EntityType type)
        {
            throw new System.NotImplementedException();
        }
    }
}