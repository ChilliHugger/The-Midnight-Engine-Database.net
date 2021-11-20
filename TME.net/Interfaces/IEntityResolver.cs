using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Interfaces
{
    public interface IEntityResolver
    {
        T? EntityById<T>(int id) where T : IEntity;
        object? EntityById(MXId id);
        object? EntityById(EntityType type, int id);
    }
}