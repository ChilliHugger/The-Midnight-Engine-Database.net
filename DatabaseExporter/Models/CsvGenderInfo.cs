// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DatabaseExporter.Models
{
    public class CsvGenderInfo : CsvInfo
    {
        public string PersonalPronoun { get; set; } 
        public string PossessivePronoun { get; set; }
        public string SingularPronoun { get; set; }
    }
    
    public sealed class CsvGenderInfoMap : CsvClassMap<CsvGenderInfo>
    {
        public CsvGenderInfoMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.EntityFlags)).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvGenderInfo
            Map(m => m.PersonalPronoun).Index(5);
            Map(m => m.PossessivePronoun).Index(6);
            Map(m => m.SingularPronoun).Index(7);
        }
    }
}