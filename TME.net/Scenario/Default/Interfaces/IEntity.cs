using System;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface IEntity
    {
        uint RawFlags { get; }
        EntityFlags Flags { get; }
        uint RawId { get; }

        EntityType Type { get; }
        MXId Id { get; }
        object? UserData { get; }
        string Symbol { get; }

        bool IsSymbol(string value);
        bool HasFlags(uint mask);
        bool IsFlags<T>(T mask) where T : Enum;
    }
}
