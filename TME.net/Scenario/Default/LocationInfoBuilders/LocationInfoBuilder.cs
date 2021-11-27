using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TME.Extensions;
using TME.Interfaces;
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
        private readonly ILocationArmyCountInfoBuilder _locationArmyCountInfoBuilder;
        private readonly IMapQueryService _mapQueryService;
        
        private Loc _location = Loc.Zero;
        private Direction _direction = Enums.Direction.None;
        private ICharacter? _lord;
        private bool _tunnel;

        public LocationInfoBuilder(
            ILocationArmyCountInfoBuilder locationArmyCountInfoBuilder,
            IMapQueryService mapQueryService,
            IEngine engine,
            IEntityResolver entityResolver,
            IEntityContainer entityContainer,
            IMap map,
            IVariables variables)
        {
            _locationArmyCountInfoBuilder = locationArmyCountInfoBuilder;
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
        
        public ILocationInfoBuilder Lord(ICharacter lord)
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

            //
            var objectToTake = _engine.Scenario.Info.IsFeature(FeatureFlags.Take) && mapLoc.HasObject && !_tunnel
                ? FindObjectAtLocation(_location)
                : null;

            // fight
            var thingToFight = (mapLoc.HasTunnelPassage && !_tunnel) 
                ? ThingType.None
                : mapLoc.Thing;

            var fightThing = CheckFightThing(thingToFight);
            
            // calculate number of friends and foe armies
            var countInfo = _locationArmyCountInfoBuilder
                .Location(_location)
                .Tunnel(_tunnel)
                .Build();

            // Calculate Ice Fear 
            if (_lord != null)
            {
                // TODO: Calc Ice Fear
                //fear = info->adj_fear ;
                //s32 temp  = cowardess-(fear/7);
                //temp = std::max<int>( temp, 0 ) / 8 ;
                //courage = std::min<int>( temp, 7 ) ;
            }
            
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
                //OwnerCourage = 0,
                FightThing = fightThing,
                FriendArmies = countInfo.Friends,
                FoeArmies = countInfo.Foes,
                FearAdjuster = 0,
                MoralAdjuster = 0,
                StubbornFollowerBattle = CheckStubbornFollowerBattle(),
                StubbornFollowerMove = CheckStubbornMove(),
                // the engine stores last nights adj_stronghold
                // the scenario holds current, thus we get from the variables
                StrongholdAdjuster = _variables.sv_stronghold_adjuster
            };
        }

        private ICharacter? CheckStubbornFollowerBattle()
        {
            return _lord?.HasFollowers == true
                ? _entityContainer.Lords.FirstOrDefault(l => l.Following == _lord && l.IsCoward)
                : null;
        }
        
        private ICharacter? CheckStubbornMove()
        {
            return _lord?.HasFollowers == true
                ? _entityContainer.Lords.FirstOrDefault(l => l.Following == _lord && !l.CanWalkForward)
                : null;
        }

        private ThingType CheckFightThing(ThingType thingType)
        {
            if (thingType == ThingType.None)
            {
                return thingType;
            }
            var thing = _entityResolver.EntityById<IObject>((int)thingType)!;
            return thing.CanFight 
                ? thingType 
                : ThingType.None ;
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
        
        private IObject? FindObjectAtLocation(Loc location)
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