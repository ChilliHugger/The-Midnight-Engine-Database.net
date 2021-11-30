using System.Collections.Generic;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationArmyCountInfoBuilder : BaseLocationArmyBuilder, ILocationArmyCountInfoBuilder
    {
        private readonly IArmyQueryService _armyQueryService;
        private readonly IMapQueryService _mapQueryService;
        
        public LocationArmyCountInfoBuilder(
            IEngine engine,
            IEntityResolver entityResolver,
            IArmyQueryService armyQueryService,
            IMapQueryService mapQueryService) : base(engine,entityResolver)
        {
            _armyQueryService = armyQueryService;
            _mapQueryService = mapQueryService;
        }

        public ILocationArmyCountInfoBuilder Location(Loc location)
        {
            CurrentLocation = location;
            return this;
        }

        public ILocationArmyCountInfoBuilder Tunnel(bool tunnel)
        {
            CurrentTunnel = tunnel;
            return this;
        }

        public LocationArmyCountInfo Build()
        {
            var regiments = _mapQueryService.RegimentsAtLocation(CurrentLocation, CurrentTunnel);
            
            var strongholds = CurrentTunnel
                ? new List<IStronghold>().AsReadOnly()
                : _mapQueryService.StrongholdsAtLocation(CurrentLocation);

            var lords = _mapQueryService.LordsAtLocation(CurrentLocation, CurrentTunnel);
            
            var friendlyStrongholds = _armyQueryService.CountStrongholdArmies(strongholds, IsStrongholdFriendly);
            var foeStrongholds = strongholds.Count - friendlyStrongholds;

            var foeRegiments = _armyQueryService.CountRegimentsArmies(regiments, _ => false);
            var friendlyRegiments = regiments.Count - foeRegiments;
            
            var friendlyLords = _armyQueryService.CountLordArmies(lords, IsFriendlyTo);
            var foeLords = lords.Count - friendlyLords;
            // TODO: Check this count with Original LoM
            // ie: does a lord without an army count as an army?
            // and does a lord with warriors and riders count as 2

            return new LocationArmyCountInfo
            {
                Location = CurrentLocation,
                Tunnel = CurrentTunnel,
                Foes = (uint)(foeLords + foeRegiments + foeStrongholds),
                Friends = (uint)(friendlyLords + friendlyRegiments + friendlyStrongholds),
                Doomdark = 0
            };
        }
    }
}