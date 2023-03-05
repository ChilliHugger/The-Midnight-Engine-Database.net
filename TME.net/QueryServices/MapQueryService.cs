using System.Collections.Generic;
using System.Linq;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.QueryServices
{
    public class MapQueryService : IMapQueryService
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IMap _map;

        public MapQueryService(
            IMap map,
            IEntityContainer entityContainer)
        {
            _map = map;
            _entityContainer = entityContainer;
        }
        
        public IReadOnlyList<ICharacter> LordsAtLocation(Loc location, bool inTunnel)
        {
            return _entityContainer
                .Lords
                .Where(l => l.IsAlive() &&
                            l.IsInTunnel() == inTunnel &&
                            !l.IsHidden() &&
                            l.Location == location).ToList();
        }
        
        public IReadOnlyList<ICharacter> NotRecruitedLordsAtLocation(Loc location, bool inTunnel)
        {
            return _entityContainer
                .Lords
                .Where(l => l.IsAlive() &&
                            !l.IsRecruited() &&
                            l.IsInTunnel() == inTunnel &&
                            !l.IsHidden() &&
                            l.Location == location).ToList();
        }

        public IReadOnlyList<ICharacter> RecruitedLordsAtLocation(Loc location, bool inTunnel)
        {
            return _entityContainer
                .Lords
                .Where(l => l.IsAlive() &&
                            l.IsRecruited() &&
                            l.IsInTunnel() == inTunnel &&
                            !l.IsHidden() &&
                            l.Location == location).ToList();
        }
        
        public IReadOnlyList<IRouteNode> RouteNodesAtLocation(Loc location)
        {
            if (!_map.GetAt(location).IsRouteNode)
            {
                return new List<IRouteNode>().AsReadOnly();
            }

            return _entityContainer
                .RouteNodes
                .Where(r => r.Location == location)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<IRegiment> RegimentsAtLocation(Loc location, bool inTunnel)
        {
            return _entityContainer
                .Regiments
                .Where(r => r.Location == location && r.IsInTunnel == inTunnel)
                .ToList()
                .AsReadOnly();
        }
        
        public IReadOnlyList<IStronghold> StrongholdsAtLocation(Loc location)
        {
            return _entityContainer
                .Strongholds
                .Where(s => s.Location == location)
                .ToList()
                .AsReadOnly();
        }
        
    }
}