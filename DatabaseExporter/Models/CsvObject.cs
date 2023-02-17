using CsvHelper.Configuration;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

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
    }
    
    public sealed class CsvObjectMap : ClassMap<CsvObject>
    {
        public CsvObjectMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvObject
            Map(m => m.Kills).Index(5);
            Map(m => m.Name).Index(6);
            Map(m => m.Description).Index(7);
            Map(m => m.UseDescription.Symbol).Index(8).Name("UseDescription");
            Map(m => m.CarriedBy.Symbol).Index(9).Name("CarriedBy");
        }
    }
}