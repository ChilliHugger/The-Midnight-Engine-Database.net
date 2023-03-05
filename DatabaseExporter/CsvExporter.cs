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

        public void Process(string folder, string scenario)
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
            var converterFactory = new CsvConverterFactory(_strings, _entityResolver);
            converterFactory.InjectConverters(csv.Context.TypeConverterCache);
            csv.Context.RegisterClassMap(map);
            csv.WriteRecords(values);
        }
        
        private void RouteNodes()
        {
            Export<IRouteNode,OutRouteNodeMap>(FileNames.RouteNodes,_entityContainer.RouteNodes);
        }
        
        private void Waypoints()
        {
            Export<IWaypoint,OutWaypointMap>(FileNames.Waypoints,_entityContainer.Waypoints);
        }
        
        private void Missions()
        {
            Export<IMission,OutMissionMap>(FileNames.Missions,_entityContainer.Missions);
        }
        
        private void Victories()
        {
            Export<IVictory,OutVictoryMap>(FileNames.Victories,_entityContainer.Victories);
        }
        
        private void Strongholds()
        {
            if (_scenario == RevengeScenario.Tag)
            {
                Export<IRevengeStronghold,OutStrongholdMap<IRevengeStronghold>>
                    (FileNames.Strongholds, _entityContainer.Strongholds.Cast<IRevengeStronghold>());
            }
            else
            {
                Export<IStronghold,OutStrongholdMap<IStronghold>>
                    (FileNames.Strongholds, _entityContainer.Strongholds);
            }
        }
        
        private void Regiments()
        {
            Export<IRegiment,OutRegimentMap>(FileNames.Regiments,_entityContainer.Regiments);
        }
        
        private void Objects()
        {
            Export<IObject,OutObjectMap<IObject>>(FileNames.Objects, _entityContainer.Things);
        }
        
        private void Characters()
        {
            if (_scenario == RevengeScenario.Tag)
            {
                Export<IRevengeLord,OutCharacterMap<IRevengeLord>>
                    (FileNames.Characters, _entityContainer.Lords.Cast<IRevengeLord>());
            }
            else
            {
                Export<ICharacter,OutCharacterMap<ICharacter>>
                    (FileNames.Characters, _entityContainer.Lords);
            }
        }
        
        private void Variables()
        {
            Export<DatabaseVariable, OutVariableMap>(FileNames.Variables, _variables.GetValues().OrderBy(c=>c.Symbol));
        }
        
        private void Strings()
        {
            Export<DatabaseString,OutDatabaseStringMap>(FileNames.Strings, _strings.Entries.OrderBy(c=>c.Id.RawId).ToList());
        }
        
        // Info
        private void AreaInfo()
        {
            Export<AreaInfo,OutAreaInfoMap>(FileNames.AreaInfo, _entityContainer.Areas);
        }
        
        private void CommandInfo()
        {
            Export<CommandInfo,OutCommandInfoMap>(FileNames.CommandInfo, _entityContainer.Commands);
        }
        
        private void DirectionInfo()
        {
            Export<DirectionInfo,OutDirectionInfoMap>(FileNames.DirectionInfo, _entityContainer.Directions);
        }
        
        private void GenderInfo()
        {
            Export<GenderInfo,OutGenderInfoMap>(FileNames.GenderInfo, _entityContainer.Genders);
        }
        
        private void ObjectPowerInfo()
        {
            Export<ObjectPowerInfo,OutObjectPowerInfoMap>(FileNames.ObjectPowerInfo, _entityContainer.ObjectPowers);
        }
        
        private void ObjectTypeInfo()
        {
            Export<ObjectTypeInfo,OutObjectTypeInfoMap>(FileNames.ObjectTypeInfo, _entityContainer.ObjectTypes);
        }
        
        private void RaceInfo()
        {
            Export<RaceInfo,OutRaceInfoMap>(FileNames.RaceInfo, _entityContainer.Races);
        }
        
        private void TerrainInfo()
        {
            Export<TerrainInfo,OutTerrainInfoMap>(FileNames.TerrainInfo, _entityContainer.Terrains);
        }
        
        private void UnitInfo()
        {
            Export<UnitInfo,OutUnitInfoMap>(FileNames.UnitInfo, _entityContainer.Units);
        }
    }
}