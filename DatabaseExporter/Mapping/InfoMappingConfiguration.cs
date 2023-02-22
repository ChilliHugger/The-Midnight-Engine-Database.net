using System;
using System.Linq;
using AutoMapper;
using DatabaseExporter.Models;
using DatabaseExporter.Models.Info;
using TME.Scenario.Default.Enums;
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

            // CreateMap<AreaInfo, CsvAreaInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));

            CreateMap<CsvAreaInfo, AreaInfo>(MemberList.Destination)
                .ForMember(a => a.Type, b => b.Ignore())
                .ForMember(a => a.UserData, b => b.Ignore())
                .ForMember(a => a.Id, b => b.MapFrom(c => new MXId(EntityType.AreaInfo, (uint) c.Id)))
                .ForMember(a => a.Symbol, b => b.MapFrom(c => c.Symbol))
                .ForMember(a => a.RawFlags, b => b.MapFrom(c => c.Flags))
                .ForMember(a => a.Name, b => b.MapFrom(c => c.Name))
                .ForMember(a => a.Prefix, b => b.MapFrom(c => c.Prefix));

            // CreateMap<CommandInfo, CsvCommandInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<DirectionInfo, CsvDirectionInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<GenderInfo, CsvGenderInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<ObjectPowerInfo, CsvObjectPowerInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<ObjectTypeInfo, CsvObjectTypeInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<RaceInfo, CsvRaceInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
            //     .ForMember(a => a.RidingMultiplier, b => b.MapFrom(c => c.RidingMultiplier / 10000.0));
            //
            // CreateMap<TerrainInfo, CsvTerrainInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            //
            // CreateMap<UnitInfo, CsvUnitInfo>(MemberList.None)
            //     .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //     .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //     .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
        }
    }
}