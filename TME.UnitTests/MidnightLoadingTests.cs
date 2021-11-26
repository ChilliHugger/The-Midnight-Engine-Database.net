using System.Linq;
using Autofac;
using NUnit.Framework;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.lom;

namespace TME.UnitTests
{
    [TestFixture]
    public class MidnightLoadingTests
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

            engine.SetScenario(MidnightScenario.Tag);
            database.Directory = "../../../data/lom";
            database.Load();
        }

        [Test]
        public void TestEntitiesCreated()
        {
            var entities = _container.Resolve<IEntityContainer>();

            Assert.True(entities.Lords.Any(), "No Lords");
            Assert.True(entities.RouteNodes.Any(), "No RouteNodes");
            Assert.True(entities.Regiments.Any(), "No Regiments");
            Assert.True(entities.Strongholds.Any(), "No Strongholds");
            Assert.True(entities.Waypoints.Any(), "No Waypoints");
            Assert.True(entities.Things.Any(), "No Things");
            Assert.True(entities.Missions.Any(), "No Missions");
            Assert.True(entities.Victories.Any(), "No Victories");
        }

        [Test]
        public void TestInfoCreated()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.True(entities.Directions.Any(), "No Directions");
            Assert.True(entities.Units.Any(), "No Units");
            Assert.True(entities.Races.Any(), "No Races");
            Assert.True(entities.Genders.Any(), "No Genders");
            Assert.True(entities.Terrains.Any(), "No Terrains");
            Assert.True(entities.Areas.Any(), "No Areas");
            Assert.True(entities.Commands.Any(), "No Commands");
        }
        
        [Test]
        public void TestStringsCreated()
        {
            var strings = _container.Resolve<IStrings>();
            Assert.True(strings.Entries.Any(), "No Strings");
        }
        
        [Test]
        public void TestVariablesCreated()
        {
            var variables = _container.Resolve<IVariables>();
            Assert.True(variables.GetValues().Any(), "No Variables");
        }

        
        [Test]
        public void TestEntitiesLoaded()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.True(entities.Lords.All( i => IsValidItemType(EntityType.Character,i) ), "Lords have not loaded");
            Assert.True(entities.RouteNodes.All(i => IsValidItemType(EntityType.RouteNode,i) ), "RouteNodes have not loaded");
            Assert.True(entities.Regiments.All(i => IsValidItemType(EntityType.Regiment,i) ), "Regiments have not loaded");
            Assert.True(entities.Strongholds.All(i => IsValidItemType(EntityType.Stronghold,i) ), "Strongholds have not loaded");
            Assert.True(entities.Waypoints.All(i => IsValidItemType(EntityType.Waypoint,i) ), "Waypoints have not loaded");
            Assert.True(entities.Things.All(i => IsValidItemType(EntityType.Thing,i) ), "Things have not loaded");
            Assert.True(entities.Missions.All(i => IsValidItemType(EntityType.Mission,i) ), "Missions have not loaded");
            Assert.True(entities.Victories.All(i => IsValidItemType(EntityType.Victory,i) ), "Victories have not loaded");
        }

        [Test]
        public void TestInfoLoaded()
        {
            var entities = _container.Resolve<IEntityContainer>();
            Assert.True(entities.Directions.All( i => IsValidInfoType(EntityType.DirectionInfo,i) ), "Directions have not loaded");
            Assert.True(entities.Units.All(i => IsValidInfoType(EntityType.UnitInfo,i) ), "Units have not loaded");
            Assert.True(entities.Races.All(i => IsValidInfoType(EntityType.RaceInfo,i) ), "Races have not loaded");
            Assert.True(entities.Genders.All(i => IsValidInfoType(EntityType.GenderInfo,i) ), "Genders have not loaded");
            Assert.True(entities.Terrains.All(i => IsValidInfoType(EntityType.TerrainInfo,i) ), "Terrains have not loaded");
            Assert.True(entities.Areas.All(i => IsValidInfoType(EntityType.AreaInfo,i) ), "Areas have not loaded");
            Assert.True(entities.Commands.All(i => IsValidInfoType(EntityType.CommandInfo,i) ), "Commands have not loaded");
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