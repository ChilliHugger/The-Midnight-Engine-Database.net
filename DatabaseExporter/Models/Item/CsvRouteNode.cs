// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace DatabaseExporter.Models.Item
{
    public class CsvRouteNode : CsvItem
    {
        public string RouteNodes { get; set; }

        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(RouteNode.Id), converter.ToId(EntityType.RouteNode,Id)},
                {nameof(RouteNode.Symbol), Symbol},
                {nameof(RouteNode.Flags), converter.ToFlags<EntityFlags>(Flags)},
                {nameof(RouteNode.Location), converter.ToLoc(Location)},
                {nameof(RouteNode.RouteNodes.Nodes), converter.ToArray<IRouteNode>(RouteNodes)}
            };
        }
    }
    
    public sealed class OutRouteNodeMap : ClassMap<IRouteNode>
    {
        public OutRouteNodeMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRouteNode
            Map(m => m.RouteNodes).Index(5);
        }
    }
}