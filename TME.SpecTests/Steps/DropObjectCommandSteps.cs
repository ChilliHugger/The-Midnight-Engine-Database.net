using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.SpecTests.Context;
using TME.SpecTests.Drivers;
using TME.SpecTests.Hooks;
using TME.Types;

namespace TME.SpecTests.Steps
{
    [Binding]
    [Scope(Feature = "Drop Object Command")]
    public sealed class DropObjectCommandSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ActionsContext _actionsContext;
        private readonly VariablesContext _variablesContext;
        private readonly CommandHistoryContext _commandHistoryContext;
        private readonly ActionDriver _actionDriver;
        
        // Step Context
        private Time _time = Time.None;

        public DropObjectCommandSteps(
            ActionsContext actionsContext,
            VariablesContext variablesContext,
            CommandHistoryContext commandHistoryContext,
            ActionDriver actionDriver,
            MainHooks mainHooks,
            ScenarioContext scenarioContext)
        {
            _actionsContext = actionsContext;
            _variablesContext = variablesContext;
            _commandHistoryContext = commandHistoryContext;
            _actionDriver = actionDriver;
            _scenarioContext = scenarioContext;
        }

        #region GIVEN
        [Given(@"it is Dawn")]
        public void GivenItIsDawn()
        {
            _time = _variablesContext.sv_time_dawn;
        }
        
        [Given(@"a lord is carrying an object")]
        public void GivenALordIsCarryingAnObject()
        {
            SetupLordAndObject();
        }

        [Given(@"(\d+) hours:? of the day remain")]
        public void GivenDHoursOfTheDayRemain(uint hours)
        {
            _time = _variablesContext.sv_time_night + (hours*_variablesContext.sv_time_scale);
        }
        
        [Given(@"the ObjectDropped action will return (success|failure)")]
        public void GivenTheObjectDroppedActionWillReturnSuccess(IResult result)
        {
            _actionsContext.ObjectDroppedActionIsMocked = true;
            _actionsContext.ObjectDroppedActionResult = result;
        }
        #endregion

        #region WHEN
        [When(@"the lord drops the object")]
        [When(@"the lord tries to drop their object")]
        public async Task WhenTheLordDropsTheObject()
        {
            var (lord,thing) = GetLordAndObject();
            
            SetupLordTime(lord);

            var result = await _actionDriver.ExecuteObjectDropped(lord, thing);
            _scenarioContext["result"] = result;
        }
        
        [When(@"the lord tries to drop an object they are not carrying")]
        public async Task WhenTheLordDropsAnObjectTheyAreNotCarrying()
        {
            var (lord,thing) = GetLordAndObject();
            
            RemoveObjectFromLord(lord,thing);   

            var result = await _actionDriver.ExecuteObjectDropped(lord, thing);
            _scenarioContext["result"] = result;
        }
        #endregion

        #region THEN
        [Then(@"the ObjectDropped action should be run")]
        public void ThenTheObjectDroppedActionShouldBeRun()
        {
            var (_,thing) = GetLordAndObject();

             _actionDriver
                 .ObjectDroppedMock
                 .Verify( m => m.Execute( It.IsAny<object[]>()), Times.Once() );
             
             Assert.AreEqual(thing.RawId, _actionsContext.ObjectDropped?.RawId);
        }
        
        [Then(@"they (should|should not) be able to drop the object")]
        [Then(@"the lord (should|should not) be able to drop the object")]
        public async Task ThenTheLordShouldShouldNotBeAbleToDropTheObject(bool shouldShouldNot)
        {
            var (lord,thing) = GetLordAndObject();
            
            SetupLordTime(lord);

            var result = await _actionDriver.ExecuteObjectDropped(lord, thing);
            CheckResult(shouldShouldNot, result);
        }

        [Then(@"the lord (should|should not) drop the object")]
        [Then(@"they (should|should not) drop the object")]
        public void ThenTheLordShouldShouldNotDropTheObject(bool shouldShouldNot)
        {
            var result = _scenarioContext["result"];
            CheckResult(shouldShouldNot, result);
        }
        
        [Then(@"the '(.*)' command should be saved to the history")]
        public void ThenTheCommandShouldBeSavedToTheHistory(Command command)
        {
            Assert.AreEqual(command, _commandHistoryContext.Items.First().Command, "Command History Stored");
        }

        [Then(@"the lord's time should not change")]
        [Then(@"their time should not change")]
        public void ThenTheLordsTimeShouldNotChange()
        {
            var (lord,_) = GetLordAndObject();
            Assert.AreEqual( _time, lord.Time);
        }

        [Then(@"the lord should no longer be carrying the object")]
        [Then(@"they should no longer be carrying the object")]
        public void ThenTheLordShouldNoLongerBeCarryingTheObject()
        {
            var (lord,thing) = GetLordAndObject();
            Assert.False(lord.Carrying.Contains(thing), "Lord does not carry object");
        }
        #endregion
        
        private void SetupLordTime(ICharacter lord)
        {
            if (lord is ICharacterInternal internalLord)
            {
                internalLord.UpdateTime(_time);
            }
        }
        
        private void RemoveObjectFromLord(ICharacter lord, IObject thing)
        {
            if (lord is ICharacterInternal internalLord)
            {
                internalLord.RemoveCarriedObject(thing);
            }
        }
        
        private void SetupLordAndObject()
        {
            var (lord,thing) = _actionDriver.GivenALordIsCarryingAnObjectOfType(ThingType.CupOfDreams);
            _scenarioContext["currentLord"] = lord;
            _scenarioContext["currentThing"] = thing;
        }
        
        private (ICharacter, IObject) GetLordAndObject()
        {
            var lord = _scenarioContext["currentLord"] as ICharacter;
            var thing = _scenarioContext["currentThing"] as IObject;
            return (lord!, thing!);
        }
        
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void CheckResult(bool shouldShouldNot, object result)
        {
            if (shouldShouldNot)
            {
                Assert.True(result is Success, "Command Successful");
            }
            else
            {
                Assert.True(result is Failure, "Command Not Successful");
            }
        }
        
        [StepArgumentTransformation(@"(success|failure)")]
        public IResult SuccessFailureTransform(string value)
        {
            return value == "success" 
                ? Success.Default 
                : Failure.Default;
        }
        
        [StepArgumentTransformation]
        public Command CommandTransform(string value)
        {
            if (!Enum.TryParse(value.Replace(" ", ""), true, out Command command))
            {
                throw new ArgumentException($"Could not get Command from '{value}'", nameof(value));
            }
            return command;
        }
        
        
    }
}