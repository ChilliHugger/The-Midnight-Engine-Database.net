
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Autofac;
using CsvHelper;
using DatabaseExporter.Models;
using TME;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;

namespace DatabaseExporter
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
            var variables = container.Resolve<IVariables>();

            database.Directory = "../../../../data/lom";
            database.Load();

            //var results = variables.GetValues();

            using var writer = new StreamWriter("routenodes.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<CSVRouteNodeMap>();
            
            var routeNodes = entities.RouteNodes.Select(r =>
                new CsvRouteNode
                {
                    Id = (uint)r.Id.RawId,
                    Symbol = r.Symbol,
                    Flags = r.RawFlags,
                    Location = r.Location,
                    Left = r.RouteNodes.Nodes.FirstOrDefault()?.Symbol ?? "",
                    Right = r.RouteNodes.Nodes.LastOrDefault()?.Symbol ?? "",
                }
            );
            
            csv.WriteRecords(routeNodes);
        }
    }
}