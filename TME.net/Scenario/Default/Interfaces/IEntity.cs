using System;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IEntityInternal : IEntity
    {
        void SetFlags<T>(T flags, bool value)  where T : Enum;
        void SetId(MXId id);
    }
    
    public interface IEntity
    {
        uint RawFlags { get; }
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
