using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
using TechTalk.SpecFlow;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Actions.Interfaces;
using TME.Scenario.Default.Commands;
using TME.Scenario.Default.Commands.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.Scenario;
using TME.SpecTests.Context;
using TME.SpecTests.Hooks;
using TME.SpecTests.Mocks;
using TME.Types;

namespace TME.SpecTests.Drivers
{
    [Binding]
    public class ActionDriver
    {
        private readonly MainHooks _mainHooks;
        private readonly ActionMockBuilder _actionMockBuilder;
        private readonly ActionsContext _actionsContext;

        public Mock<IObjectDroppedAction> ObjectDroppedMock { get; private set; } = null!;

        public ActionDriver(
            ActionsContext actionsContext,
            ActionMockBuilder actionMockBuilder,
            MainHooks mainHooks)
        {
            _actionsContext = actionsContext;
            _actionMockBuilder = actionMockBuilder;
            _mainHooks = mainHooks;
            
            _mainHooks.RegisterMockHandler(MockRegistration);
        }
        
        public (ICharacter lord, IObject thing) GivenALordIsCarryingAnObjectOfType(ThingType thingType, bool unique = false)
        {
            var lord = _mainHooks.Container.Resolve<ICharacter>();
            var thing = GivenAnObjectOfType(thingType,unique);

            if (lord is ICharacterInternal internalLord)
            {
                internalLord.SetCarrying(new List<IObject>{thing});
            }

            if (thing is IObjectInternal internalThing)
            {
                internalThing.UpdateCarriedBy(lord);
            }

            return (lord, thing);
        }

        public IObject GivenAnObjectOfType(ThingType thingType, bool unique = false)
        {
            var thing = _mainHooks.Container.Resolve<IObject>();
            
            if (thing is Object item)
            {
                item.Id = new MXId(EntityType.Thing,(uint)thingType);
                item.SetFlags(ThingFlags.Unique, unique);
            }

            return thing;
        }

        private void MockRegistration(ContainerBuilder builder)
        {
            if (_actionsContext.ObjectDroppedActionIsMocked)
            {
                ObjectDroppedMock = _actionMockBuilder.Build(builder);
            }
        }
        
        
        public IResult ExecuteObjectDropped(ICharacter character, IObject thing)
        {
            var command = _mainHooks.TestsContainer?.Resolve<IDropObjectCommand>();
            return command?.Execute(character, thing) ?? Failure.Default;
        }
    }
}