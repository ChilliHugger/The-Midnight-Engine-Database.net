using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Autofac;
using Microsoft.Extensions.Logging;
using TME;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Scenario.lom;
using TME.Types;

namespace MidnightConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dependencyContainer = new TMEDependencyContainer(new ContainerBuilder());
            dependencyContainer.Build();
            var container = dependencyContainer.CurrentContainer!;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();
            var entities = container.Resolve<IEntityContainer>();
            var strings = container.Resolve<IStrings>();
            var variables = container.Resolve<IVariables>();
     
            engine.SetScenario(RevengeScenario.Tag);
            database.Directory = "data/ddr";
            if (!database.Load())
            {
                Console.WriteLine("Unable to load Database");
                return;
            }
            
            OutputCollection("Lords", entities.Lords);
            OutputCollection("Route Nodes", entities.RouteNodes);
            OutputCollection("Regiments", entities.Regiments);
            OutputCollection("Strongholds", entities.Strongholds);
            OutputCollection("Waypoints", entities.Waypoints);
            OutputCollection("Things", entities.Things);
            OutputCollection("Missions", entities.Missions);
            OutputCollection("Victories", entities.Victories);
            
            OutputCollection("Directions", entities.Directions);
            OutputCollection("Units", entities.Units);
            OutputCollection("Races", entities.Races);
            OutputCollection("Genders", entities.Genders);
            OutputCollection("Terrains", entities.Terrains);
            OutputCollection("Areas", entities.Areas);
            OutputCollection("Commands", entities.Commands);
            
            OutputCollection("Strings", strings.Entries);
            
            
            Console.WriteLine($"Variables\n{new string('=', 9)}");
            foreach (var (key, value) in variables.GetValues())
            {
                Console.WriteLine($"{key}='{value}'");
            }
            Console.WriteLine();
            
        }

        private static void OutputCollection( string title, IEnumerable<object> list)
        {
            Console.WriteLine($"{title}\n{new string('=', title.Length)}");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
        
    }
}
