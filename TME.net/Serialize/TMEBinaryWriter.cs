// ReSharper disable MemberCanBePrivate.Global

using System;
using System.IO;
using System.Linq.Expressions;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public sealed class TMEBinaryWriter : ISerializeWriter, IDisposable
    {
        private readonly BinaryWriter _writer;
 
        public TMEBinaryWriter(Stream stream)
        {
            _writer = new BinaryWriter(stream);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
        
        public void Int16(short value) => _writer.Write(value);
        public void Int32(int value) => _writer.Write(value);
        public void UInt16(ushort value) => _writer.Write(value);
        public void UInt32(uint value) => _writer.Write(value);
        public void UInt64(ulong value) => _writer.Write(value);
        
        public void String(string value)
        {
            var length = (ushort) value.Length;
            _writer.Write(length);
            _writer.Write(value.ToCharArray());
        }
        
        
        public void Size(Size value)
        {
            Int32(value.Width);
            Int32(value.Height);
        }

        public void Loc(Loc value)
        {
            Int32(value.X);
            Int32(value.Y);
        }

        public void MXId(IEntity? value) => UInt32(value?.Id ?? 0);
        public void MXId(MXId value) => UInt32(value);
        public void Enum<T>(T value) where T : Enum => UInt32(value.Raw());
        public void Time(Time value) => UInt32(value);
        
        //public void Direction(Direction value) => Enum(value);
        // public Race Race() => (Race)_reader.ReadUInt32();
        // public Gender Gender() => (Gender)_reader.ReadUInt32();
        // public WaitStatus WaitStatus() => (WaitStatus)_reader.ReadUInt32();
        // public Orders Orders() => (Orders)_reader.ReadUInt32();
        // public ArmyType ArmyType() => (ArmyType)_reader.ReadUInt32();
        // public UnitType UnitType() => (UnitType)_reader.ReadUInt32();
        // public Terrain Terrain() => (Terrain)_reader.ReadUInt32();
        // public ThingType ThingType() => (ThingType)_reader.ReadUInt32();
    }
}