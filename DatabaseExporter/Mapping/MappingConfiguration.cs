using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using DatabaseExporter.Models;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Types;

namespace DatabaseExporter.Mapping
{
    public class MappingConfiguration : MapperConfigurationExpression
    {
        private const int CsvExportVersion = 1;
        
        private static string GetSymbol(IEntity node)
        {
            return node?.Symbol ?? "";
        }

        private static string ConvertString(string value)
        {
            return value.Replace("\n\r", "{crlf}")
                .Replace("\n", "{cr}")
                .Replace("\r", "{lf}")
                ;
        }
        
        public MappingConfiguration()
        {
            CreateMap<RouteNode, CsvRouteNode>(MemberList.None)
                .ForMember(a => a.Version, b=> b.MapFrom(c=> CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Flags, b => b.MapFrom(c => c.RawFlags))
                .ForMember(a => a.Left, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.FirstOrDefault())))
                .ForMember(a => a.Right, b => b.MapFrom(c => GetSymbol(c.RouteNodes.Nodes.LastOrDefault())));

            CreateMap<DatabaseString, CsvDatabaseString>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion))
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id.RawId))
                .ForMember(a => a.Text, b => b.MapFrom(c => ConvertString(c.Text)));
            
            CreateMap<DatabaseVariable, CsvDatabaseVariable>(MemberList.None)
                .ForMember(a => a.Version, b => b.MapFrom(c => CsvExportVersion));
        }
    }
}
