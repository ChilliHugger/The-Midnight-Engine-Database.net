
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Autofac;
using AutoMapper;
using CsvHelper;
using DatabaseExporter.Mapping;
using DatabaseExporter.Models;
using TME;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.Scenario;
using TME.Scenario.lom;

namespace DatabaseExporter
{
    internal static class Program
    {
        static void Main(string[] args)
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
            database.Directory = "../../../../data/lom";
            database.Load();

            var exporter = container.Resolve<CsvExporter>();
            exporter.Export();

        }
        
        private static void RegisterMapping(ContainerBuilder builder)
        {
            builder.RegisterInstance(MapperProvider.GetMapper(
                new MappingConfiguration()
            ));
        }


    }
}