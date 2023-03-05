using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class BaseLocationArmyBuilder
    {
        private readonly IEngine _engine;
        private readonly IEntityResolver _entityResolver;
        
        protected Loc CurrentLocation = Loc.Zero;
        protected bool CurrentTunnel;
        protected readonly ICharacter Doomdark;
        protected readonly ICharacter Luxor;

        protected BaseLocationArmyBuilder(
            IEngine engine,
            IEntityResolver entityResolver)
        {
            _engine = engine;
            _entityResolver = entityResolver;
            
            Doomdark = _entityResolver.EntityBySymbol<ICharacter>("CH_DOOMDARK")!;
            Luxor = _entityResolver.EntityBySymbol<ICharacter>("CH_LUXOR")!;
        }
        
        protected bool IsFriendlyTo(ICharacter lord)
        {
            return _engine.Scenario is not RevengeScenario || lord.IsFriendlyTo(Luxor);
        }

        protected bool IsStrongholdFriendly(IStronghold stronghold)
        {
            var isLoyal = stronghold.OccupyingRace != Race.Doomguard;

            if (_engine.Scenario is RevengeScenario)
            {
                isLoyal = stronghold.Loyalty == Luxor.Loyalty;
            }

            return isLoyal;
        }
        
        protected TerrainInfo GetTerrainInfo(Terrain terrain)
        {
            return _entityResolver.EntityById<TerrainInfo>(terrain.Raw())!;
        }
    }
}