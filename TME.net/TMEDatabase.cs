using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Autofac;
using Microsoft.Extensions.Logging;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class TMEDatabase : IDatabase
    {
        private static readonly ID_4CC TMEMagicNo = ID_4CC.FromSig('T', 'M', 'E', '!');

        private readonly ILogger _logger;
        private readonly IEngine _engine;
        private readonly IVariables _variables;
        private readonly IStrings _strings;
        private readonly IContainer _container;
        private readonly ISerializeContext _serializeContext;
        private readonly IEntityContainer _entityContainer;
        
        public string Directory { get; set; } = "";

        public uint ScenarioId { get; private set; }
        public uint Version { get; private set; }
        public string Header { get; private set; } = "";
        public string Description { get; private set; } = "";
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
            var scenario = _engine.Scenario;
            var scenarioInfo = scenario.Info;
            var path = Path.Combine(Directory, "database");

            // TODO: DI
            using (var reader = new TMEBinaryReader(File.Open(path, FileMode.Open)))
            {
                // chunk 0
                _serializeContext.Section = DataSection.None;
                
                var magicNo = reader.UInt32();
                if (magicNo != TMEMagicNo)
                {
                    var number = BinaryPrimitives.ReverseEndianness(TMEMagicNo);
                    if (magicNo != number)
                    {
                        _logger.LogError($"Database MagicNo '{magicNo}' does not match '{TMEMagicNo}'");
                        return false;
                    }

                    reader.EnableByteSwap = true;
                }

                // chunk 1
                _serializeContext.Section = DataSection.Header;

                ScenarioId = reader.UInt32();
                if (ScenarioId != scenarioInfo.Id)
                {
                    _logger.LogError($"Database ScenarioId '{ScenarioId}' does not match '{scenarioInfo.Id}'");
                    return false;
                }

                Version = reader.UInt32();
                if (Version < scenarioInfo.DatabaseVersion)
                {
                    _logger.LogError(
                        $"Database Version '{Version}' is less than scenario version '{scenarioInfo.DatabaseVersion}'");
                    return false;
                }
                
                Header = reader.String();
                
                // chunk 2
                _serializeContext.Section = DataSection.SaveGameHeader;
                // Description = "";
                // if (_serializeContext.IsSaveGame)
                // {
                //     Description = Version > 8
                //         ? reader.ReadString()
                //         : "";
                // }

                _serializeContext.Reader = reader;
                _serializeContext.Version = Version;
                _serializeContext.IsDatabase = true;
                _serializeContext.IsSaveGame = false;
                _serializeContext.Scenario = scenario;
                
                var readers = new List<object>()
                {
                    _entityContainer,
                    _strings,
                    _variables,
                };

                // TODO: Loop until data exhausted
                var sections = new List<DataSection> 
                {
                    DataSection.Entities,
                    DataSection.Strings,
                    DataSection.Variables,
                    DataSection.ObjectInfo
                };
                
                foreach (var section in sections)
                {
                    _serializeContext.Section = section;
                    foreach (var r in readers)
                    {
                        if (r is ISerializable item)
                        {
                            if (!item.Load(_serializeContext))
                            {
                                throw new FileLoadException($"Error in {r}");
                            }
                        }
                    }
                }

                scenario.InitialiseAfterGameLoad();
            }

            return LoadMap();
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
