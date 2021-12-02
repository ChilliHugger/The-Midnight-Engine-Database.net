using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using DatabaseExporter.Models;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;

namespace DatabaseExporter.Mapping
{
    public class MappingConfiguration : MapperConfigurationExpression
    {
        private const int CsvExportVersion = 1;
        
        private static string GetSymbol(IEntity node)
        {
            return node?.Symbol ?? "";
        }

        public MappingConfiguration()
        {
            CreateMap<RouteNode, CsvRouteNode>(MemberList.None)
                .ForMember(a => a.Version, b=> b.MapFrom(c=> CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                //.ForMember(a => a.Symbol, b => b.MapFrom(c => c.Symbol))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                //.ForMember(a => a.Location, b => b.MapFrom(c => c.Location))
                .ForMember(a => a.Left, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.FirstOrDefault())))
                .ForMember(a => a.Right, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.LastOrDefault())));

        }
    }
}
