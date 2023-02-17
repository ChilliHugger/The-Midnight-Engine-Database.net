using System.Linq;
using AutoMapper;
using DatabaseExporter.Models;
using TME.Scenario.Default.Items;

namespace DatabaseExporter.Mapping
{
    public class ItemMappingConfiguration : MappingConfiguration
    {
        public ItemMappingConfiguration()
        {
            //CreateMap<Item, CsvItem>(MemberList.None)
            //    .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
            //    .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
            //    .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            
            CreateMap<RouteNode, CsvRouteNode>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.Left, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.FirstOrDefault())))
                .ForMember(a => a.Right, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.LastOrDefault())));

            CreateMap<Waypoint, CsvWaypoint>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            
            CreateMap<Stronghold, CsvStronghold>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.TotalTroops, b => b.MapFrom(c => c.Total))
                .ForMember(a => a.MaxTroops, b => b.MapFrom(c => c.Max))
                .ForMember(a => a.MinTroops, b => b.MapFrom(c => c.Min));
            
            CreateMap<Regiment, CsvRegiment>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            
            CreateMap<Object, CsvObject>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
        }
    }
}