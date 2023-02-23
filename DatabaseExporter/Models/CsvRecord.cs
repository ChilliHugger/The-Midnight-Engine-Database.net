// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using DatabaseExporter.Converters;
using TME.Serialize;

namespace DatabaseExporter.Models
{
    public class CsvRecord
    {
        public int Version { get; set; }
        public int Id { get; set; }
        public string Symbol { get; set; }

        public virtual Bundle ToBundle(CsvImportConverter converter)
        {
            return null;
        }
    }
}