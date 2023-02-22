using AutoMapper;
using DatabaseExporter.Models;
using DatabaseExporter.Models.Item;
using TME.Scenario.Default.Entities;

namespace DatabaseExporter.Mapping
{
    public class EntityMappingConfiguration : MappingConfiguration
    {
        public EntityMappingConfiguration()
        {
            // CreateMap<Mission, CsvMission>(MemberList.Destination)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<Victory, CsvVictory>(MemberList.Destination)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
        }
    }
}