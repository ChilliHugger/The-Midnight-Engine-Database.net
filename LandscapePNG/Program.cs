using System;
using Autofac;
using TME;
using TME.Interfaces;
using TME.Landscaping;
using TME.Scenario.lom;

namespace LandscapePNG
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ImageLoader>();

            builder.RegisterType<LandscapeLand>();
            builder.RegisterType<LandscapeSky>();
            builder.RegisterType<LandscapeTerrain>();
            
            builder.RegisterType<LandscapeBitmapGenerator>();
            builder.RegisterType<LandscapeGenerator>().SingleInstance();
            
            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();
            var container = dependencyContainer.CurrentContainer!;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();
            
            engine.SetScenario(MidnightScenario.Tag);
            database.Directory = "../../../../data/lom";
            if (!database.Load())
            {
                Console.WriteLine("Unable to load Database");
                return;
            }
            
            var bitmapGenerator = container.Resolve<LandscapeBitmapGenerator>();
            bitmapGenerator.Build();
            
            
        }
    }
}
