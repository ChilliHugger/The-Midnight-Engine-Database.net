using CsvHelper.Configuration;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Scenario;

namespace DatabaseExporter.Models
{
    public class CsvEntity
    {
        public uint Id { get; set; }
        public string Symbol { get; set; }
        public ulong Flags { get; set; }
    }

    public class CsvItem : CsvEntity
    {
        public Loc Location { get; set; }
    }

    public class CsvRouteNode : CsvItem
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }
    
    public sealed class CSVRouteNodeMap : ClassMap<CsvRouteNode>
    {
        public CSVRouteNodeMap()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Symbol).Index(1);
            Map(m => m.Flags).Index(1);
            Map(m => m.Location).Index(1);
            Map(m => m.Left).Index(1);
            Map(m => m.Right).Index(1);
        }
    }
}