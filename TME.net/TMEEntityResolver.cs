using System.Linq;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME
{
    public class TMEEntityResolver : IEntityResolver
    {
        private readonly IEntityContainer _entityContainer;

        public TMEEntityResolver(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        public T? EntityById<T>(int id)
            where T : IEntity
        {
            if (typeof(T) == typeof(ILord))
            {
                return (T?) EntityById(EntityType.Character, id);
            }
            else if (typeof(T) == typeof(IRegiment))
            {
                return (T?) EntityById(EntityType.Regiment, id);
            }
            else if (typeof(T) == typeof(IRouteNode))
            {
                return (T?) EntityById(EntityType.RouteNode, id);
            }
            else if (typeof(T) == typeof(IStronghold))
            {
                return (T?) EntityById(EntityType.Stronghold, id);
            }

            return default;
        }

        // ReSharper disable once UnusedMember.Global
        public object? EntityById(MXId id) => EntityById(id.Type, id.RawId);

        // ReSharper disable once MemberCanBePrivate.Global
        public object? EntityById(EntityType type, int index)
        {
            if (index == 0) return null;
            
            var id = index - 1;
            switch(type)
            {
                case EntityType.Character:
                    return _entityContainer.Lords.ElementAt(id);
                case EntityType.RouteNode:
                    return _entityContainer.RouteNodes.ElementAt(id);
                case EntityType.Regiment:
                    return _entityContainer.Regiments.ElementAt(id);
                case EntityType.Stronghold:
                    return _entityContainer.Strongholds.ElementAt(id);
            }

            return null;
        }
    }

}