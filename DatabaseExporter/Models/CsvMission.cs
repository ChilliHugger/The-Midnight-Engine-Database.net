using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration;
using TME.Scenario.Default.Enums;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
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
    
    public sealed class CsvMissionMap : ClassMap<CsvMission>
    {
        public CsvMissionMap()
        {
            static string Transform(CsvMission m)
            {
                var output = string.Join("|", m.References.Select(c => c.Symbol));
                return output;
            }
            
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvMission
            Map(m => m.Priority).Index(4);
            Map(m => m.Objective).Index(5);
            Map(m => m.Condition).Index(6);
            Map(m => m.References ).Convert( o => Transform(o.Value) ).Index(7);
            Map(m => m.Points).Index(8);
            Map(m => m.Scorer.Symbol).Index(9).Name("Scorer");
            Map(m => m.Action).Index(10);
            Map(m => m.ActionId.Symbol).Index(11).Name("ActOn");
        }
    }
}