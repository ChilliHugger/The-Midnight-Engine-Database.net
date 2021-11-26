using Autofac;
using Autofac.Core.Lifetime;

namespace TME.Interfaces
{
    public interface IEngine
    {
        IScenario Scenario { get; }
        IScenario SetScenario(string scenario);
        ILifetimeScope? CurrentScope { get; }
    }
}
