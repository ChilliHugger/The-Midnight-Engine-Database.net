using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Types;

// ReSharper disable ClassNeverInstantiated.Global
namespace DatabaseExporter.Models
{
    public class CsvDatabaseString : CsvRecord
    {
        public string Text { get; set; }
    }
    
    public sealed class OutDatabaseStringMap : ClassMap<DatabaseString>
    {
        public OutDatabaseStringMap()
        {
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            Map(m => m.Text)
                .TypeConverter<MultiLineStringConverter>()
                .Index(3);
        }
    }
}