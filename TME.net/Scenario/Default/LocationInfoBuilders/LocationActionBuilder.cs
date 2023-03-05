using System;
using System.Linq;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.lom;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    public class LocationActionBuilder : ILocationActionBuilder
    {
        private readonly IEngine _engine;
        private readonly IEntityContainer _entityContainer;
        private readonly IEntityResolver _entityResolver;
        private readonly IVariables _variables;

        private bool _tunnel;
        private LocationInfo? _here;
        private LocationInfo? _ahead;
        private LocationInfoFlags _flags;
        private ICharacter _lord = null!;

        public LocationActionBuilder(
            IEngine engine,
            IEntityContainer entityContainer,
            IEntityResolver entityResolver,
            IVariables variables)
        {
            _engine = engine;
            _entityContainer = entityContainer;
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


        private void SetFlags(LocationInfoFlags f, bool value)
        {
            if (value)
            {
                _flags |= f;
            }
            else
            {
                _flags &= ~f;
            }
        }
        
        public LocationInfoFlags Build()
        {
            _flags = LocationInfoFlags.None;

            if (_ahead?.Owner == null)
            {
                return _flags;
            }

            _lord = _ahead.Owner;
            
            // if the character is dead then nothing else is possible
            if (_lord.IsDead())
            {
                return _flags;
            }
            
            // if the character is hidden then nothing else is possible
            if (_lord.IsHidden())
            {
                SetFlags(LocationInfoFlags.Hide, true);
                return _flags;
            }
            
            CheckTake();

            // Move Forward && Blocked
            if (_tunnel)
            {
                CheckMoveInTunnel();
            }
            else
            {
                CheckMoveBlocked();
                CheckEnterTunnel();
            }
            
            CheckEnterBattle();
            CheckCourageEnterBattle();
            CheckFollowingEnterBattle();

            CheckLocationAheadFull();

            CheckAlreadyInBattle();
            
            // LOM Specific
            
            CheckHide();

            CheckSeek();

            // if in battle there is nothing more we can do
            if (_engine.Scenario is MidnightScenario && _here?.FoeArmies > 0)
            {
                return _flags;
            }

            CheckRecruit();

            CheckPostGuard();
            
            CheckRest();
            
            CheckFight();

            CheckStubbornAttacker();

            CheckStubbornMover();
            
            // DDR specific

            CheckGiveObject();

            CheckUseObject();
            
            return _flags;
        }

        // TODO: Location Rules
        // priority
        // enabled

        private void CheckPostGuard()
        {
            // can we recruit men or guard men ?
            // Recruit:
            // 1. only if there is a stronghold here
            // 2. Is the stronghold race the same as us?
            // 3. Are we allowed an army?
            // 4. does the stronghold have more than its minimum in?
            // 5. can we take any more soldiers?

            // guard:
            // 1. only if there is a stronghold here
            // 2. Is the stronghold race the same as us?
            // 3. does the stronghold have less than its maximum in?
            // 4. do we have enough soldiers of the required type?

            // TODO this is too restrictive, the stronghold::add routine
            // allows for arbitrary values to be added, but this
            // uses the LOM model of having to place 100 at a time
            // should this change?

            throw new NotImplementedException();
        }
        
        private void CheckRecruit()
        {
            /*
             // make a list of all other recruitable
            // characters available
            // TODO in LOM this can only be one
            if ( mx->scenario->IsFeature(SF_APPROACH_DDR)  ) 
                info->FindApproachCharactersInfront(info->owner);
            else
                info->FindRecruitCharactersHere (info->owner);

            // can we recruit ?
            if ( info->objRecruit.Count() )
                info->flags.Set(lif_recruitchar); // = TRUE ;
             */
            
            throw new NotImplementedException();
        }
        
        private void CheckHide()
        {
            var allowHide = _lord.IsAllowedHide() && 
                            !_lord.HasArmy() && 
                            !_lord.IsFollowing() && 
                            !_lord.HasFollowers();
            
            // can we hide ?
            // 1. Only if we are allowed to an we have no armies
            // 2. TODO how about allowing hiding when armies
            // are below a certain number? - maybe warriors only
            SetFlags(LocationInfoFlags.Hide, allowHide);
        }

        private void CheckFight()
        {
            SetFlags(LocationInfoFlags.Fight, _here?.FightThing != ThingType.None);
        }
        
        private void CheckRest()
        {
            SetFlags(LocationInfoFlags.Rest, true);
        }
        
        private void CheckSeek()
        {
            SetFlags(LocationInfoFlags.Seek, true);
        }
        
        private void CheckAlreadyInBattle()
        {
            // we can't enter another battle while we are in one, which we must be
            // if we are in the same location as the enemy
            if (_here?.FoeArmies > 0)
            {
                SetFlags(LocationInfoFlags.EnterBattle, false);
            }
        }

        private void CheckLocationAheadFull()
        {
            // We can't enter a battle or move forward if there are too many friendlys in the location in front
            var extra = _lord.Followers ;
            if (_ahead?.FriendArmies + extra >= _variables.sv_max_armies_in_location)
            {
                SetFlags(LocationInfoFlags.EnterBattle | LocationInfoFlags.MoveForward, false);
            }
        }

        private void CheckEnterBattle()
        {
            // Move forward && battle
            // the only way to move forward when there is an army in front of you is to enter battle
            if (_ahead?.FoeArmies > 0 /* || _ahead?.FoeLords */)
            {
                if (!_variables.sv_cheat_army_no_block)
                {
                    SetFlags(LocationInfoFlags.MoveForward, false);
                }

                SetFlags(LocationInfoFlags.EnterBattle, true);
            }
        }

        private void CheckMoveBlocked()
        {
            if (_ahead != null)
            {
                if (GetTerrainInfo(_ahead.MapLoc.Terrain).IsBlock)
                {
                    SetFlags(LocationInfoFlags.MoveForward, false);
                    SetFlags(LocationInfoFlags.Blocked, true);
                }
            }
        }

        private void CheckEnterTunnel()
        {
            if (_engine.Scenario.Info.IsFeature(FeatureFlags.Tunnels)
                && _here?.MapLoc.HasTunnelEntrance == true)
            {
                SetFlags(LocationInfoFlags.EnterTunnel, true);
            }
        }

        private void CheckMoveInTunnel()
        {
            if (_engine.Scenario.Info.IsFeature(FeatureFlags.Tunnels))
            {
                // we can only move through tunnels when in a tunnel
                if (!_ahead?.MapLoc.HasTunnel == true)
                {
                    SetFlags(LocationInfoFlags.MoveForward, false);
                    SetFlags(LocationInfoFlags.Blocked, true);
                }
            }
        }

        private void CheckTake()
        {
            SetFlags(LocationInfoFlags.Take, _here?.ObjectToTake != null);
        }
        
        private void CheckGiveObject()
        {
            throw new NotImplementedException();
        }

        private void CheckUseObject()
        {
            throw new NotImplementedException();
        }
        
        private TerrainInfo GetTerrainInfo(Terrain terrain)
        {
            return _entityResolver.EntityById<TerrainInfo>(terrain.Raw())!;
        }

        private void CheckCourageEnterBattle()
        {
            if (_lord.Courage == 0 || _lord.HasTrait(LordTraits.Coward))
            {
                SetFlags(LocationInfoFlags.EnterBattle, false);
            }
        }

        private void CheckFollowingEnterBattle()
        {
            if (_lord.IsFollowing())
            {
                SetFlags(LocationInfoFlags.EnterBattle, false);
            }
        }
        
        private void CheckStubbornAttacker()
        {
            if (_here?.StubbornFollowerBattle != null)
            {
                //SetFlags(LocationInfoFlags.MoveForward, false);
            }
        }

        private void CheckStubbornMover()
        {
            if (_here?.StubbornFollowerMove != null)
            {
                SetFlags(LocationInfoFlags.MoveForward, false);
            }
        }
    }
}