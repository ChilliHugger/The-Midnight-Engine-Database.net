using AutoMapper;
using DatabaseExporter.Models;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace DatabaseExporter.Mapping
{
    public class GeneralMappingConfiguration : MappingConfiguration
    {
        private readonly IStrings _strings;
        public GeneralMappingConfiguration(IStrings strings)
        {
            _strings = strings;
            
            CreateMap<uint, CsvId>(MemberList.None)
                .ForMember(a => a.Id, b => b.MapFrom(c => c))
                .ForMember(a => a.Symbol, b => b.MapFrom(c => GetString(c)));
            
            CreateMap<IEntity, CsvId>(MemberList.None)
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id))
                .ForMember(a => a.Symbol, b => b.MapFrom(c => c.Symbol));
            
            CreateMap<DatabaseString, CsvId>(MemberList.None)
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id))
                .ForMember(a => a.Symbol, b => b.MapFrom(c => c.Symbol));
            
            CreateMap<DatabaseString, CsvDatabaseString>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Text, b => b.MapFrom(c => ConvertString(c.Text)));
            
            CreateMap<DatabaseVariable, CsvDatabaseVariable>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Value, b => b.MapFrom(c => c.Value))

                ;
        }
        
        private string GetString(uint id)
        {
            var stringId = new MXId(EntityType.String, id);
            return _strings.GetById(stringId)?.Symbol ?? "";
        }
    }
}