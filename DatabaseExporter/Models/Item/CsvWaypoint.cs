// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Models.Item
{
    public class CsvWaypoint : CsvItem
    {
    }
    
    public sealed class OutWaypointMap : ClassMap<IWaypoint>
    {
        public OutWaypointMap()
        {
            // Entity
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // Item
            Map(m => m.Location).Index(4);
        }
    }
}