using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using AutoMapper;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.SpecTests.Hooks;
using TME.SpecTests.Mapping.Models;

namespace TME.SpecTests.Steps.QueryServices
{
    [Binding]
    [Scope(Feature = "Army Query Service Feature")]
    public sealed class ArmyQueryServiceStep
    {
        private readonly IMapper _mapper;
        private readonly ScenarioContext _scenarioContext;
        private readonly MainHooks _mainHooks;

        private IList<Character> _lords = new List<Character>();
        private IList<Regiment> _regiments = new List<Regiment>();
        private IList<Stronghold> _strongholds = new List<Stronghold>();
        
        private uint _countLordArmies;
        private IList<Army> _armies = new List<Army>();
        private Race _race = Race.None;
        private UnitType _unitType = UnitType.None;
        
        public ArmyQueryServiceStep(
            IMapper mapper,
            ScenarioContext scenarioContext,
            MainHooks mainHooks)
        {
            _mapper = mapper;
            _scenarioContext = scenarioContext;
            _mainHooks = mainHooks;
        }


        #region GIVEN
        
        [Given(@"there are lords with the following friend or foe status")]
        public void GivenThereAreLordsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            var items = table.Rows
                .Select(entry => new TestCharacter
                {
                    Race = FriendOrFoe(entry["status"]), 
                    Warriors = entry["warriors"] == "yes" ? 100u : 0u, 
                    Riders = entry["riders"] == "yes" ? 100u : 0u
                }).ToList();

            _lords =  _mapper.Map<List<Character>>(items);
        }
        
        [Given(@"there are regiments with the following friend or foe status")]
        public void GivenThereAreRegimentsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            var items = table.Rows
                .Select(entry => new TestRegiment
                {
                    Race = FriendOrFoe(entry["status"]), 
                    UnitType = WarriorsOrRiders(entry["type"]),
                    Total = uint.Parse(entry["total"])
                }).ToList();

            _regiments = _mapper.Map<List<Regiment>>(items);
        }
        
        [Given(@"there are strongholds with the following friend or foe status")]
        public void GivenThereAreStrongholdsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            var items = table.Rows
                .Select(entry => new TestStronghold
                {
                    Race = FriendOrFoe(entry["status"]), 
                    OccupyingRace = FriendOrFoe(entry["status"]),
                    UnitType = WarriorsOrRiders(entry["type"]),
                    Total = uint.Parse(entry["total"])
                }).ToList();

            _strongholds = _mapper.Map<List<Stronghold>>(items);
        }
        
        #endregion

        #region WHEN

        [When(@"the request to count the (friend|foe)s? of the character is made")]
        public void WhenTheRequestForFriendFoeSOfTheCharacterIsMade(string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _race = FriendOrFoe(friendOrFoe);
            _countLordArmies = sut.CountLordArmies(_lords, (lord) => lord.Race == _race);
        }
        
        [When(@"the request for warriors that are (friend|foe)s? of the character")]
        public void WhenTheRequestForWarriorsThatAreOfTheCharacter(string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _race = FriendOrFoe(friendOrFoe);
            _unitType = UnitType.Warrior;
            _armies = sut.GetLordWarriorsAsArmies(_lords, (lord) => lord.Race == Race.Moonprince).ToList();
        }
        
        [When(@"the request for riders that are (friend|foe)s? of the character")]
        public void WhenTheRequestForRidersThatAreOfTheCharacter(string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _race = FriendOrFoe(friendOrFoe);
            _unitType = UnitType.Rider;
            _armies = sut.GetLordRidersAsArmies(_lords, (lord) => lord.Race == Race.Moonprince).ToList();
        }

        [When(@"the request to count regiments that are (friend|foe)s? of the character")]
        public void ThenTheRequestForRegimentsShouldReturnFriends(string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _race = FriendOrFoe(friendOrFoe);
            _countLordArmies = sut.CountRegimentsArmies(_regiments, r => r.Race == _race);
        }

        [When(@"the request for (warrior|rider) regiments that are (friend|foe)s? of the character")]
        public void WhenTheRequestForWarriorRiderRegimentsThatAreFriendFoeSOfTheCharacter(string unit, string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _unitType = WarriorsOrRiders(unit);
            _race = FriendOrFoe(friendOrFoe);
            _armies = sut.GetRegimentsAsArmies(_regiments, r => r.LoyaltyRace == Race.Moonprince).ToList();
        }
        
        [When(@"the request to count strongholds that are (friend|foe)s? of the character")]
        public void WhenTheRequestToCountStrongholdsThatAreFriendFoeSOfTheCharacter(string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _race = FriendOrFoe(friendOrFoe);
            _countLordArmies = sut.CountStrongholdArmies(_strongholds, r => r.Race == _race);
        }

        [When(@"the request for (warrior|rider) strongholds that are (friend|foe)s? of the character")]
        public void WhenTheRequestForWarriorRiderStrongholdsThatAreFriendFoeSOfTheCharacter(string unit, string friendOrFoe)
        {
            var sut = _mainHooks.TestsContainer.Resolve<IArmyQueryService>();
            _unitType = WarriorsOrRiders(unit);
            _race = FriendOrFoe(friendOrFoe);
            _armies = sut.GetStrongholdArmy(_strongholds, r => r.LoyaltyRace == Race.Moonprince).ToList();
        }
        
        #endregion

        #region THEN

        [Then(@"there should be (\d+) armies")]
        public void ThenThereShouldBeDArmies(uint count)
        {
            _countLordArmies.Should().Be(count);
        }
        [Then(@"there should only be (\d+) armies of the correct type")]
        public void ThenThereShouldOnlyBeDArmiesOfTheCorrectType(uint count)
        {
            CheckArmies(_armies, _race, _unitType, count, true);
        }
        
        [Then(@"there should be (\d+) armies of the correct type")]
        public void ThenThereShouldBeDArmiesOfTheCorrectType(uint count)
        {
            CheckArmies(_armies, _race, _unitType, count, false);
        }

        #endregion
        
        private static void CheckArmies(IList<Army> armies, Race race, UnitType type, uint count, bool exact)
        {
            if (race == Race.Moonprince)
            {
                armies.Count(a => a.Friendly && a.UnitType == type).Should().Be((int)count);
            }
            else
            {
                armies.Count(a => !a.Friendly && a.UnitType == type).Should().Be((int)count);
            }

            if (exact)
            {
                armies.All(a => a.UnitType == type).Should().BeTrue();
            }
        }
        
        private static Race FriendOrFoe(string value)
        {
            return value.StartsWith("friend") ? Race.Moonprince : Race.Doomdark;
        }

        private static UnitType WarriorsOrRiders(string value)
        {
            return value.StartsWith("warrior") ? UnitType.Warrior: UnitType.Rider;
        }


    }
}