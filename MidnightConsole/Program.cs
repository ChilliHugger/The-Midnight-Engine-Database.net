using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using TME;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Scenario;

namespace MidnightConsole
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var dependencyContainer = new TMEDependencyContainer(new ContainerBuilder());
            var container = dependencyContainer.CurrentContainer;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();
            var entities = container.Resolve<IEntityContainer>();

            database.Directory = "data/lom";
            database.Load();
            
            OutputCollection("Lords", entities.Lords);
            OutputCollection("Route Nodes", entities.RouteNodes);
            OutputCollection("Regiments", entities.Regiments);
            OutputCollection("Strongholds", entities.Strongholds);
        
        }

        private static void OutputCollection( string title, IEnumerable<IEntity> list)
        {
            Console.WriteLine($"{title}\n{new string('=', title.Length)}");
            foreach (var item in list)
            {
                OutputSymbol(item);
            }
            Console.WriteLine();
        }
        
        private static void OutputSymbol(IEntity item)
        {
            Console.WriteLine(item);
        }
        
    }
}
