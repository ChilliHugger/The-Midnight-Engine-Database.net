using System;
using System.Collections.Generic;
using System.Linq;
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
            { typeof(ILord), EntityType.Character },
            { typeof(IRevengeLord), EntityType.Character },
            { typeof(IRegiment), EntityType.Regiment },
            { typeof(IRouteNode), EntityType.RouteNode },
            { typeof(IStronghold), EntityType.Stronghold },   
            { typeof(IRevengeStronghold), EntityType.Stronghold },
            { typeof(IWaypoint), EntityType.Waypoint },
            { typeof(IThing), EntityType.Thing },
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
            if(_entityContainer.SymbolCache.TryGetValue( symbol, out var entity))
            {
                return (T?)entity;
            }
            return default;
        }
        
        public T? EntityById<T>(int id)
            where T : IEntity
        {
            if (_typeMappings.TryGetValue(typeof(T), out var entityType))
            {
                return (T?) EntityById(entityType, id);
            }
            return default;
        }

        // ReSharper disable once UnusedMember.Global
        public object? EntityById(MXId id) => EntityById(id.Type, id.RawId);

        // ReSharper disable once MemberCanBePrivate.Global
        public object? EntityById(EntityType type, int index)
        {
            if (index == 0) return null;
            try
            {

                var id = index - 1;
                return type switch
                {
                    EntityType.Character => _entityContainer.Lords.ElementAt(id),
                    EntityType.RouteNode => _entityContainer.RouteNodes.ElementAt(id),
                    EntityType.Regiment => _entityContainer.Regiments.ElementAt(id),
                    EntityType.Stronghold => _entityContainer.Strongholds.ElementAt(id),
                    EntityType.Waypoint => _entityContainer.Waypoints.ElementAt(id),
                    EntityType.Thing => _entityContainer.Things.ElementAt(id),
                    EntityType.Mission => _entityContainer.Missions.ElementAt(id),
                    EntityType.Victory => _entityContainer.Victories.ElementAt(id),
                    EntityType.DirectionInfo => _entityContainer.Directions.ElementAt(id),
                    EntityType.UnitInfo => _entityContainer.Units.ElementAt(id),
                    EntityType.RaceInfo => _entityContainer.Races.ElementAt(id),
                    EntityType.GenderInfo => _entityContainer.Genders.ElementAt(id),
                    EntityType.TerrainInfo => _entityContainer.Terrains.ElementAt(id),
                    EntityType.AreaInfo => _entityContainer.Areas.ElementAt(id),
                    EntityType.CommandInfo => _entityContainer.Commands.ElementAt(id),
                    _ => null
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}