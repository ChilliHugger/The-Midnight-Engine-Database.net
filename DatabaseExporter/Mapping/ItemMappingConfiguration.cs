using System.Linq;
using AutoMapper;
using DatabaseExporter.Models;
using TME.Scenario.ddr;
using TME.Scenario.ddr.Items;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.Scenario;

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
                .ForMember(a => a.MinTroops, b => b.MapFrom(c => c.Min))
                .ForMember(a => a.Energy, b => b.Ignore());
            
            CreateMap<RevengeStronghold, CsvStronghold>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.TotalTroops, b => b.MapFrom(c => c.Total))
                .ForMember(a => a.MaxTroops, b => b.MapFrom(c => c.Max))
                .ForMember(a => a.MinTroops, b => b.MapFrom(c => c.Min))
                .ForMember(a => a.Energy, b => b.MapFrom(c => c.Energy));
            
            CreateMap<Regiment, CsvRegiment>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags));
            
            CreateMap<Object, CsvObject>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.ObjectType, b=> b.MapFrom(c=> ObjectType.None))
                .ForMember(a => a.ObjectPower, b=> b.MapFrom(c=> ObjectPower.None))
                .ForMember(a => a.UseDescription, b => b.Condition(c => c.UseDescription != 0));
            
            CreateMap<RevengeObject, CsvObject>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.UseDescription, b => b.Condition(c => c.UseDescription != 0));
                //.ForMember(a => a.ObjectType, b=> b.MapFrom(c=> ObjectType.None))
                //.ForMember(a => a.ObjectPower, b=> b.MapFrom(c=> ObjectPower.None));
                
            CreateMap<Character, CsvCharacter>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.DesiredObject, b=> b.Ignore())
                .ForMember(a => a.Home, b=> b.Ignore());

            CreateMap<RevengeCharacter, CsvCharacter>(MemberList.Destination)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.DesiredObject, b => b.MapFrom(c => c.DesiredObject))
                .ForMember(a => a.Home, b => b.MapFrom(c => c.HomeStronghold));
            
            CreateMap<IUnit, CsvUnit>(MemberList.None);
                //.ForMember(a => a.Type, b => b.MapFrom(c => c.Type));

        }
    }
}