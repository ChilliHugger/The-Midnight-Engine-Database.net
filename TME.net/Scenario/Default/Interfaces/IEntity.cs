using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IEntityInternal
    {
        void SetFlags(ulong flags, bool value);
    }
    
    public interface IEntity
    {
        ulong RawFlags { get; }
        uint RawId { get; }

        EntityType Type { get; }
        MXId Id { get; }
        object? UserData { get; }
        string Symbol { get; }

        bool IsSymbol(string value);
        bool HasFlags(ulong mask);
    }
}
