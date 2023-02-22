// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
{
    public class CsvGenderInfo : CsvInfo
    {
        public string PersonalPronoun { get; set; } 
        public string PossessivePronoun { get; set; }
        public string SingularPronoun { get; set; }
    }
    
    public sealed class OutGenderInfoMap : ClassMap<GenderInfo>
    {
        public OutGenderInfoMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvGenderInfo
            Map(m => m.PersonalPronoun).Index(5).Name("Personal Pronoun");
            Map(m => m.PossessivePronoun).Index(6).Name("Possessive Pronoun");
            Map(m => m.SingularPronoun).Index(7).Name("Singular Pronoun");
        }
    }
}