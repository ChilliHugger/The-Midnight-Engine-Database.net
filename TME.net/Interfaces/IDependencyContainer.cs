using Autofac;

namespace TME.Interfaces
{
    public interface IDependencyContainer
    {
        IContainer CurrentContainer { get; }
        ILifetimeScope? CurrentScope { get; set; }
        
        IDependencyContainer RegisterModules();
        IDependencyContainer Build();
    }
}
