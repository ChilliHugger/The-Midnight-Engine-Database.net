using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Scenario.Default.Scenario.Actions;
using TME.SpecTests.Context;
using TME.SpecTests.Drivers;
using TME.SpecTests.Hooks;

namespace TME.SpecTests.Steps
{
    [Binding]
    [Scope(Feature = "Object Dropped Action")]
    public sealed class ObjectDroppedActionSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly MainHooks _mainHooks;
        private readonly ActionDriver _actionDriver;
        private readonly MapContext _mapContext;
        
        public ObjectDroppedActionSteps(
            MainHooks mainHooks,
            MapContext mapContext,
            ActionDriver actionDriver,
            ScenarioContext scenarioContext)
        {
            _mapContext = mapContext;
            _actionDriver = actionDriver;
            _mainHooks = mainHooks;
            _scenarioContext = scenarioContext;
        }

        #region GIVEN
        [Given(@"a non unique object is being carried")]
        public void GivenANonUniqueObjectIsBeingCarried() => 
            SetupLordAndObject(ThingType.Blood, false);

        [Given(@"a unique object is being carried")]
        public void GivenAUniqueObjectIsBeingCarried() => 
            SetupLordAndObject(ThingType.Moonring, true);
        #endregion

        #region When
        [When(@"the object is dropped")]
        public async Task WhenTheObjectIsDropped()
        {
            var thing = GetThing();
            var result = await ExecuteObjectDropped(thing);
            _scenarioContext["result"] = result;
        }
        #endregion

        #region THEN
        [Then(@"action will complete successfully")]
        public void ThenActionWillCompleteSuccessfully()
        {
            var result = _scenarioContext["result"] as IResult;
            Assert.True(result is Success, "Command Successful");
        }
        
        [Then(@"the object (should|should not) be at the current location")]
        public void ThenTheObjectShouldShouldNotBeAtTheCurrentLocation(bool shouldShouldNot)
        {
            var thing = GetThing();
            var loc = _mapContext.CurrentLocation;
            Assert.AreEqual((ThingType)thing.RawId, loc.Thing, "Location has correct object type");
            
            if (thing.IsUnique)
            {
                Assert.True(loc.HasObject, "Location has object");
            }
        }
        #endregion
        
        private void SetupLordAndObject(ThingType thingType, bool unique)
        {
            var (lord,thing) = _actionDriver.GivenALordIsCarryingAnObjectOfType(thingType, unique);
            _scenarioContext["currentLord"] = lord;
            _scenarioContext["currentThing"] = thing;
        }

        private IThing GetThing()
        {
            var thing = _scenarioContext["currentThing"] as IThing;
            return thing!;
        }

        [Given(@"an object is not being carried")]
        public void GivenAnObjectIsNotBeingCarried()
        {
            var thing = _actionDriver.GivenAnObjectOfType(ThingType.Moonring);
            _scenarioContext["currentThing"] = thing;
        }

        [Then(@"the object should not be able to be dropped")]
        public async Task ThenTheObjectShouldNotBeAbleToBeDropped()
        {
            var thing = GetThing();
            var result = await ExecuteObjectDropped(thing);
            Assert.True(result is Failure, "Command Not Successful");
        }
        
        private async Task<IResult> ExecuteObjectDropped(IThing thing)
        {
            var sut = _mainHooks.Container.ResolveKeyed<IAction>(nameof(ObjectDropped));
            return await sut.Execute(thing);
        }

    }
}