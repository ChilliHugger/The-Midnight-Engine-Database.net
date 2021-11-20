using Autofac;

namespace TME.Interfaces
{
    public interface IDependencyContainer
    {
        IContainer? CurrentContainer { get; set; }

        IDependencyContainer RegisterModules();
        IDependencyContainer Build();

    }
}
