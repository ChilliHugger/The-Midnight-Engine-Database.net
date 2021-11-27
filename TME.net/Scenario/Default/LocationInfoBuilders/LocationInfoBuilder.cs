using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
 
    public class LocationInfoBuilder : ILocationInfoBuilder
    {
        private readonly IMap _map;
        private readonly IVariables _variables;
        private readonly IEntityContainer _entityContainer;
        private readonly IEntityResolver _entityResolver;
        private readonly IEngine _engine;
        private readonly IMapQueryService _mapQueryService;
        
        private Loc _location = Loc.Zero;
        private Direction _direction = Enums.Direction.None;
        private ILord? _lord;
        private bool _tunnel;

        public LocationInfoBuilder(
            IMapQueryService mapQueryService,
            IEngine engine,
            IEntityResolver entityResolver,
            IEntityContainer entityContainer,
            IMap map,
            IVariables variables)
        {
            _mapQueryService = mapQueryService;
            _engine = engine;
            _entityResolver = entityResolver;
            _entityContainer = entityContainer;
            _map = map;
            _variables = variables;
        }
        
        public ILocationInfoBuilder Location(Loc location)
        {
            _location = location;
            return this;
        }
        
        public ILocationInfoBuilder Direction(Direction direction)
        {
            _direction = direction;
            return this;
        }
        
        public ILocationInfoBuilder Lord(ILord lord)
        {
            _lord = lord;
            return this;
        }
        
        public ILocationInfoBuilder Tunnel(bool tunnel)
        {
            _tunnel = tunnel;
            return this;
        }
        
        public LocationInfo Build()
        {
            var mapLoc = _map.GetAt(_location);
            
            // Route
            var routenodes = !_tunnel
                ? _mapQueryService.RouteNodesAtLocation(_location)
                : new List<IRouteNode>().AsReadOnly();

            var objectToTake = _engine.Scenario is RevengeScenario && mapLoc.HasObject && !_tunnel
                ? FindObjectAtLocation(_location)
                : null;
 
            return new LocationInfo()
            {
                Location = _location,
                Tunnel = _tunnel,
                LocationInFront = _location + _direction,
                Looking = _direction,
                LocationLookingAt = FindLookingTowards(_location, _direction),
                MapLoc = mapLoc,
                ObjectToTake = objectToTake,
                Routenodes = routenodes,
                Owner = _lord,
                
                // the engine stores last nights adj_stronghold
                // the scenario holds current, thus we get from the variables
                StrongholdAdjuster = _variables.sv_stronghold_adjuster
            };
        }
        
        private TerrainInfo GetTerrainInfo(Terrain terrain)
        {
            return _entityResolver.EntityById<TerrainInfo>((int)terrain)!;
        }

        private Loc FindLookingTowards(Loc location, Direction direction)
        {
            for (var ii = 0; ii < _variables.sv_look_forward_distance; ii++)
            {
                location += direction;
                var mapLoc = _map.GetAt(location);
                
                if(GetTerrainInfo(mapLoc.Terrain).IsInteresting)
                {
                    return location;
                }

                if (_engine.Scenario.Info.IsFeature(FeatureFlags.Mist) 
                    && mapLoc.IsMisty)
                {
                    return location;
                }
            }

            return location;
        }
        
        private IThing? FindObjectAtLocation(Loc location)
        {
            return _entityContainer.Things
                    .FirstOrDefault(t => !t.IsCarried && t.Location == location);
        }
        

        // private static ILord? WhoHasObject(IThing thing)
        // {
        //     return thing.IsCarried 
        //         ? thing.CarriedBy as ILord 
        //         : null;
        // }
    }
}