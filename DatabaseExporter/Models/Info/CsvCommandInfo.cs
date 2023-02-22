// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;

namespace DatabaseExporter.Models.Info
{
    public class CsvCommandInfo : CsvInfo
    {
        public uint SuccessTime { get; set; }
        public uint FailureTime { get; set; }
    }
    
    public sealed class OutCommandInfoMap : ClassMap<CommandInfo>
    {
        public OutCommandInfoMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvInfo
            Map(m => m.Name).Index(4);
            // CsvCommandInfo
            Map(m => m.SuccessTime).Index(5).Name("Success Time");
            Map(m => m.FailureTime).Index(6).Name("Failure Time");
        }
    }
}