using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Default;
using TME.Interfaces;
using TME.Scenario;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Scenario.Default.Scenario.Actions;
using TME.Scenario.Default.Scenario.Commands;
using TME.Serialize;

namespace TME
{
    public class TMEModule : Module
    {
        private ContainerBuilder _builder;
        protected override void Load(ContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.RegisterType<TMEEngine>().As<IEngine>().SingleInstance();
            
            RegisterDatabase();
            RegisterHelpers();
            RegisterEntities();
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
            _builder.RegisterType<Lord>().As<ILord>().WithAttributeFiltering();

            _builder.RegisterType<Warriors>().Keyed<IUnit>(UnitType.Warrior);
            _builder.RegisterType<Riders>().Keyed<IUnit>(UnitType.Rider);
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
