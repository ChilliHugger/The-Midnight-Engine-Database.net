using System.Collections.Generic;
using CsvHelper.Configuration;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models.Item
{
    public class CsvMission : CsvEntity
    {
        public int Priority { get; set; }
        public MissionObjective Objective { get; set; }
        public MissionCondition Condition { get; set; }
        public List<CsvId> References { get; set; }
        public int Points { get; set; }
        public CsvId Scorer { get; set; }
        public MissionAction Action { get; set; }
        public CsvId ActionId { get; set; }
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