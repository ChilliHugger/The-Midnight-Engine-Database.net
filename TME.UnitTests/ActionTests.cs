using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
using NUnit.Framework;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Actions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;
using Thing = TME.Scenario.Default.Items.Thing;

namespace TME.UnitTests
{
    [TestFixture]
    public class ActionTests : BaseTestClass
    {

        // context
        private MapLoc _currentLocation;
        
        protected override void AfterSetup()
        {
            _currentLocation = new MapLoc
            {
                Terrain = Terrain.Citadel,
                Flags = 0,
                Thing = ThingType.None,
                HasObject = false
            };
            
            var database = Container.Resolve<IDatabase>();
            var map = Container.Resolve<IMap>();
        }

        delegate void SetAtDelegate(Loc loc, ref MapLoc mapLoc);

        
        protected override void RegisterMocks(ContainerBuilder containerBuilder)
        {
            var mockMap = new Mock<IMap>();
            mockMap
                .Setup(m => m.GetAt(It.IsAny<Loc>()))
                .Returns(() => _currentLocation);

            mockMap
                .Setup(m => m.SetAt(It.IsAny<Loc>(), ref It.Ref<MapLoc>.IsAny))
                .Callback(new SetAtDelegate((Loc loc, ref MapLoc mapLoc) =>
                {
                    _currentLocation = mapLoc;
                }));
            
            containerBuilder.RegisterInstance(mockMap.Object).SingleInstance();
        }

        [Test]
        public async Task TestActionObjectDropped()
        {
            var sut = Container.ResolveKeyed<IAction>(nameof(ObjectDropped));

            // GIVEN a lord is carrying an object
            var lord = Container.Resolve<ILord>();
            var thing = Container.Resolve<IThing>();

            GivenALordIsCarryingAnObjectOfType(lord, thing, ThingType.Moonring);

            // WHEN the object is dropped
            var result = await sut.Execute(thing!);
            
            // THEN the object will be at the current location
            Assert.True(result is Success, "Command Successful");
            Assert.False(_currentLocation.HasObject, "Location has object");
            Assert.AreEqual(ThingType.Moonring, _currentLocation.Thing, "Location has correct object type");
        }
        
        [Test]
        public async Task TestActionUniqueObjectDropped()
        {
            var sut = Container.ResolveKeyed<IAction>(nameof(ObjectDropped));

            // GIVEN a lord is carrying an object
            var lord = Container.Resolve<ILord>();
            var thing = Container.Resolve<IThing>();

            GivenALordIsCarryingAnObjectOfType(lord, thing, ThingType.IceCrown, true);

            // WHEN the object is dropped
            var result = await sut.Execute(thing!);
            
            // THEN the object will be at the current location
            Assert.True(result is Success, "Command Successful");
            Assert.True(_currentLocation.HasObject, "Location has object");
            Assert.AreEqual(ThingType.IceCrown, _currentLocation.Thing, "Location has correct object type");
        }

        private static void GivenALordIsCarryingAnObjectOfType(IItem lord, IThing thing, ThingType thingType, bool unique = false)
        {
            if (lord is ILordInternal internalLord)
            {
                internalLord.SetCarrying(new List<IThing>{thing});
            }

            if (thing is IThingInternal internalThing && thing is Thing item)
            {
                item.Id = new MXId(EntityType.Thing,(uint)thingType);
                if (unique)
                {
                    item.SetFlags(ThingFlags.Unique.Raw(), true);
                }
                else
                {
                    item.SetFlags(ThingFlags.Unique.Raw(), false);
                }
                internalThing.UpdateCarriedBy(lord);
            }
        }
        
        
    }
}