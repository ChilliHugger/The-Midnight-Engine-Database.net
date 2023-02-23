// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Serialize;

namespace DatabaseExporter.Models.Info
{
    public class CsvGenderInfo : CsvInfo
    {
        [Name("Personal Pronoun")]
        public string PersonalPronoun { get; set; } 
        [Name("Possessive Pronoun")]
        public string PossessivePronoun { get; set; }
        [Name("Singular Pronoun")]
        public string SingularPronoun { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.GenderInfo,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(GenderInfo.Name), Name},
                {nameof(GenderInfo.PersonalPronoun), PersonalPronoun},
                {nameof(GenderInfo.PossessivePronoun), PossessivePronoun},
                {nameof(GenderInfo.SingularPronoun), SingularPronoun},
            };
        }
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