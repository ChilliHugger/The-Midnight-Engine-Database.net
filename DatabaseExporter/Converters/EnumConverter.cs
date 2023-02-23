using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter.Converters
{
    public class EnumConverter : DefaultTypeConverter
    {
        private readonly IEntityResolver _entityResolver;
        
        public EnumConverter(IEntityResolver entityResolver)
        {
            _entityResolver = entityResolver;
        }
        
        string Resolve<TInfo,T>(T enumValue)
            where T : Enum 
            where TInfo : IEntity
        {
            return _entityResolver.EntityById<TInfo>(enumValue.Raw())?.Symbol;
        }
        
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is Race race)
            {
                return Resolve<RaceInfo,Race>(race);
            }
            if (value is UnitType unitType)
            {
                return Resolve<UnitInfo,UnitType>(unitType);
            }
            if (value is Gender gender)
            {
                return Resolve<GenderInfo,Gender>(gender);
            }
            if (value is Direction direction)
            {
                return Resolve<DirectionInfo,Direction>(direction);
            }
            if (value is Terrain terrain)
            {
                return Resolve<TerrainInfo,Terrain>(terrain);
            }
            if (value is Command command)
            {
                return Resolve<CommandInfo,Command>(command);
            }
            //if (value is Orders orders)
            //{
            //    return Resolve<OrdersI,Command>(command);
            //}
            return "";
        }
    }
}