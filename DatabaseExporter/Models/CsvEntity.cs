// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using TME.Scenario.Default.Flags;

namespace DatabaseExporter.Models
{
    public class CsvEntity : CsvRecord
    {
        public ulong Flags { get; set; }
        
        // for export
        public EntityFlags EntityFlags => (EntityFlags) Flags;
    }
}