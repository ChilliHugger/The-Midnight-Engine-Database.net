using Autofac;
using DatabaseExporter.Mapping;
using TME;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.lom;

namespace DatabaseExporter
{
    internal static class Program
    {
        static void Main()
        {
            ExportDatabase(MidnightScenario.Tag,"../../../../data/lom","../../../../data/csv_lom");
            ExportDatabase(RevengeScenario.Tag,"../../../../data/ddr","../../../../data/csv_ddr");

            //ImportDatabase(MidnightScenario.Tag, "../../../../data/csv_lom", "../../../../data/lom2");

        }

        private static void ImportDatabase(string scenario, string input, string output)
        {
            var builder = new ContainerBuilder();

            RegisterMapping(builder);

            builder.RegisterType<CsvImporter>();

            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();

            var container = dependencyContainer.CurrentContainer;

            var importer = container.Resolve<CsvImporter>();
            importer.Process(input);
        }
        
        private static void ExportDatabase(string scenario,string directory, string output)
        {
            var builder = new ContainerBuilder();

            RegisterMapping(builder);

            builder.RegisterType<CsvExporter>();

            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();

            var container = dependencyContainer.CurrentContainer;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();

            engine.SetScenario(scenario);
            database.Directory = directory;
            database.Load();

            var exporter = container.Resolve<CsvExporter>();
            exporter.Process(output,scenario);
        }

        private static void RegisterMapping(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var strings = context.Resolve<IStrings>();
                return MapperProvider.GetMapper(
                    new ItemMappingConfiguration(),
                    new InfoMappingConfiguration(),
                    new EntityMappingConfiguration(),
                    new GeneralMappingConfiguration(strings));
            });

        }
    }
}