using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Autofac;
using CsvHelper;
using CsvHelper.Configuration;
using DatabaseExporter.Converters;
using DatabaseExporter.Models;
using DatabaseExporter.Models.Info;
using DatabaseExporter.Models.Item;
using Microsoft.Extensions.Logging;
using TME;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace DatabaseExporter
{
    public class CsvImporter
    {
        private readonly IDependencyContainer _container;

        private string _folder = "";

        private CsvEntityContainer _entityContainer;
        private CsvVariables _variables;
        private CsvStrings _strings;

        private TMEEntityResolver _entityResolver;
        private Dictionary<string, IEntity> _symbolCache;
        private CsvImportConverter _importConverter;
        
        private readonly List<Action> _finalActions = new ();

        
        public CsvImporter(IDependencyContainer container)
        {
            _container = container;
        }

        public void Process(IScenario scenario, string folder, string output)
        {
            _folder = folder;
            
            _variables = new CsvVariables();
            _strings = new CsvStrings();
            _entityContainer = new CsvEntityContainer();
            _entityResolver = new TMEEntityResolver(_entityContainer);
            _symbolCache = new Dictionary<string, IEntity>();
            _importConverter = new CsvImportConverter(_entityResolver, _strings);
            
            // misc
            Strings();
            Variables();

            // info
            AreaInfo();
            CommandInfo();
            DirectionInfo();
            GenderInfo();
            ObjectPowerInfo();
            ObjectTypeInfo();
            RaceInfo();
            TerrainInfo();
            UnitInfo();
            
            // Items
            RouteNodes();
            Waypoints();
            Missions();
            Victories();
            Strongholds();
            Regiments();
            Objects();
            Characters();

            // Final
            _entityContainer.SymbolCache = _symbolCache;
            
            _finalActions.ForEach(a => a());

            Save(scenario, output);
        }

        private bool Save(IScenario scenario, string output)
        {
            var path = Path.Combine(output, "database");
                
            using var writer = new TMEBinaryWriter(File.Open(path, FileMode.Create));

            var ctx = new SerializeContext(_entityResolver, _strings)
            {
                Writer = writer,
                Version = scenario.Info.DatabaseVersion,
                IsDatabase = true,
                IsSaveGame = false,
                Scenario = scenario
            };

            var logger = _container.CurrentContainer.Resolve<ILogger<TMEDatabase>>();
            var databaseWriter = new TMEDatabaseWriter(logger, new List<object>
            {
                _strings,
                _entityContainer,
                _variables
            });

            return databaseWriter.Save(ctx);
        }
        
        
        private StreamReader GetStream(string tag)
        {
            return new StreamReader(Path.Combine(_folder, tag) + ".tsv");
        }

        private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            Mode = CsvMode.NoEscape,
            Delimiter = "\t",
            ShouldQuote = _ => true
        };
        
        
        private IList<TIn> Import<TIn>(string tag)
        {
            using var reader = GetStream(tag);
            using var csv = new CsvReader(reader, _configuration);
            var results = csv.GetRecords<TIn>();
            return results.ToList();
        }

        //
        private void Strings()
        {
            var results = Import<CsvDatabaseString>(FileNames.Strings);
            var strings = results.Select(s => new DatabaseString
                {
                    Id = new MXId(EntityType.String, (uint) s.Id),
                    Symbol = s.Symbol,
                    Text = s.Text
                }
            );
            _strings.Entries = strings.ToList().AsReadOnly();
        }
        
        private void Variables()
        {
            var results = Import<CsvDatabaseVariable>(FileNames.Variables);
            var variables = results.Select(v => new DatabaseVariable
                {
                    Symbol = v.Symbol,
                    Value = v.Value
                }
            );
            _variables.Entries = variables;
        }
        
        void ImportThen<TIn, TOut>(string filename, Action<IReadOnlyList<TOut>> action)
            where TIn : CsvEntity
            where TOut : IEntity
        {
            var results = Import<TIn>(filename);
            var output = CreateCollection<TOut, TIn>(results);
            _finalActions.Add(() =>
            {
                action(output);
                Process<TIn,TOut>(results);
            });
        }

        private void Process<TIn, TOut>(IEnumerable<TIn> input)
            where TIn : CsvEntity
            where TOut : IEntity
        {
            foreach (var entity in input)
            {
                var resolved = _entityResolver.EntityById<TOut>((uint) entity.Id);
                if (resolved is IBundle item)
                {
                    var reader = new TMEBundleReader(entity.ToBundle(_importConverter), _entityResolver);
                    item.Load(reader);
                }
            }
        }
        
        private void RouteNodes()
        {
            ImportThen<CsvRouteNode, IRouteNode>(FileNames.RouteNodes, result =>
            {
                _entityContainer.RouteNodes = result ;
            });
        }


        private void Waypoints()
        {
            ImportThen<CsvWaypoint, IWaypoint>(FileNames.Waypoints, result =>
            {
                _entityContainer.Waypoints = result;
            });
        }

        private void Missions()
        {
            ImportThen<CsvMission, IMission>(FileNames.Missions, result =>
            {
                _entityContainer.Missions = result;
            });
        }
        
        private void Victories()
        {
            ImportThen<CsvVictory, IVictory>(FileNames.Victories, result =>
            {
                _entityContainer.Victories = result;
            });
        }
        
        private void Strongholds()
        {
            ImportThen<CsvStronghold, IStronghold>(FileNames.Strongholds, result =>
            {
                _entityContainer.Strongholds = result;
            });
        }
        
        private void Regiments()
        {
            ImportThen<CsvRegiment,IRegiment>(FileNames.Regiments, result =>
            {
                _entityContainer.Regiments = result;
            });
        }
        
        private void Objects()
        {
            ImportThen<CsvObject,IObject>(FileNames.Objects, result =>
            {
                _entityContainer.Things = result;
            });
        }
        
        private void Characters()
        {
            ImportThen<CsvCharacter,ICharacter>(FileNames.Characters, result =>
            {
                _entityContainer.Lords = result;
            });
        }
        
        private void AreaInfo()
        {
            ImportThen<CsvAreaInfo,AreaInfo>(FileNames.AreaInfo, result =>
            {
                _entityContainer.Areas = result;
            });
        }
        
        private void CommandInfo()
        {
            ImportThen<CsvCommandInfo,CommandInfo>(FileNames.CommandInfo, result =>
            {
                _entityContainer.Commands = result;
            });
        }
        
        private void DirectionInfo()
        {
            ImportThen<CsvDirectionInfo, DirectionInfo>(FileNames.DirectionInfo, result =>
            {
                _entityContainer.Directions = result;
            });
        }
        
        private void GenderInfo()
        {
            ImportThen<CsvGenderInfo, GenderInfo>(FileNames.GenderInfo, result =>
            {
                _entityContainer.Genders = result;
            });
        }
        
        private void ObjectPowerInfo()
        {
            ImportThen<CsvObjectPowerInfo, ObjectPowerInfo>(FileNames.ObjectPowerInfo, result =>
            {
                _entityContainer.ObjectPowers = result;
            });
        }
        
        private void ObjectTypeInfo()
        {
            ImportThen<CsvObjectTypeInfo, ObjectTypeInfo>(FileNames.ObjectTypeInfo, result =>
            {
                _entityContainer.ObjectTypes = result;
            });
        }
        
        private void RaceInfo()
        {
            ImportThen<CsvRaceInfo, RaceInfo>(FileNames.RaceInfo, result =>
            {
                _entityContainer.Races = result;
            });
        }
        
        private void TerrainInfo()
        {
            ImportThen<CsvTerrainInfo, TerrainInfo>(FileNames.TerrainInfo, result =>
            {
                _entityContainer.Terrains = result;
            });
        }
        
        private void UnitInfo()
        {
            ImportThen<CsvUnitInfo, UnitInfo>(FileNames.UnitInfo, result =>
            {
                _entityContainer.Units = result;
            });
        }
        
        private IReadOnlyList<TOut> CreateCollection<TOut,TIn>(ICollection<TIn> results)
            where TOut : IEntity
            where TIn : CsvEntity
        {
            var values = CreateCollection<TOut>(results.Count);

            if (values.Any())
            {
                var adjust = values.First().Type.IsZeroBased() ? 0 : 1;
                foreach (var item in results)
                {
                    _symbolCache[item.Symbol] = values[item.Id - adjust];
                }
            }

            return values.ToList().AsReadOnly();
        }
        
        private T[] CreateCollection<T>(int count)
            where T: IEntity
        {
            var result = new T[count];
            for(var ii=0; ii<count; ii++)
            {
                var entity = _container.CurrentScope != null
                    ? _container.CurrentScope.Resolve<T>()
                    : _container.CurrentContainer.Resolve<T>();
                result[ii] = entity;
            }
            return result.ToArray();
        }
        
    }
}