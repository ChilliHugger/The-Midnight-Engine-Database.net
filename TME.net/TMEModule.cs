using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Interfaces;
using TME.Scenario.Default.Actions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Commands;
using TME.Scenario.Default.Entities;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.Scenario;
using TME.Serialize;
using Lord = TME.Scenario.Default.Items.Lord;
using Regiment = TME.Scenario.Default.Items.Regiment;
using Stronghold = TME.Scenario.Default.Items.Stronghold;
using Thing = TME.Scenario.Default.Items.Thing;

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
            RegisterTypes();
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
            _builder.RegisterType<TMEMap>().As<IMap>();
        }

        private void RegisterEntities()
        {
            _builder.RegisterType<RouteNodes>().As<IRouteNodes>();
            _builder.RegisterType<RouteNode>().As<IRouteNode>();
            _builder.RegisterType<Regiment>().As<IRegiment>();
            _builder.RegisterType<Stronghold>().As<IStronghold>();
            _builder.RegisterType<Waypoint>().As<IWaypoint>();
            _builder.RegisterType<Thing>().As<IThing>();

            _builder.RegisterType<Mission>().As<IMission>();
            _builder.RegisterType<Victory>().As<IVictory>();
            
            _builder.RegisterType<Lord>().As<ILord>().WithAttributeFiltering();

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
    }
}
