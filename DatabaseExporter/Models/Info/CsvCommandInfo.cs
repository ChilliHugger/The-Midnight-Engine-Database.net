// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Serialize;

namespace DatabaseExporter.Models.Info
{
    public class CsvCommandInfo : CsvInfo
    {
        [Name("Success Time")]
        public string SuccessTime { get; set; }
        [Name("Failure Time")]
        public string FailureTime { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(CommandInfo.Id), converter.ToId(EntityType.CommandInfo,Id)},
                {nameof(CommandInfo.Symbol), Symbol},
                {nameof(CommandInfo.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(CommandInfo.Name), Name},
                {nameof(CommandInfo.SuccessTime), uint.Parse(SuccessTime)},
                {nameof(CommandInfo.FailureTime), uint.Parse(FailureTime)},
            };
        }
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