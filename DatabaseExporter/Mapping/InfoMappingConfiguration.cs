using System.Linq;
using AutoMapper;
using DatabaseExporter.Models;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Items;
using TME.Types;

namespace DatabaseExporter.Mapping
{
    public class InfoMappingConfiguration : MappingConfiguration
    {
        public InfoMappingConfiguration()
        {
            //CreateMap<Info, CsvInfo>(MemberList.None)
            //    .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //    .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //    .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<AreaInfo, CsvAreaInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<CommandInfo, CsvCommandInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<DirectionInfo, CsvDirectionInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<GenderInfo, CsvGenderInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<ObjectPowerInfo, CsvObjectPowerInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<ObjectTypeInfo, CsvObjectTypeInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<RaceInfo, CsvRaceInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.RidingMultiplier, b => b.MapFrom(c => c.RidingMultiplier / 10000.0));

            CreateMap<TerrainInfo, CsvTerrainInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<UnitInfo, CsvUnitInfo>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
        }
    }
}