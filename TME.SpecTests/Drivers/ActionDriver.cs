using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Extensions;
using TME.Scenario.Default.Commands;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.SpecTests.Context;
using TME.SpecTests.Hooks;
using TME.SpecTests.Mocks;
using TME.Types;
using Thing = TME.Scenario.Default.Items.Thing;

namespace TME.SpecTests.Drivers
{
    [Binding]
    public class ActionDriver
    {
        private readonly MainHooks _mainHooks;
        private readonly ActionMockBuilder _actionMockBuilder;
        private readonly ActionsContext _actionsContext;

        public Mock<IAction> ObjectDroppedMock { get; private set; } = null!;

        public ActionDriver(
            ActionsContext actionsContext,
            ActionMockBuilder actionMockBuilder,
            MainHooks mainHooks)
        {
            _actionsContext = actionsContext;
            _actionMockBuilder = actionMockBuilder;
            _mainHooks = mainHooks;
        }
        
        public (ILord lord, IThing thing) GivenALordIsCarryingAnObjectOfType(ThingType thingType, bool unique = false)
        {
            var lord = _mainHooks.Container.Resolve<ILord>();
            var thing = GivenAnObjectOfType(thingType,unique);

            if (lord is ILordInternal internalLord)
            {
                internalLord.SetCarrying(new List<IThing>{thing});
            }

            if (thing is IThingInternal internalThing)
            {
                internalThing.UpdateCarriedBy(lord);
            }

            return (lord, thing);
        }

        public IThing GivenAnObjectOfType(ThingType thingType, bool unique = false)
        {
            var thing = _mainHooks.Container.Resolve<IThing>();
            
            if (thing is Thing item)
            {
                item.Id = new MXId(EntityType.Thing,(uint)thingType);
                item.SetFlags(ThingFlags.Unique, unique);
            }

            return thing;
        }
        
        public async Task<IResult> ExecuteObjectDropped(params object[] args)
        {
            if (!_actionsContext.ObjectDroppedActionIsMocked)
            {
                var command = _mainHooks.Container.ResolveKeyed<ICommand>(nameof(DropObject));
                return  await command.Execute(args);
            }
            
            var sut = new DropObject(
                _mainHooks.Container.Resolve<ICommandHistory>(),
                _mainHooks.Container.Resolve<IVariables>(),
                ObjectDroppedMock.Object);
            
            return await sut.Execute(args);
        }

        [BeforeScenario]
        private void BeforeScenario()
        {
            ObjectDroppedMock = _actionMockBuilder.Build();
        }
        
        
    }
}