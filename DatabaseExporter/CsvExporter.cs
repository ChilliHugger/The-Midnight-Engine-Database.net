using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using DatabaseExporter.Models;
using DatabaseExporter.Models.Info;
using DatabaseExporter.Models.Item;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Types;

// ReSharper disable StringLiteralTypo
namespace DatabaseExporter
{
    public class CsvExporter
    {
        private readonly IVariables _variables;
        private readonly IStrings _strings;
        private readonly IEntityContainer _entityContainer;
        private readonly IEntityResolver _entityResolver;
        private string _scenario;
        
        private string _folder = "";
        
        
        public CsvExporter(
            IVariables variables,
            IStrings strings,
            IEntityContainer entityContainer,
            IEntityResolver entityResolver)
        {
            _variables = variables;
            _strings = strings;
            _entityContainer = entityContainer;
            _entityResolver = entityResolver;
        }

        public void Process(string folder,string scenario)
        {
            _folder = folder;
            _scenario = scenario;
            
            // Misc
            Variables();
            Strings();
            
            // Items
            RouteNodes();
            Waypoints();
            Missions();
            Victories();
            Strongholds();
            Regiments();
            Objects();
            Characters();
            
            // Infos
            AreaInfo();
            CommandInfo();
            DirectionInfo();
            GenderInfo();
            ObjectPowerInfo();
            ObjectTypeInfo();
            RaceInfo();
            TerrainInfo();
            UnitInfo();
        }

        private StreamWriter GetStream(string tag)
        {
            return new StreamWriter( Path.Combine(_folder,tag) + ".tsv");
        }

        private readonly CsvConfiguration _configuration = new (CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Mode = CsvMode.NoEscape,
            Delimiter = "\t",
            ShouldQuote = _ => true
            
        };
        
        private void Export<TOut,TMap>(string tag, IEnumerable<TOut> values)
            where TMap : ClassMap
        {
            var map = (TMap) Activator.CreateInstance(typeof(TMap));
            Export(tag,values,map);
        }
        
        private void Export<TOut>(string tag, IEnumerable<TOut> values, ClassMap map)
        {
            using var writer = GetStream(tag);
            using var csv = new CsvWriter(writer, _configuration);
            var enumConverter = new EnumConverter(_entityResolver);
            csv.Context.TypeConverterCache.AddConverter<Race>(enumConverter);
            csv.Context.TypeConverterCache.AddConverter<UnitType>(enumConverter);
            csv.Context.TypeConverterCache.AddConverter<Gender>(enumConverter);
            csv.Context.TypeConverterCache.AddConverter<Direction>(enumConverter);
            csv.Context.TypeConverterCache.AddConverter<Terrain>(enumConverter);
            csv.Context.TypeConverterCache.AddConverter<Command>(enumConverter);
            
            csv.Context.TypeConverterCache.AddConverter<MXId>(new MXIdConverter());
            csv.Context.TypeConverterCache.AddConverter<Loc>(new LocationConverter());
            csv.Context.TypeConverterCache.AddConverter<IRouteNodes>(new RouteNodesConverter());
            csv.Context.TypeConverterCache.AddConverter<uint>(new StringIdConverter(_strings));
            csv.Context.TypeConverterCache.AddConverter<IEntity>(new EntityConverter<IEntity>());
            csv.Context.TypeConverterCache.AddConverter<ICharacter>(new EntityConverter<ICharacter>());
            csv.Context.TypeConverterCache.AddConverter<IStronghold>(new EntityConverter<IStronghold>());
            csv.Context.TypeConverterCache.AddConverter<IObject>(new EntityConverter<IObject>());
            csv.Context.TypeConverterCache.AddConverter<IMission>(new EntityConverter<IMission>());
            csv.Context.TypeConverterCache.AddConverter<IItem>(new EntityConverter<IItem>());
            csv.Context.TypeConverterCache.AddConverter<IList<IEntity>>(new EntityListConverter<IList<IEntity>,IEntity>());
            csv.Context.TypeConverterCache.AddConverter<IReadOnlyList<IObject>>(new EntityListConverter<IReadOnlyList<IObject>,IObject>());
            
            csv.Context.TypeConverterCache.AddConverter<LordTraits>(new FlagConverter<LordTraits>());
            csv.Context.TypeConverterCache.AddConverter<EntityFlags>(new FlagConverter<EntityFlags>());
            csv.Context.TypeConverterCache.AddConverter<LordFlags>(new FlagConverter<LordFlags>());
            csv.Context.TypeConverterCache.AddConverter<RegimentFlags>(new FlagConverter<RegimentFlags>());
            csv.Context.TypeConverterCache.AddConverter<ThingFlags>(new FlagConverter<ThingFlags>());
            csv.Context.TypeConverterCache.AddConverter<MissionFlags>(new FlagConverter<MissionFlags>());
            csv.Context.TypeConverterCache.AddConverter<VictoryFlags>(new FlagConverter<VictoryFlags>());
            csv.Context.TypeConverterCache.AddConverter<TerrainInfoFlags>(new FlagConverter<TerrainInfoFlags>());
            
            csv.Context.RegisterClassMap(map);
            csv.WriteRecords(values);
        }
        
