using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using FluentAssertions;
using TechTalk.SpecFlow;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.SpecTests.Hooks;

namespace TME.SpecTests.Steps.QueryServices
{
    [Binding]
    [Scope(Feature = "Army Query Service Feature")]
    public sealed class ArmyQueryServiceStep
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MainHooks _mainHooks;

        private readonly IList<ICharacter> _lords = new List<ICharacter>();
        private readonly IList<IRegiment> _regiments = new List<IRegiment>();
        private readonly IList<IStronghold> _strongholds = new List<IStronghold>();
        
        private uint _countLordArmies;
        private IList<IArmy> _armies = new List<IArmy>();
        private Race _race = Race.None;
        private UnitType _unitType = UnitType.None;
        
        public ArmyQueryServiceStep(
            ScenarioContext scenarioContext,
            MainHooks mainHooks)
        {
            _scenarioContext = scenarioContext;
            _mainHooks = mainHooks;
        }


        #region GIVEN
        
        [Given(@"there are lords with the following friend or foe status")]
        public void GivenThereAreLordsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            foreach (var entry in table.Rows)
            {
                var item = _mainHooks.Container.Resolve<ICharacter>();
                if (item is Lord lord)
                {
                    lord.Race = FriendOrFoe(entry["status"]);

                    if (lord.Units[0] is IUnitInternal warriors)
                    {
                        warriors.SetTotal(entry["warriors"] == "yes" ? 100u : 0u);
                    }
                    if (lord.Units[1] is IUnitInternal riders)
                    {
                        riders.SetTotal(entry["riders"] == "yes" ? 100u : 0u);
                    }
                }
                _lords.Add(item);
            }
        }
        
        [Given(@"there are regiments with the following friend or foe status")]
        public void GivenThereAreRegimentsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            foreach (var entry in table.Rows)
            {
                var item = _mainHooks.Container.Resolve<IRegiment>();
                if (item is Regiment regiment)
                {
                    regiment.Race = FriendOrFoe(entry["status"]);
                    regiment.UnitType = WarriorsOrRiders(entry["type"]);
                    regiment.Total = uint.Parse(entry["total"]);
                }
                _regiments.Add(item);
            }
        }
        
        [Given(@"there are strongholds with the following friend or foe status")]
        public void GivenThereAreStrongholdsWithTheFollowingFriendOrFoeStatus(Table table)
        {
            foreach (var entry in table.Rows)
            {
                var item = _mainHooks.Container.Resolve<IStronghold>();
                if (item is Stronghold stronghold)
                {
                    stronghold.Race = FriendOrFoe(entry["status"]);
                    stronghold.OccupyingRace = stronghold.Race;
                    stronghold.UnitType = WarriorsOrRiders(entry["type"]);
                    stronghold.Total = uint.Parse(entry["total"]);
                }
                _strongholds.Add(item);
            }
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
        
        private static void CheckArmies(IList<IArmy> armies, Race race, UnitType type, uint count, bool exact)
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