﻿using Autofac;
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using TME.Interfaces;
using TME.QueryServices;
using TME.Scenario.Default.Actions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Commands;
using TME.Scenario.Default.Entities;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.LocationInfoBuilders;
using TME.Scenario.Default.Rules;
using TME.Scenario.Default.Scenario;
using TME.Serialize;
using Lord = TME.Scenario.Default.Items.Lord;
using Regiment = TME.Scenario.Default.Items.Regiment;
using Stronghold = TME.Scenario.Default.Items.Stronghold;

namespace TME
{
    public class TMEModule : Module
    {
        private ContainerBuilder _builder = null!;
        protected override void Load(ContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.RegisterType<TMEEngine>().As<IEngine>().SingleInstance();
            
            RegisterDatabase();
            RegisterHelpers();
            RegisterEntities();
            RegisterInfo();
            RegisterActions();
            RegisterCommands();
            RegisterRules();
            RegisterTypes();
            RegisterQueryServices();
            RegisterBuilders();
            RegisterLogger();
        }

        private void RegisterTypes()
        {
            _builder.RegisterType<BattleInfo>().As<IBattleInfo>();
            _builder.RegisterType<Recruitment>().As<IRecruitment>();
        }

        private void RegisterDatabase()
        {
            _builder.RegisterType<TMEDatabase>().As<IDatabase>().SingleInstance();
            _builder.RegisterType<TMEEntityContainer>().As<IEntityContainer>().SingleInstance();
            _builder.RegisterType<Variables>().As<IVariables>().SingleInstance();
            _builder.RegisterType<TMEStrings>().As<IStrings>().SingleInstance();
            _builder.RegisterType<TMEMap>().As<IMap>();
        }

        private void RegisterQueryServices()
        {
            _builder.RegisterType<MapQueryService>().As<IMapQueryService>();
            _builder.RegisterType<ArmyQueryService>().As<IArmyQueryService>();
        }

        private void RegisterBuilders()
        {
            _builder.RegisterType<LocationInfoBuilder>().As<ILocationInfoBuilder>();
            _builder.RegisterType<LocationActionBuilder>().As<ILocationActionBuilder>();
            _builder.RegisterType<LocationLordInfoBuilder>().As<ILocationLordInfoBuilder>();
            _builder.RegisterType<LocationArmyInfoBuilder>().As<ILocationArmyInfoBuilder>();
            _builder.RegisterType<LocationArmyCountInfoBuilder>().As<ILocationArmyCountInfoBuilder>();
        }
        
        
        private void RegisterEntities()
        {
            _builder.RegisterType<RouteNodes>().As<IRouteNodes>();
            _builder.RegisterType<RouteNode>().As<IRouteNode>();
            _builder.RegisterType<Regiment>().As<IRegiment>();
            _builder.RegisterType<Stronghold>().As<IStronghold>();
            _builder.RegisterType<Waypoint>().As<IWaypoint>();
            _builder.RegisterType<Object>().As<IObject>();

            _builder.RegisterType<Mission>().As<IMission>();
            _builder.RegisterType<Victory>().As<IVictory>();
            
            _builder.RegisterType<Lord>().As<ICharacter>().WithAttributeFiltering();

            _builder.RegisterType<Warriors>().Keyed<IUnit>(UnitType.Warrior);
            _builder.RegisterType<Riders>().Keyed<IUnit>(UnitType.Rider);
        }

        private void RegisterInfo()
        {
            _builder.RegisterType<DirectionInfo>();
            _builder.RegisterType<UnitInfo>();
            _builder.RegisterType<RaceInfo>();
            _builder.RegisterType<GenderInfo>();
            _builder.RegisterType<TerrainInfo>();
            _builder.RegisterType<AreaInfo>();
            _builder.RegisterType<CommandInfo>();
        }
        
        private void RegisterHelpers()
        {
            _builder.RegisterType<TMEEntityResolver>().As<IEntityResolver>();
            _builder.RegisterType<SerializeContext>().As<ISerializeContext>();
        }

        private void RegisterActions()
        {
            _builder
                .RegisterType<ObjectDropped>()
                .Keyed<IAction>(nameof(ObjectDropped));
        }

        private void RegisterCommands()
        {
            _builder
                .RegisterType<DropObject>()
                .Keyed<ICommand>(nameof(DropObject))
                .WithAttributeFiltering();
        }

        private void RegisterRules()
        {
            _builder.RegisterType<CharacterApproachRule>().As<ICharacterApproachRule>();
            _builder.RegisterType<CharacterMoveForwardRule>().As<ICharacterMoveForwardRule>();
            _builder.RegisterType<CharacterRecruitRule>().As<ICharacterRecruitRule>();
        }
        
        private static void ConfigureLogging(ILoggingBuilder log)
        {
            log.ClearProviders();
            log.SetMinimumLevel(LogLevel.Debug);
            log.AddProvider(new DebugLoggerProvider());
        }
        
        private void RegisterLogger()
        {
            _builder.Register(_ => LoggerFactory.Create(ConfigureLogging))
                .As<ILoggerFactory>()
                .SingleInstance()
                .AutoActivate();

            _builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();
        }
    }
}
