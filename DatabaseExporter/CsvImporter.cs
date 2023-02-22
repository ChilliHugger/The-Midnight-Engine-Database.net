using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using DatabaseExporter.Models;
using TME.Interfaces;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace DatabaseExporter
{
    public class CsvImporter
    {
        private readonly IMapper _mapper;

        private string _folder = "";

        public CsvImporter(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Process(string folder)
        {
            _folder = folder;

            // misc
            Strings();
            Variables();

            // Items
            //RouteNodes();
            //Waypoints();
            //Missions();
            //Victories();
            //Strongholds();
            //Regiments();
            //Objects();
            Characters();
            
            // info
            AreaInfo();
            //CommandInfo();
            //DirectionInfo();
            //GenderInfo();
            //ObjectPowerInfo();
            //ObjectTypeInfo();
            //RaceInfo();
            //TerrainInfo();
            //UnitInfo();
        }

        private StreamReader GetStream(string tag)
        {
            return new StreamReader(Path.Combine(_folder, tag) + ".tsv");
        }

        private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Mode = CsvMode.NoEscape,
            Delimiter = "\t",
            ShouldQuote = args => true
        };

        private IEnumerable<TOut> Import<TIn,TOut>(string tag, ClassMap classMap)
        {
            using var reader = GetStream(tag);
            using var csv = new CsvReader(reader, _configuration);
            csv.Context.RegisterClassMap(classMap);
            var results = csv.GetRecords<TIn>();
            return _mapper.Map<List<TOut>>(results);
        }

        //
        private void Strings()
        {
            //var results = Import<CsvDatabaseString,DatabaseString>("strings");

        }
        private void Variables()
        {
            //var results = Import<CsvDatabaseVariable,DatabaseVariable>("variables");
        }
     
        private void AreaInfo()
        {
            //var results = Import<CsvAreaInfo,AreaInfo>("areainfo", new CsvAreaInfoMap());
        }

        private void Characters()
        {
            //var results = Import<CsvCharacter,ICharacter>("characters", new InCharacterMap());
        }
        
    }
}