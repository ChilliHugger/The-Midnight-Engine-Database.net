using System.Linq;
using Autofac;
using NUnit.Framework;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.lom;

namespace TME.UnitTests
{
    [TestFixture]
    public class RevengeLoadingTests
    {
        private IContainer _container;
        
        [SetUp]
        public void Setup()
        {
            var dependencyContainer = new TMEDependencyContainer(new ContainerBuilder());
            dependencyContainer.Build();
            _container= dependencyContainer.CurrentContainer;
            
            var database = _container.Resolve<IDatabase>();
            var engine = _container.Resolve<IEngine>();

            engine.SetScenario(RevengeScenario.Tag);
            database.Directory = "../../../../data/ddr";
            database.Load();
        }

        [Test]
        public void TestEntitiesCreated()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.Multiple(() =>
            {
                Assert.That(entities.Lords.Any(), "No Lords");
                Assert.That(entities.RouteNodes, Is.Empty, "Not expecting RouteNodes");
                Assert.That(entities.Regiments, Is.Empty, "Not expecting Regiments");
                Assert.That(entities.Strongholds.Any(), "No Strongholds");
                Assert.That(entities.Waypoints, Is.Empty, "Not expecting Waypoints");
                Assert.That(entities.Things.Any(), "No Things");
                Assert.That(entities.Missions, Is.Empty, "Not expecting Missions");
                Assert.That(entities.Victories, Is.Empty, "Not expecting Victories");
            });
        }

        [Test]
        public void TestInfoCreated()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.Multiple(() =>
            {
                Assert.That(entities.Directions.Any(), "No Directions");
                Assert.That(entities.Units.Any(), "No Units");
                Assert.That(entities.Races.Any(), "No Races");
                Assert.That(entities.Genders.Any(), "No Genders");
                Assert.That(entities.Terrains.Any(), "No Terrains");
                Assert.That(entities.Areas.Any(), "No Areas");
                Assert.That(entities.Commands.Any(), "No Commands");
            });
        }

        [Test]
        public void TestStringsCreated()
        {
            var strings = _container.Resolve<IStrings>();
            Assert.That(strings.Entries.Any(), "No Strings");
        }
        
        [Test]
        public void TestVariablesCreated()
        {
            var variables = _container.Resolve<IVariables>();
            Assert.That(variables.GetValues().Any(), "No Variables");
        }
        
        [Test]
        public void TestEntitiesLoaded()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.Multiple(() =>
            {
                Assert.That(entities.Lords.All(i => IsValidItemType(EntityType.Character, i)), "Lords have not loaded");
                Assert.That(entities.Strongholds.All(i => IsValidItemType(EntityType.Stronghold, i)), "Strongholds have not loaded");
                Assert.That(entities.Waypoints.All(i => IsValidItemType(EntityType.Waypoint, i)), "Waypoints have not loaded");
                Assert.That(entities.Things.All(i => IsValidItemType(EntityType.Thing, i)), "Things have not loaded");
            });
        }

        [Test]
        public void TestInfoLoaded()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.Multiple(() =>
            {
                Assert.That(entities.Directions.All(i => IsValidInfoType(EntityType.DirectionInfo, i)), "Directions have not loaded");
                Assert.That(entities.Units.All(i => IsValidInfoType(EntityType.UnitInfo, i)), "Units have not loaded");
                Assert.That(entities.Races.All(i => IsValidInfoType(EntityType.RaceInfo, i)), "Races have not loaded");
                Assert.That(entities.Genders.All(i => IsValidInfoType(EntityType.GenderInfo, i)), "Genders have not loaded");
                Assert.That(entities.Terrains.All(i => IsValidInfoType(EntityType.TerrainInfo, i)), "Terrains have not loaded");
                Assert.That(entities.Areas.All(i => IsValidInfoType(EntityType.AreaInfo, i)), "Areas have not loaded");
                Assert.That(entities.Commands.All(i => IsValidInfoType(EntityType.CommandInfo, i)), "Commands have not loaded");
            });
        }

        private static bool IsValidItemType(EntityType type, IEntity entity)
        {
            return entity.Id.Type == type && entity.Id.RawId != 0 && !string.IsNullOrEmpty(entity.Symbol);
        }
        private static bool IsValidInfoType(EntityType type, IEntity entity)
        {
            return entity.Id.Type == type && !string.IsNullOrEmpty(entity.Symbol);
        }
    }
}