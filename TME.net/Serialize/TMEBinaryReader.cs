using System;
using System.IO;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public sealed class TMEBinaryReader : ISerializeReader, IDisposable
    {
        private readonly BinaryReader _reader;
        //public bool EnableByteSwap { get; set; }

        public TMEBinaryReader(Stream stream)
        {
            _reader = new BinaryReader(stream);
        }
        
        public void Dispose()
        {
            _reader.Dispose();
        }
        
        public int PeekInt32()
        {
            var pos = _reader.BaseStream.Position;
            var value = _reader.ReadInt32();
            _reader.BaseStream.Position = pos;
            return value;
        }
        
        //
        public short Int16() => _reader.ReadInt16();
        public ushort UInt16() => _reader.ReadUInt16();
        public int Int32() => _reader.ReadInt32();
        public uint UInt32() => _reader.ReadUInt32();
        public ulong UInt64() => _reader.ReadUInt64();

        public string String()
        {
            var length = _reader.ReadUInt16();
            var chars = _reader.ReadChars(length);
            return new string(chars);
        }

        public Size Size()
        {
            var w = _reader.ReadInt32();
            var h = _reader.ReadInt32();
            return new Size(w, h);
        }

        public Loc Loc()
        {
            var x = _reader.ReadInt32();
            var y = _reader.ReadInt32();
            return new Loc(x, y);
        }

        public MXId MXId()
        {
            return _reader.ReadUInt32();
        }
        public MXId MXId(EntityType type)
        {
            var id = _reader.ReadUInt32();
            return new MXId(type,id);
        }

        public T Enum<T>() => (T)(object)_reader.ReadUInt32();
        
        public Time Time() => _reader.ReadUInt32();
        public Direction Direction() => (Direction)_reader.ReadUInt32();
        public Race Race() => (Race)_reader.ReadUInt32();
        public Gender Gender() => (Gender)_reader.ReadUInt32();
        public WaitStatus WaitStatus() => (WaitStatus)_reader.ReadUInt32();
        public Orders Orders() => (Orders)_reader.ReadUInt32();
        public ArmyType ArmyType() => (ArmyType)_reader.ReadUInt32();
        public UnitType UnitType() => (UnitType)_reader.ReadUInt32();
        public Terrain Terrain() => (Terrain)_reader.ReadUInt32();
        public ThingType ThingType() => (ThingType)_reader.ReadUInt32();
        
    }
}
