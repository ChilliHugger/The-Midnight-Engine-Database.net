using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationLordInfoBuilder : ILocationLordInfoBuilder
    {
        private readonly IEngine _engine;
        private readonly IMapQueryService _mapQueryService;

        private Loc _location = Loc.Zero;
        private ICharacter? _lord;
        private bool _tunnel;
        
        public LocationLordInfoBuilder(
            IEngine engine,
            IMapQueryService mapQueryService)
        {
            _engine = engine;
            _mapQueryService = mapQueryService;
        }
        
        public ILocationLordInfoBuilder Location(Loc location)
        {
            _location = location;
            return this;
        }
        
        public ILocationLordInfoBuilder Lord(ICharacter lord)
        {
            _lord = lord;
            return this;
        }
        
        public ILocationLordInfoBuilder Tunnel(bool tunnel)
        {
            _tunnel = tunnel;
            return this;
        }
        
        public LocationLordInfo Build()
        {
            //IThing? moonring;
            //ILord? moonringCarrier;
            
            // if (_engine.Scenario.Info.IsFeature(FeatureFlags.MoonRing))
            // {
            //     moonring = _entityResolver.EntityBySymbol<IThing>("OB_MOONRING");
            //     moonringCarrier = moonring != null
            //         ? WhoHasObject(moonring)
            //         : null;
            // }
            
            var recruitedLords = _mapQueryService.RecruitedLordsAtLocation(_location, _tunnel);
           
            var notRecruitedLords = _mapQueryService.NotRecruitedLordsAtLocation(_location, _tunnel);

            return new LocationLordInfo
            {
                Location = _location,
                Tunnel = _tunnel,
                Lords = recruitedLords.Concat(notRecruitedLords).ToList().AsReadOnly(),
                UnRecruited = notRecruitedLords.ToList().AsReadOnly(),
                Recruited = recruitedLords.ToList().AsReadOnly(),
                Recruitable = GetRecruitableLords(notRecruitedLords)
            };
        }
        
        private IReadOnlyList<ICharacter> GetRecruitableLords(IEnumerable<ICharacter> lords)
        {
            return _lord != null
                ? lords.Where(l => CanRecruitLord(_lord, l)).ToList().AsReadOnly()
                : new List<ICharacter>().AsReadOnly();
        }
        
        // TODO: Scenario check
        private bool CanRecruitLord(ICharacter recruiter, ICharacter lord)
        {
            if (_engine.Scenario.Info.IsFeature(FeatureFlags.Approach))
            {
                return true;
            }
            return recruiter.WillRecruitSucceed(lord);
        }
    }
}