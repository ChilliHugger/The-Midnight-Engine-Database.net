using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationArmyInfoBuilder : ILocationArmyInfoBuilder
    {
        private readonly IEngine _engine;
        private readonly IEntityResolver _entityResolver;
        private readonly IMap _map;
        private readonly IArmyQueryService _armyQueryService;
        private readonly IMapQueryService _mapQueryService;

        private Loc _location = Loc.Zero;
        private bool _tunnel;
        private ILord _doomdark = null!;
        private ILord _luxor = null!;

        public LocationArmyInfoBuilder(
            IEngine engine,
            IEntityResolver entityResolver,
            IMap map,
            IArmyQueryService armyQueryService,
            IMapQueryService mapQueryService)
        {
            _engine = engine;
            _entityResolver = entityResolver;
            _map = map;
            _armyQueryService = armyQueryService;
            _mapQueryService = mapQueryService;
        }

        public ILocationArmyInfoBuilder Location(Loc location)
        {
            _location = location;
            return this;
        }

        public ILocationArmyInfoBuilder Tunnel(bool tunnel)
        {
            _tunnel = tunnel;
            return this;
        }

        /// <summary>
        /// Creates a list of all armies at the current location
        /// </summary>
        /// <remarks>
        /// This function ideally needs to use some mechanism of friend or foe
        /// based on the character requesting the location info
        /// </remarks>
        public LocationArmyInfo Build()
        {
            var armies = new List<IArmy>();

            var mapLoc = _map.GetAt(_location);
            _doomdark = _entityResolver.EntityBySymbol<ILord>("CH_DOOMDARK")!;
            _luxor = _entityResolver.EntityBySymbol<ILord>("CH_LUXOR")!;

            var regiments = _mapQueryService.RegimentsAtLocation(_location, _tunnel);
            var strongholds = _tunnel
                ? new List<IStronghold>().AsReadOnly()
                : _mapQueryService.StrongholdsAtLocation(_location);

            var lords = _mapQueryService.LordsAtLocation(_location, _tunnel);

            // TODO all these need to be governed by "friend or foe"

            var strongholdAdjustment = 0u;
            var isStrongholdFriendly = false;
            var strongholdArmies = _armyQueryService.GetStrongholdArmy(strongholds, IsStrongholdFriendly).ToList();
            if (strongholdArmies.Any())
            {
                strongholdAdjustment = GetTerrainInfo(mapLoc.Terrain).Success;
                var army = strongholdArmies.First();
                isStrongholdFriendly = army.Friendly;
                armies.AddRange(strongholdArmies);
            }

            var foeTotals = new ArmyTotals
            {
                Adjustment = !isStrongholdFriendly ? strongholdAdjustment : 0,
                Lord = _doomdark,
            };

            var doomdarkTotals = new ArmyTotals
            {
                Lord = _doomdark,
            };

            var friendTotal = new ArmyTotals
            {
                Adjustment = isStrongholdFriendly ? strongholdAdjustment : 0,
                Lord = _luxor,
            };

            var regimentArmies = _armyQueryService.GetRegimentsAsArmies(regiments, _ => false).ToList();
            armies.AddRange(regimentArmies);

            foreach (var army in regimentArmies)
            {
                doomdarkTotals.Armies++;
                doomdarkTotals.Units[army.UnitType] += army.Total;
            }

            armies.AddRange(_armyQueryService.GetLordWarriorsAsArmies(lords, IsFriendlyTo));
            armies.AddRange(_armyQueryService.GetLordRidersAsArmies(lords, IsFriendlyTo));

            foreach (var army in armies)
            {
                UpdateFriendFoeArmy(army, friendTotal, foeTotals);
            }

            return new LocationArmyInfo
            {
                Location = _location,
                Tunnel = _tunnel,
                Foe = foeTotals,
                Friends = friendTotal,
                Doomdark = doomdarkTotals,
                Armies = armies,
                Strongholds = strongholds,
                Regiments = regiments
            };
        }

        private bool IsFriendlyTo(ILord lord)
        {
            return _engine.Scenario is not RevengeScenario || lord.IsFriendlyTo(_luxor);
        }

        private bool IsStrongholdFriendly(IStronghold stronghold)
        {
            var isLoyal = stronghold.OccupyingRace != Race.Doomguard;

            if (_engine.Scenario is RevengeScenario)
            {
                var ddrStronghold = stronghold as IRevengeStronghold;
                isLoyal = ddrStronghold!.Loyalty == _luxor.Loyalty;
            }

            return isLoyal;
        }

        private static void UpdateFriendFoeArmy(IArmy army, ArmyTotals friends, ArmyTotals foe)
        {
            if (army.Friendly)
            {
                friends.Armies++;
                friends.Units[army.UnitType] += army.Total;
            }
            else
            {
                foe.Armies++;
                foe.Units[army.UnitType] += army.Total;
            }
        }

        private TerrainInfo GetTerrainInfo(Terrain terrain)
        {
            return _entityResolver.EntityById<TerrainInfo>((int) terrain)!;
        }
    }
}