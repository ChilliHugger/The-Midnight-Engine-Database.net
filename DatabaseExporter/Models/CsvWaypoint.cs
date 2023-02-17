using CsvHelper.Configuration;

namespace DatabaseExporter.Models
{
    public class CsvWaypoint : CsvItem
    {
    }
    
    public sealed class CsvWaypointMap : ClassMap<CsvWaypoint>
    {
        public CsvWaypointMap()
        {
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            Map(m => m.Flags).Index(3);
            Map(m => m.Location).Index(4);
        }
    }
}