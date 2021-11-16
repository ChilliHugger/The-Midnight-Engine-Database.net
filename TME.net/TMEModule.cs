using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Default;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;

namespace TME
{
    public class TMEModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TMEEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<TMEDatabase>().As<IDatabase>().SingleInstance();
            
            builder.RegisterType<TMEEntityResolver>().As<IEntityResolver>();
            builder.RegisterType<TMEMap>().As<IMap>();
            builder.RegisterType<Variables>().As<IVariables>().SingleInstance();

            builder.RegisterType<RouteNodes>().As<IRouteNodes>();
            builder.RegisterType<RouteNode>().As<IRouteNode>();
            builder.RegisterType<Regiment>().As<IRegiment>();
 
            builder.RegisterType<Lord>().As<ILord>().WithAttributeFiltering();
            builder.RegisterType<BattleInfo>().As<IBattleInfo>();
            builder.RegisterType<Recruitment>().As<IRecruitment>();

            builder.RegisterType<Warriors>().Keyed<IUnit>(UnitType.Warrior);
            builder.RegisterType<Riders>().Keyed<IUnit>(UnitType.Rider);


        }
    }
}
