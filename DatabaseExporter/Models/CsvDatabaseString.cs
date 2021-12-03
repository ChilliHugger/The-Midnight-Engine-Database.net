using CsvHelper.Configuration;
// ReSharper disable ClassNeverInstantiated.Global

namespace DatabaseExporter.Models
{

    public class CsvDatabaseString : CsvRecord
    {
        public string Text { get; set; }
    }

    public sealed class CsvDatabaseStringMap : ClassMap<CsvDatabaseString>
    {
        public CsvDatabaseStringMap()
        {
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            Map(m => m.Text).Index(3);
        }
    }
}