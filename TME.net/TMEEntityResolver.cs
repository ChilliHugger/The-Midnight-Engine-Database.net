using System;
using System.Collections.Generic;
using System.Linq;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME
{
    public class TMEEntityResolver : IEntityResolver
    {
        private readonly IEntityContainer _entityContainer;

        private readonly Dictionary<Type, EntityType> _typeMappings = new()
        {
            { typeof(ICharacter), EntityType.Character },
            { typeof(IRevengeLord), EntityType.Character },
            { typeof(IRegiment), EntityType.Regiment },
            { typeof(IRouteNode), EntityType.RouteNode },
            { typeof(IStronghold), EntityType.Stronghold },   
            { typeof(IRevengeStronghold), EntityType.Stronghold },
            { typeof(IWaypoint), EntityType.Waypoint },
            { typeof(IObject), EntityType.Thing },
            { typeof(IRevengeThing), EntityType.Thing },
            { typeof(IMission), EntityType.Mission },
            { typeof(IVictory), EntityType.Victory },
            
            { typeof(DirectionInfo), EntityType.DirectionInfo },
            { typeof(UnitInfo), EntityType.UnitInfo },
            { typeof(RaceInfo), EntityType.RaceInfo },                
            { typeof(GenderInfo), EntityType.GenderInfo },
            { typeof(TerrainInfo), EntityType.TerrainInfo },
            { typeof(AreaInfo), EntityType.AreaInfo },
            { typeof(CommandInfo), EntityType.CommandInfo },
        };
        
        public TMEEntityResolver(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        public T? EntityBySymbol<T>(string symbol)
            where T : IEntity
        {
            if(!string.IsNullOrWhiteSpace(symbol)) 
            {
                if (_entityContainer.SymbolCache.TryGetValue(symbol, out var entity))
                {
                    return (T?) entity;
                }
            }
            return default;
        }
        
        public IEntity? EntityBySymbol(string symbol)
        {
            return EntityBySymbol<IEntity>(symbol);
        }
        
        public T? EntityById<T>(uint id)
            where T : IEntity
        {
            if (_typeMappings.TryGetValue(typeof(T), out var entityType))
            {
                return (T?) EntityById(entityType, id);
            }
            return default;
        }

        // ReSharper disable once UnusedMember.Global
        public IEntity? EntityById(MXId id) => EntityById(id.Type, id.RawId);

        // ReSharper disable once MemberCanBePrivate.Global
        public IEntity? EntityById(EntityType type, uint id)
        {
            var index = (int) id;
            if (!type.IsZeroBased())
            {
                if (index == 0) return null;
                index -= 1;
            }
            
            try
            {
                return type switch
                {
                    EntityType.Character => _entityContainer.Lords.ElementAt(index),
                    EntityType.RouteNode => _entityContainer.RouteNodes.ElementAt(index),
                    EntityType.Regiment => _entityContainer.Regiments.ElementAt(index),
                    EntityType.Stronghold => _entityContainer.Strongholds.ElementAt(index),
                    EntityType.Waypoint => _entityContainer.Waypoints.ElementAt(index),
                    EntityType.Thing => _entityContainer.Things.ElementAt(index),
                    EntityType.Mission => _entityContainer.Missions.ElementAt(index),
                    EntityType.Victory => _entityContainer.Victories.ElementAt(index),
                    
                    EntityType.DirectionInfo => _entityContainer.Directions.ElementAt(index),
                    EntityType.UnitInfo => _entityContainer.Units.ElementAt(index),
                    EntityType.RaceInfo => _entityContainer.Races.ElementAt(index),
                    EntityType.GenderInfo => _entityContainer.Genders.ElementAt(index),
                    EntityType.TerrainInfo => _entityContainer.Terrains.ElementAt(index),
                    EntityType.AreaInfo => _entityContainer.Areas.ElementAt(index),
                    EntityType.CommandInfo => _entityContainer.Commands.ElementAt(index),
                    _ => null
                };
            }
            catch (Exception)
            {
                return default;
            }
        }
    }

}