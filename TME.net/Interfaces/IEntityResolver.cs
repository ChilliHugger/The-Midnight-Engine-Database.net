using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Interfaces
{
    public interface IEntityResolver
    {
        public T? EntityById<T>(int id) where T : IEntity;
        public T? EntityBySymbol<T>(string symbol) where T : IEntity;
        public IEntity? EntityById(MXId id);
        public IEntity? EntityById(EntityType type, int id);
        public IEntity? EntityBySymbol(string symbol);
    }

    public class NoopEntityResolver : IEntityResolver
    {
        public T? EntityById<T>(int id) where T : IEntity => default;
        public T? EntityBySymbol<T>(string symbol) where T : IEntity => default;
        public IEntity? EntityById(MXId id) => default;
        public IEntity? EntityById(EntityType type, int id) => default;
        public IEntity? EntityBySymbol(string symbol) => default;
    }
}