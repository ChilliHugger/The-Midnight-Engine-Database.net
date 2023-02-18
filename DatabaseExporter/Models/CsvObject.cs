using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvObject : CsvItem
    {
        public ThingType Kills { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public CsvId UseDescription { get; set; }
        public CsvId CarriedBy { get; set; }
        
        // ddr
        public ObjectType ObjectType { get; set; }
        public ObjectPower ObjectPower { get; set; }
        
        // for export
        public ThingFlags ObjectFlags => (ThingFlags) Flags;
    }
    
    public sealed class CsvObjectMap : CsvClassMap<CsvObject>
    {
        public CsvObjectMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.ObjectFlags)).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvObject
            Map(m => m.Kills).Index(5);
            Map(m => m.Name).Index(6);
            Map(m => m.Description).Index(7);
            Map(m => m.UseDescription.Symbol).Index(8).Name("UseDescription");
            Map(m => m.CarriedBy.Symbol).Index(9).Name("CarriedBy");
            Map(m => m.ObjectType).Index(10).Name("Type");
            Map(m => m.ObjectPower).Index(11).Name("Power");
        }
    }
}