using Autofac;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.lom
{
    public class MidnightScenario : IScenario, IScenarioInfo
    {
        private readonly IContainer _container;
        
        public static string Tag => "lom";
        public uint Id =>  16;
        public uint Version => 200;
        public int DatabaseVersion => 7;
        public string Symbol => Tag;
        public string Name => "The Lords of Midnight";
        public string Website => "https://www.thelordsofmidnight.com";
        public string Support => "rorthron@thelordsofmidnight.com";
        public string Author => "Chris Wild";
        public string Copyright => "Copyright 1984 - 2021 Mike Singleton & Chris Wild";
        
        public IScenarioInfo Info => this;

        public MidnightScenario(IDependencyContainer dependencyContainer)
        {
            _container = dependencyContainer.CurrentContainer;
        }
        
        public void Register(ContainerBuilder containerBuilder)
        {
        }
 
        public object Resolve<T>()
            where T : IEntity
        {
            return _container.Resolve<T>();
        }
        

    }
}