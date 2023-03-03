using System;
using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public interface ISerializeWriter
    {
        void Int16(short value);
        void Int32(int value);
        void UInt16(ushort value);
        void UInt32(uint value);
        void UInt64(ulong value);
        void String(string value);
        void Size(Size value);
        void Loc(Loc value);
        void MXId(MXId value);
        void MXId(IEntity? value);
        void Enum<T>(T value) where T : Enum;
        void Time(Time value);
    }
}