using Autofac;
using Autofac.Features.AttributeFilters;
using TME.Interfaces;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr
{
    public class RevengeScenario : IScenario, IScenarioInfo
    {
        public static string Tag => "ddr";
        public uint Id =>  17;
        public uint Version => 200;
        public int DatabaseVersion => 11;
        public string Symbol => Tag;
        public string Name => "Doomdark's Revenge";
        public string Website => "https://www.doomdarksrevenge.com";
        public string Support => "rorthron@doomdarksrevenge.com";
        public string Author => "Chris Wild";
        public string Copyright => "Copyright 1985 - 2021 Mike Singleton & Chris Wild";
        
        public IScenarioInfo Info => this;
        
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<RevengeLord>().As<ILord>().WithAttributeFiltering();
            containerBuilder.RegisterType<RevengeStronghold>().As<IStronghold>();
            containerBuilder.RegisterType<RevengeThing>().As<IThing>();
        }
        
        // public object Resolve<T>()
        //     where T : IEntity
        // {
        //     if (typeof(T) == typeof(ILord))
        //     {
        //         return _container.Resolve<IRevengeLord>();
        //     }
        //     if (typeof(T) == typeof(IStronghold))
        //     {
        //         return _container.Resolve<IRevengeStronghold>();
        //     }
        //     if (typeof(T) == typeof(IThing))
        //     {
        //         return _container.Resolve<IRevengeThing>();
        //     }
        //     return _container.Resolve<T>();
        // }
    }
}