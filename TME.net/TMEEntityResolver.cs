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
        private readonly IDatabase _database;

        public TMEEntityResolver(IDatabase database)
        {
            _database = database;
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
                    return _database.Lords.ElementAt(id);

                case EntityType.RouteNode:
                    return _database.RouteNodes.ElementAt(id);
                
                case EntityType.Regiment:
                    return _database.Regiments.ElementAt(id);
            }

            return null;
        }
    }

}