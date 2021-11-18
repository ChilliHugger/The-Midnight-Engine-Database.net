using System;
using System.Linq;
using Autofac;
using NUnit.Framework;
using TME;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace DatabaseTest
{
    [TestFixture]
    public class DatabaseLoadingTests
    {
        private IContainer _container;
        
        [SetUp]
        public void Setup()
        {
            var dependencyContainer = new TMEDependencyContainer(new ContainerBuilder());
            _container= dependencyContainer.CurrentContainer;
            
            var database = _container.Resolve<IDatabase>();
            
            database.Directory = "../../../data/lom";
            database.Load();
        }
        
        [Test]
        public void TestEntitiesCreated()
        {
            var entities = _container.Resolve<IEntityContainer>();

            Assert.True(entities.Lords.Any());
            Assert.True(entities.RouteNodes.Any());
            Assert.True(entities.Regiments.Any());
            Assert.True(entities.Strongholds.Any());
            
        }
        
        [Test]
        public void TestEntitiesLoaded()
        {
            var entities = _container.Resolve<IEntityContainer>();
            
            Assert.True(entities.Lords.All( i => IsValidItemType(EntityType.Character,i) ), "Lords have not loaded");
            Assert.True(entities.RouteNodes.All(i => IsValidItemType(EntityType.RouteNode,i) ), "RouteNodes have not loaded");
            Assert.True(entities.Regiments.All(i => IsValidItemType(EntityType.Regiment,i) ), "Regiments have not loaded");
            Assert.True(entities.Strongholds.All(i => IsValidItemType(EntityType.Stronghold,i) ), "Strongholds have not loaded");
            
        }

        private static bool IsValidItemType(EntityType type, IEntity entity)
        {
            return entity.Id.Type == type && entity.Id.RawId != 0 && !string.IsNullOrEmpty(entity.Symbol);
        }
    }
}