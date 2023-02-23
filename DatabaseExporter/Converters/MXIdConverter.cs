using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Extensions;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace DatabaseExporter.Converters
{
    public class MXIdConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is MXId id)
            {
                return id.RawId.ToString();
            }
            return "";
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var value =  Convert.ToUInt32(text, CultureInfo.InvariantCulture);
            var type = GetEntityType(memberMapData.TypeConverterOptions.Formats?.FirstOrDefault() ?? "");
            if (type == EntityType.None)
            {
                return new MXId(EntityType.None, value);
            }

            return !type.IsZeroBased() && value == 0
                ? MXId.None
                : new MXId(type, value);
        }
        
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private EntityType GetEntityType(string type)
        {
            return type.ToLower() switch
            {
                "string" => EntityType.String,
                "character" => EntityType.Character,
                "mission" => EntityType.Mission,
                "object" => EntityType.Thing,
                "regiment" => EntityType.Regiment,
                "routenode" => EntityType.RouteNode,
                "stronghold" => EntityType.Stronghold,
                "victory" => EntityType.Victory,
                "waypoint" => EntityType.Waypoint,
                "areainfo" => EntityType.AreaInfo,
                "commandinfo" => EntityType.CommandInfo,
                "directioninfo" => EntityType.DirectionInfo,
                "genderinfo" => EntityType.GenderInfo,
                "objectpower" => EntityType.ObjectPower,
                "objecttype" => EntityType.ObjectType,
                "raceinfo" => EntityType.RaceInfo,
                "terraininfo" => EntityType.TerrainInfo,
                "unitinfo" => EntityType.UnitInfo,
                _ => EntityType.None
            };
        }
    }
}