using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Autofac;
using Microsoft.Extensions.Logging;
using TME.Interfaces;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class TMEDatabase : IDatabase
    {
        private readonly ILogger<TMEDatabase> _logger;
        private readonly IEngine _engine;
        private readonly IVariables _variables;
        private readonly IStrings _strings;
        private readonly IContainer _container;
        private readonly ISerializeContext _serializeContext;
        private readonly IEntityContainer _entityContainer;
        
        public string Directory { get; set; } = "";
        public uint ScenarioId { get; private set; }
        public uint Version { get; private set; }

        public IMap GameMap { get; set; }

        public TMEDatabase(
            ILogger<TMEDatabase> logger,
            IEngine engine,
            IVariables variables,
            IStrings strings,
            IEntityContainer entityContainer,
            ISerializeContext serializeContext,
            IDependencyContainer container)
        {
            _container = container.CurrentContainer;
            _logger = logger;
            _engine = engine;
            _variables = variables;
            _strings = strings;
            _entityContainer = entityContainer;
            _serializeContext = serializeContext;
            
            GameMap = _container.Resolve<IMap>();
        }

        public bool Load()
        {
            var path = Path.Combine(Directory, "database");

            using var reader = new TMEBinaryReader(File.Open(path, FileMode.Open));
            
            _serializeContext.Reader = reader;
            _serializeContext.Version = Version;
            _serializeContext.IsDatabase = true;
            _serializeContext.IsSaveGame = false;
            _serializeContext.Scenario = _engine.Scenario;

            var readers = new List<object>
            {
                _strings,
                _entityContainer,
                _variables
            };
                
            var databaseReader = new TMEDatabaseReader(_logger, readers);

            if (!databaseReader.Load(_serializeContext)) return false;

            ScenarioId = databaseReader.ScenarioId;
            Version = databaseReader.Version;
       
            _serializeContext.Scenario.InitialiseAfterGameLoad();

            return true;
            //return LoadMap();
        }

        private bool Save()
        {
            var path = Path.Combine(Directory, "database");

            using var writer = new TMEBinaryWriter(File.Open(path, FileMode.Create));
            
            _serializeContext.Writer = writer;
            _serializeContext.Version = Version;
            _serializeContext.IsDatabase = true;
            _serializeContext.IsSaveGame = false;
            _serializeContext.Scenario = _engine.Scenario;
                
            var databaseWriter = new TMEDatabaseWriter(_logger, new List<object>
            {
                _strings,
                _entityContainer,
                _variables
            });

            return databaseWriter.Save(_serializeContext);
        }
        
        
        private bool LoadMap()
        {
            var path = Path.Combine(Directory, "map");
            using var reader = new TMEBinaryReader(File.Open(path, FileMode.Open));
            GameMap = _container.Resolve<IMap>();
            if (GameMap is TMEMap map)
            {
                map.MistEnabled = _engine.Scenario.Info.IsFeature(FeatureFlags.Mist);
                map.TunnelsEnabled = _engine.Scenario.Info.IsFeature(FeatureFlags.Tunnels);
            }
            GameMap.LoadFullMapFromStream(reader);
            return true;
        }


        public bool Unload()
        {
            throw new NotImplementedException();
        }
        
    }
}
