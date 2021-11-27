using Autofac;

namespace TME.Interfaces
{
    public interface IScenario
    {
        IScenarioInfo Info { get;}
        void Register(ContainerBuilder containerBuilder);

        void InitialiseAfterGameLoad();
    }
}