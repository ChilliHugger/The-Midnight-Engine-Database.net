using System;
using Autofac;
using TME;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.lom;

namespace DatabaseExporter
{
    internal static class Program
    {
        static void Main()
        {
            //ExportDatabase(MidnightScenario.Tag,"../../../../data/lom","../../../../data/csv_lom");
            //ExportDatabase(RevengeScenario.Tag,"../../../../data/ddr","../../../../data/csv_ddr");

            ImportDatabase(MidnightScenario.Tag, "../../../../data/csv_lom", "../../../../data/lom2");

            
        }

        private static void ImportDatabase(string scenarioTag, string directory, string output)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<CsvImporter>();

            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();

            var container = dependencyContainer.CurrentContainer;

            var engine = container.Resolve<IEngine>();
            //var database = container.Resolve<IDatabase>();

            engine.SetScenario(scenarioTag);

            var importer = container.Resolve<CsvImporter>();
            importer.Process(directory, scenarioTag);
        }
        
        private static void ExportDatabase(string scenarioTag,string directory, string output)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<CsvExporter>();

            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();

            var container = dependencyContainer.CurrentContainer;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();

            engine.SetScenario(scenarioTag);
            database.Directory = directory;
            database.Load();

            var exporter = container.Resolve<CsvExporter>();
            exporter.Process(output,scenarioTag);
        }
    }
}