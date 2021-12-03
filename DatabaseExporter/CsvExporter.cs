using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AutoMapper;
using CsvHelper;
using DatabaseExporter.Models;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

namespace DatabaseExporter
{
    public class CsvExporter
    {
        private readonly IMapper _mapper;
        private readonly IVariables _variables;
        private readonly IStrings _strings;
        private readonly IEntityContainer _entityContainer;

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

        public void Export()
        {
            OutputRouteNodes();
            OutputVariables();
            OutputStrings();
        }
        
        
        private void OutputRouteNodes()
        {
            using var writer = new StreamWriter("routenodes.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRouteNodeMap>();
            var routeNodes = _mapper.Map<List<CsvRouteNode>>(_entityContainer.RouteNodes);
            csv.WriteRecords(routeNodes);
        }
        
        private void OutputVariables()
        {
            using var writer = new StreamWriter("variables.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvVariableMap>();
            var variablesData = _mapper.Map<List<CsvDatabaseVariable>>(_variables.GetValues());
            csv.WriteRecords(variablesData);
        }
        
        private void OutputStrings()
        {
            using var writer = new StreamWriter("strings.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvDatabaseStringMap>();
            var stringsData = _mapper.Map<List<CsvDatabaseString>>(_strings.Entries);
            csv.WriteRecords(stringsData);
        }
    }
}