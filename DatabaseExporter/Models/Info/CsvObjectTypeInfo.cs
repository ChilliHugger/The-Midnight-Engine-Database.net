// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
{
    public class CsvObjectTypeInfo : CsvInfo
    {
    }
    
    public sealed class OutObjectTypeInfoMap : ClassMap<ObjectTypeInfo>
    {
        public OutObjectTypeInfoMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
        }
    }
}