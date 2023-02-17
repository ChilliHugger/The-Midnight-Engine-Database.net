// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvWaypoint : CsvItem
    {
    }
    
    public sealed class CsvWaypointMap : CsvClassMap<CsvWaypoint>
    {
        public CsvWaypointMap()
        {
            // Entity
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // Item
            Map(m => m.Location).Index(4);
        }
    }
}