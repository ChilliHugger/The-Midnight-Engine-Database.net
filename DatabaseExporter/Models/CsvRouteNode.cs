// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvRouteNode : CsvItem
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }
    
    public sealed class CsvRouteNodeMap : CsvClassMap<CsvRouteNode>
    {
        public CsvRouteNodeMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRouteNode
            Map(m => m.Left).Index(5);
            Map(m => m.Right).Index(6);
        }
    }
}