using System;
using System.IO;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public class TMEBinaryReader : ISerializeReader, IDisposable
    {
        private readonly BinaryReader _reader;
        public bool EnableByteSwap { get; set; }

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
        public short ReadInt16() => _reader.ReadInt16();
        public ushort ReadUInt16() => _reader.ReadUInt16();
        public int ReadInt32() => _reader.ReadInt32();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public ulong ReadUInt64() => _reader.ReadUInt64();

        public string ReadString()
        {
            var length = _reader.ReadUInt16();
            var chars = _reader.ReadChars(length);
            return new string(chars);
        }

        public Size ReadSize()
        {
            var w = _reader.ReadInt32();
            var h = _reader.ReadInt32();
            return new Size(w, h);
        }

        public Loc ReadLoc()
        {
            var x = _reader.ReadInt32();
            var y = _reader.ReadInt32();
            return new Loc(x, y);
        }

        public MXId ReadMXId()
        {
            var id = _reader.ReadUInt32();
            return (MXId) id;
        }
        
        public Time ReadTime() => _reader.ReadUInt32();
        public Direction ReadDirection() => (Direction)_reader.ReadUInt32();
        public Race ReadRace() => (Race)_reader.ReadUInt32();
        public Gender ReadGender() => (Gender)_reader.ReadUInt32();
        public WaitStatus ReadWaitStatus() => (WaitStatus)_reader.ReadUInt32();
        public Orders ReadOrders() => (Orders)_reader.ReadUInt32();
        public ArmyType ReadArmyType() => (ArmyType)_reader.ReadUInt32();
        public UnitType ReadUnitType() => (UnitType)_reader.ReadUInt32();
        public Terrain ReadTerrain() => (Terrain)_reader.ReadUInt32();
        
    }
}
