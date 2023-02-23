using System.Collections.Generic;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvMission : CsvEntity
    {
        public string Priority { get; set; }
        public string Objective { get; set; }
        public string Condition { get; set; }
        public string References { get; set; }
        public string Points { get; set; }
        public string Scorer { get; set; }
        public string Action { get; set; }
        
        [Name("Action Id")]
        public string ActionId { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Mission,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<MissionFlags>(Flags)},
            };
        }
    }
    
    public sealed class OutMissionMap : ClassMap<IMission>
    {
        public OutMissionMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvMission
            Map(m => m.Priority).Index(4);
            Map(m => m.Objective).Index(5);
            Map(m => m.Condition).Index(6);
            Map(m => m.References).Index(7);
            Map(m => m.Points).Index(8);
            Map(m => m.Scorer).Index(9);
            Map(m => m.Action).Index(10);
            Map(m => m.ActionId).Index(11).Name("Action Id");

            // Ignores
        }
    }
}