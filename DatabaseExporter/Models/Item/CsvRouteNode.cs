// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Models.Item
{
    public class CsvRouteNode : CsvItem
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }
    
    public sealed class OutRouteNodeMap : ClassMap<IRouteNode>
    {
        public OutRouteNodeMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRouteNode
            Map(m => m.RouteNodes).Index(5);
        }
    }
}