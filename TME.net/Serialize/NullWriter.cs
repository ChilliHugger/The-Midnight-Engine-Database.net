using System;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public class NullWriter : ISerializeWriter
    {
        public void Int16(short value)
        {
            throw new NotImplementedException();
        }

        public void Int32(int value)
        {
            throw new NotImplementedException();
        }

        public void UInt16(ushort value)
        {
            throw new NotImplementedException();
        }

        public void UInt32(uint value)
        {
            throw new NotImplementedException();
        }

        public void UInt64(ulong value)
        {
            throw new NotImplementedException();
        }

        public void String(string value)
        {
            throw new NotImplementedException();
        }

        public void Size(Size value)
        {
            throw new NotImplementedException();
        }

        public void Loc(Loc value)
        {
            throw new NotImplementedException();
        }

        public void MXId(MXId value)
        {
            throw new NotImplementedException();
        }

        public void MXId(IEntity? value)
        {
            throw new NotImplementedException();
        }

        public void Enum<T>(T value) where T : Enum
        {
            throw new NotImplementedException();
        }

        public void Time(Time value)
        {
            throw new NotImplementedException();
        }
    }
}