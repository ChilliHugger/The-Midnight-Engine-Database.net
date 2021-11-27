using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationActionBuilder : ILocationActionBuilder
    {
        private readonly IEngine _engine;
        private readonly IEntityResolver _entityResolver;
        private readonly IVariables _variables;

        private bool _tunnel;
        private LocationInfo? _here;
        private LocationInfo? _ahead;

        public LocationActionBuilder(
            IEngine engine,
            IEntityResolver entityResolver,
            IVariables variables)
        {
            _engine = engine;
            _entityResolver = entityResolver;
            _variables = variables;
        }
        
        public ILocationActionBuilder Tunnel(bool tunnel)
        {
            _tunnel = tunnel;
            return this;
        }

        public ILocationActionBuilder Here(LocationInfo locationInfo)
        {
            _here = locationInfo;
            return this;
        }

        public ILocationActionBuilder Ahead(LocationInfo locationInfo)
        {
            _ahead = locationInfo;
            return this;
        }

        public LocationInfoFlags Build()
        {
            var flags = LocationInfoFlags.None;

            void SetFlags(LocationInfoFlags f, bool value)
            {
                if (value)
                {
                    flags |= f;
                }
                else
                {
                    flags &= ~f;
                }
            }

            // Take
            SetFlags(LocationInfoFlags.Take, _here?.ObjectToTake != null);

            // Blocked
            SetFlags(LocationInfoFlags.Blocked, false);

            // Move Forward && Blocked
            if (_engine.Scenario.Info.IsFeature(FeatureFlags.Tunnels) && _tunnel)
            {
                // we can only move through tunnels when in a tunnel
                if (!_ahead?.MapLoc.HasTunnel == true)
                {
                    SetFlags(LocationInfoFlags.MoveForward, false);
                    SetFlags(LocationInfoFlags.Blocked, true);
                }
            }
            else
            {
                if (_ahead != null)
                {
                    if (GetTerrainInfo(_ahead.MapLoc.Terrain).IsBlock)
                    {
                        SetFlags(LocationInfoFlags.MoveForward, false);
                        SetFlags(LocationInfoFlags.Blocked, true);
                    }
                }

                if (_engine.Scenario.Info.IsFeature(FeatureFlags.Tunnels)
                    && _here?.MapLoc.HasTunnelEntrance == true)
                {
                    SetFlags(LocationInfoFlags.EnterTunnel, true);
                }
            }

            // TODO: Cheap here / ahead armies
            // Owner of this info

            // Move forward && battle

            // the only way to move forward when there is an army in front of you
            // is to enter battle
            if (_ahead?.FoeArmies > 0)
            {
                if (!_variables.sv_cheat_army_no_block)
                {
                    SetFlags(LocationInfoFlags.MoveForward, false);
                }

                SetFlags(LocationInfoFlags.EnterBattle, false);
            }

            // but if there are too many people there, then we can't
            var extra = _here?.Owner?.Followers ?? 0;
            if (_ahead?.FriendArmies + extra >= _variables.sv_max_armies_in_location)
            {
                SetFlags(LocationInfoFlags.EnterBattle | LocationInfoFlags.MoveForward, false);
            }

            // we can't enter another battle while we are in one, which we must be
            // if we are in the same location as the enemy
            if (_here?.FoeArmies > 0)
            {
                SetFlags(LocationInfoFlags.EnterBattle, false);
            }

            return flags;
        }

        private TerrainInfo GetTerrainInfo(Terrain terrain)
        {
            return _entityResolver.EntityById<TerrainInfo>((int) terrain)!;
        }
    }
}