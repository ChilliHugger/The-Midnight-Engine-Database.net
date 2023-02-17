using Autofac;
using DatabaseExporter.Mapping;
using TME;
using TME.Interfaces;
using TME.Scenario.lom;

namespace DatabaseExporter
{
    internal static class Program
    {
        static void Main()
        {
            ExportDatabase("../../../../data/lom","../../../../data/csv_lom");
        }

        private static void ExportDatabase(string directory, string output)
        {
            var builder = new ContainerBuilder();

            RegisterMapping(builder);

            builder.RegisterType<CsvExporter>();

            var dependencyContainer = new TMEDependencyContainer(builder);
            dependencyContainer.Build();

            var container = dependencyContainer.CurrentContainer;

            var engine = container.Resolve<IEngine>();
            var database = container.Resolve<IDatabase>();

            engine.SetScenario(MidnightScenario.Tag);
            database.Directory = directory;
            database.Load();

            var exporter = container.Resolve<CsvExporter>();
            exporter.Export(output);
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