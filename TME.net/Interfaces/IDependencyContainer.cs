using Autofac;

namespace TME.Interfaces
{
    public interface IDependencyContainer
    {
        IContainer CurrentContainer { get; }

        IDependencyContainer RegisterModules();
        IDependencyContainer Build();

    }
}
