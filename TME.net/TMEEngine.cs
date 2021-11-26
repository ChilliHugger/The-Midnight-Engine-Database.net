using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Core.Lifetime;
using TME.Interfaces;

[assembly:InternalsVisibleTo("TME.UnitTests")]
[assembly:InternalsVisibleTo("TME.SpecTests")]

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TMEEngine : IEngine, IDisposable
    {
        private readonly IDependencyContainer _container;
        public ILifetimeScope? CurrentScope { get; internal set; }
        
        public IScenario Scenario { get; internal set; }= null!;
        
        public TMEEngine(IDependencyContainer container)
        {
            _container = container;
        }

        public IScenario SetScenario(string scenarioTag)
        {
            CurrentScope?.Dispose();
            CurrentScope = null;
            
            var scenario = _container.CurrentContainer.ResolveKeyed<IScenario>(scenarioTag);
            Scenario = scenario;

            var scope = _container.CurrentContainer.BeginLifetimeScope( scenario.Info.Symbol, builder =>
            {
                scenario.Register(builder);
            });

            _container.CurrentScope = scope;

            return Scenario;
        }

        public void Dispose()
        {
            CurrentScope?.Dispose();
        }
    }
}
