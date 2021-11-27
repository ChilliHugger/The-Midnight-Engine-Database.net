using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationArmyInfoBuilder : BaseLocationArmyBuilder, ILocationArmyInfoBuilder
    {
        private readonly IMap _map;
        private readonly IArmyQueryService _armyQueryService;
        private readonly IMapQueryService _mapQueryService;
        
        public LocationArmyInfoBuilder(
            IEngine engine,
            IEntityResolver entityResolver,
            IMap map,
            IArmyQueryService armyQueryService,
            IMapQueryService mapQueryService) : base(engine,entityResolver)
        {
            _map = map;
            _armyQueryService = armyQueryService;
            _mapQueryService = mapQueryService;
        }

        public ILocationArmyInfoBuilder Location(Loc location)
        {
            CurrentLocation = location;
            return this;
        }

        public ILocationArmyInfoBuilder Tunnel(bool tunnel)
        {
            CurrentTunnel = tunnel;
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

            var mapLoc = _map.GetAt(CurrentLocation);
            
            var regiments = _mapQueryService.RegimentsAtLocation(CurrentLocation, CurrentTunnel);
            var strongholds = CurrentTunnel
                ? new List<IStronghold>().AsReadOnly()
                : _mapQueryService.StrongholdsAtLocation(CurrentLocation);

            var lords = _mapQueryService.LordsAtLocation(CurrentLocation, CurrentTunnel);

            // TODO all these need to be governed by proper "friend or foe"

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
                Lord = Doomdark,
            };

            var doomdarkTotals = new ArmyTotals
            {
                Lord = Doomdark,
            };

            var friendTotal = new ArmyTotals
            {
                Adjustment = isStrongholdFriendly ? strongholdAdjustment : 0,
                Lord = Luxor,
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
                Location = CurrentLocation,
                Tunnel = CurrentTunnel,
                Foe = foeTotals,
                Friends = friendTotal,
                Doomdark = doomdarkTotals,
                Armies = armies,
                Strongholds = strongholds,
                Regiments = regiments
            };
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


    }
}