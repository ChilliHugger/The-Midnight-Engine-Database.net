using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Autofac;
using TME.Interfaces;
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
            IVariables variables,
            IStrings strings,
            IEntityContainer entityContainer,
            ISerializeContext serializeContext,
            IDependencyContainer container)
        {
            _container = container.CurrentContainer;
            _variables = variables;
            _strings = strings;
            _entityContainer = entityContainer;
            _serializeContext = serializeContext;
            
            GameMap = _container.Resolve<IMap>();
        }

        public bool Load()
        {
            _variables.Initialise();

            var path = Path.Combine(Directory, "database");

            // TODO: DI
            using (var reader = new TMEBinaryReader(File.Open(path, FileMode.Open)))
            {
                var magicNo = reader.ReadUInt32();
                // TODO: Check magic no and set endianess

                ScenarioId = reader.ReadUInt32();
                // TODO: Check against current scenario

                Version = reader.ReadUInt32();

                Header = reader.ReadString();

                Description = Version > 8 
                    ? reader.ReadString() 
                    : "";
                
                _serializeContext.Reader = reader;
                _serializeContext.Version = Version;
                _serializeContext.IsDatabase = true;
                _serializeContext.IsSaveGame = false;

                // Read objects
                if (_variables is ISerializable variables)
                {
                    if (!variables.Load(_serializeContext))
                    {
                        throw new FileLoadException("Error in variables");
                    }
                }

                if (_entityContainer is ISerializable entities)
                {
                    if (!entities.Load(_serializeContext))
                    {
                        throw new FileLoadException("Error in Entities");
                    }
                }

                // Load Text
                if (_strings is ISerializable strings)
                {
                    if (!strings.Load(_serializeContext))
                    {
                        throw new FileLoadException("Error in Strings");
                    }
                }
                
                // load variables
                
                // load Version 10 extras ** DDR **

            }

            return LoadMap();
        }

        private bool LoadMap()
        {
            var path = Path.Combine(Directory, "map");
            using var reader = new TMEBinaryReader(File.Open(path, FileMode.Open));
            GameMap = _container.Resolve<IMap>();
            GameMap.LoadFullMapFromStream(reader);
            return true;
        }


        public bool Unload()
        {
            throw new NotImplementedException();
        }
        
    }
}
