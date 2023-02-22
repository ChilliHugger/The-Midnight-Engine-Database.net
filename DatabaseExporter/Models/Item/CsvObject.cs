using CsvHelper.Configuration;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
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
        
    }
    
    public sealed class OutObjectMap<T> : ClassMap<T>
        where T : IObject
    {
        public OutObjectMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
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
            Map(m => m.UseDescription)
                .TypeConverterOption.Format("StringId")
                .Index(8)
                .Name("Use Description");
            Map(m => m.CarriedBy).Index(9).Name("Carried By");

            if (typeof(T)==typeof(IRevengeThing))
            {
                Map<IRevengeThing>(m => m.ObjectType).Index(10).Name("Type");
                Map<IRevengeThing>(m => m.ObjectPower).Index(11).Name("Power");
            }

            Map(m => m.CanDrop).Ignore();
            Map(m => m.CanFight).Ignore();
            Map(m => m.CanPickup).Ignore();
            Map(m => m.CanRemove).Ignore();
            Map(m => m.CanSee).Ignore();
            Map(m => m.HelpsRecruitment).Ignore();
            Map(m => m.IsCarried).Ignore();
            Map(m => m.IsRandomStart).Ignore();
            Map(m => m.IsSpecial).Ignore();
            Map(m => m.IsUnique).Ignore();
            Map(m => m.IsWeapon).Ignore();
        }
    }
}