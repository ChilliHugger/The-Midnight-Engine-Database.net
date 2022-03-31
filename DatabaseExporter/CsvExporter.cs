using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AutoMapper;
using CsvHelper;
using DatabaseExporter.Models;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

// ReSharper disable StringLiteralTypo
namespace DatabaseExporter
{
    public class CsvExporter
    {
        private readonly IMapper _mapper;
        private readonly IVariables _variables;
        private readonly IStrings _strings;
        private readonly IEntityContainer _entityContainer;

        private string _folder = "";
        
        public CsvExporter(
            IMapper mapper,
            IVariables variables,
            IStrings strings,
            IEntityContainer entityContainer)
        {
            _mapper = mapper;
            _variables = variables;
            _strings = strings;
            _entityContainer = entityContainer;
        }

        public void Export(string folder)
        {
            _folder = folder;
            
            OutputVariables();
            OutputStrings();
            
            // items
            OutputRouteNodes();
            
            // Infos
            OutputAreaInfo();
            OutputCommandInfo();
            OutputDirectionInfo();
            OutputGenderInfo();
            OutputObjectPowerInfo();
            OutputObjectTypeInfo();
            OutputRaceInfo();
            OutputTerrainInfo();
            OutputUnitInfo();
        }

        private StreamWriter GetStream(string tag)
        {
            return new StreamWriter( Path.Combine(_folder,tag) + ".csv");
        }
        
        private void OutputRouteNodes()
        {
            using var writer = GetStream("routenodes");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRouteNodeMap>();
            csv.WriteRecords(_mapper.Map<List<CsvRouteNode>>(_entityContainer.RouteNodes));
        }
        
        private void OutputVariables()
        {
            using var writer = GetStream("variables");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvVariableMap>();
            csv.WriteRecords(_mapper.Map<List<CsvDatabaseVariable>>(_variables.GetValues()));
        }
        
        private void OutputStrings()
        {
            using var writer = GetStream("strings");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvDatabaseStringMap>();
            csv.WriteRecords(_mapper.Map<List<CsvDatabaseString>>(_strings.Entries));
        }
        
        // Info
        private void OutputAreaInfo()
        {
            using var writer = GetStream("areainfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvAreaInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvAreaInfo>>(_entityContainer.Areas));
        }
        
        private void OutputCommandInfo()
        {
            using var writer = GetStream("commandinfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvCommandInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvCommandInfo>>(_entityContainer.Commands));
        }
        
        private void OutputDirectionInfo()
        {
            using var writer = GetStream("directioninfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvDirectionInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvDirectionInfo>>(_entityContainer.Directions));
        }
        
        private void OutputGenderInfo()
        {
            using var writer = GetStream("genderinfo.");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvGenderInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvGenderInfo>>(_entityContainer.Genders));
        }
        
        private void OutputObjectPowerInfo()
        {
            using var writer = GetStream("objectpowerinfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvObjectPowerInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvObjectPowerInfo>>(_entityContainer.ObjectPowers));
        }
        
        private void OutputObjectTypeInfo()
        {
            using var writer = GetStream("objecttypeinfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvObjectTypeInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvObjectTypeInfo>>(_entityContainer.ObjectTypes));
        }
        
        private void OutputRaceInfo()
        {
            using var writer = GetStream("raceinfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRaceInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvRaceInfo>>(_entityContainer.Races));
        }
        
        private void OutputTerrainInfo()
        {
            using var writer = GetStream("terraininfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvTerrainInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvTerrainInfo>>(_entityContainer.Terrains));
        }
        
        private void OutputUnitInfo()
        {
            using var writer = GetStream("unitinfo");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvUnitInfoMap>();
            csv.WriteRecords(_mapper.Map<List<CsvUnitInfo>>(_entityContainer.Units));
        }
    }
}