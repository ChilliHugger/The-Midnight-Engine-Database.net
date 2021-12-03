using CsvHelper.Configuration;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Scenario;

// ReSharper disable ClassNeverInstantiated.Global
namespace DatabaseExporter.Models
{
    public class CsvRecord
    {
        public int Version { get; set; }
        public uint Id { get; set; }
        public string Symbol { get; set; }
    }
    
    public class CsvEntity : CsvRecord
    {
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
    
    public sealed class CsvRouteNodeMap : ClassMap<CsvRouteNode>
    {
        public CsvRouteNodeMap()
        {
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            Map(m => m.Flags).Index(3);
            Map(m => m.Location).Index(4);
            Map(m => m.Left).Index(5);
            Map(m => m.Right).Index(6);
        }
    }
}