        private void RouteNodes()
        {
            Export<IRouteNode,OutRouteNodeMap>("routenodes",_entityContainer.RouteNodes);
        }
        
        private void Waypoints()
        {
            Export<IWaypoint,OutWaypointMap>("waypoints",_entityContainer.Waypoints);
        }
        
        private void Missions()
        {
            Export<IMission,OutMissionMap>("missions",_entityContainer.Missions);
        }
        
        private void Victories()
        {
            Export<IVictory,OutVictoryMap>("victories",_entityContainer.Victories);
        }
        
        private void Strongholds()
        {
            if (_scenario == RevengeScenario.Tag)
            {
                Export<IRevengeStronghold,OutStrongholdMap<IRevengeStronghold>>
                    ("strongholds", _entityContainer.Strongholds.Cast<IRevengeStronghold>());
            }
            else
            {
                Export<IStronghold,OutStrongholdMap<IStronghold>>
                    ("strongholds", _entityContainer.Strongholds);
            }
        }
        
        private void Regiments()
        {
            Export<IRegiment,OutRegimentMap>("regiments",_entityContainer.Regiments);
        }
        
        private void Objects()
        {
            if (_scenario == RevengeScenario.Tag)
            {
                Export<IRevengeThing,OutObjectMap<IRevengeThing>>
                    ("objects", _entityContainer.Things.Cast<IRevengeThing>());
            }
            else
            {
                Export<IObject,OutObjectMap<IObject>>("objects", _entityContainer.Things);
            }
        }
        
        private void Characters()
        {
            if (_scenario == RevengeScenario.Tag)
            {
                Export<IRevengeLord,OutCharacterMap<IRevengeLord>>
                    ("characters", _entityContainer.Lords.Cast<IRevengeLord>());
            }
            else
            {
                Export<ICharacter,OutCharacterMap<ICharacter>>
                    ("characters", _entityContainer.Lords);
            }
        }
        
        private void Variables()
        {
            Export<DatabaseVariable, OutVariableMap>("variables", _variables.GetValues().OrderBy(c=>c.Symbol));
        }
        
        private void Strings()
        {
            Export<DatabaseString,OutDatabaseStringMap>("strings", _strings.Entries.OrderBy(c=>c.Id.RawId).ToList());
        }
        
        // Info
        private void AreaInfo()
        {
            Export<AreaInfo,OutAreaInfoMap>("areainfo", _entityContainer.Areas);
        }
        
        private void CommandInfo()
        {
            Export<CommandInfo,OutCommandInfoMap>("commandinfo", _entityContainer.Commands);
        }
        
        private void DirectionInfo()
        {
            Export<DirectionInfo,OutDirectionInfoMap>("directioninfo", _entityContainer.Directions);
        }
        
        private void GenderInfo()
        {
            Export<GenderInfo,OutGenderInfoMap>("genderinfo", _entityContainer.Genders);
        }
        
        private void ObjectPowerInfo()
        {
            Export<ObjectPowerInfo,OutObjectPowerInfoMap>("objectpowerinfo", _entityContainer.ObjectPowers);
        }
        
        private void ObjectTypeInfo()
        {
            Export<ObjectTypeInfo,OutObjectTypeInfoMap>("objecttypeinfo", _entityContainer.ObjectTypes);
        }
        
        private void RaceInfo()
        {
            Export<RaceInfo,OutRaceInfoMap>("raceinfo", _entityContainer.Races);
        }
        
        private void TerrainInfo()
        {
            Export<TerrainInfo,OutTerrainInfoMap>("terraininfo", _entityContainer.Terrains);
        }
        
        private void UnitInfo()
        {
            Export<UnitInfo,OutUnitInfoMap>("unitinfo", _entityContainer.Units);
        }
    }
}