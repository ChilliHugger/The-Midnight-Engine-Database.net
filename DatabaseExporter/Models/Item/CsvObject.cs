// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace DatabaseExporter.Models.Item
{
    public class CsvObject : CsvItem
    {
        public string Kills { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        [Name("Use Description")]
        public string UseDescription { get; set; }
        [Name("Carried By")]
        public string CarriedBy { get; set; }
        // ddr
        [Name("Type"), Optional]
        public string ObjectType { get; set; }
        [Name("Power"), Optional]
        public string ObjectPower { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            //var test = converter.ToEnum<ObjectPower>(ObjectPower);
            
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Object,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<ObjectFlags>(Flags)},
                {nameof(Object.Location), converter.ToLoc(Location)},
                
                {nameof(Object.Kills), converter.ToEnum<ThingType>(Kills)},
                {nameof(Object.Name), Name},
                {nameof(Object.Description), Description},
                {nameof(Object.UseDescription), converter.ToString(UseDescription)?.Id.RawId ?? 0}, 
                {nameof(Object.CarriedBy), converter.ToEntity<IItem>(CarriedBy)},
                {nameof(RevengeObject.ObjectType), converter.ToEnum<ObjectType>(ObjectType)},
                {nameof(RevengeObject.ObjectPower), converter.ToEnum<ObjectPower>(ObjectPower)},
            };
        }
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
            else
            {
                Map().Constant("").Index(10).Name("Type");
                Map().Constant("").Index(11).Name("Power");
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