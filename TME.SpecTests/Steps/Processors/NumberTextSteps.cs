using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using FluentAssertions;
using TechTalk.SpecFlow;
using TME.Enums;
using TME.Processors;
using TME.SpecTests.Context;
using TME.SpecTests.Hooks;

namespace TME.SpecTests.Steps.Processors
{
    [Binding]
    public sealed class NumberTextSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MainHooks _mainHooks;
        private readonly StringsContext _stringsContext;

        private int _number;
        private ZeroMode _zeroMode = ZeroMode.No;
        private string _result = "";
        
        public NumberTextSteps(
            ScenarioContext scenarioContext, 
            MainHooks mainHooks,
            StringsContext stringsContext)
        {
            _scenarioContext = scenarioContext;
            _mainHooks = mainHooks;
            _stringsContext = stringsContext;
        }


        [Given(@"the number is (\d+)")]
        public void GivenTheNumberIs(int number)
        {
            _number = number;
        }

        [Given(@"the zero mode is '(.*)'")]
        public void GivenTheZeroModeIs(ZeroMode zeroMode)
        {
            _zeroMode = zeroMode;
        }

        [When(@"the number is displayed")]
        public void WhenTheNumberIsDisplayed()
        {
            var processor = _mainHooks.Container.Resolve<INumberText>();
            _result = processor.Describe(_number, _zeroMode);
            
        }

        [Then(@"the result should be '(.*)'")]
        public void ThenTheResultShouldBe(string result)
        {
            _result.Should().Be(result);
        }
    }
}