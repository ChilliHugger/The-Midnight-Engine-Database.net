using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME
{
    public class TMEDatabase : IDatabase
    {
        //#define TME_MAGIC_NO		ID_4CC('T','M','E','!')
        private readonly IVariables _variables;
        private readonly IContainer _container;
        private readonly ISerializeContext _serializeContext;
        private readonly IEntityResolver _entityResolver;
        private readonly IEntityContainer _entityContainer;
        
        public string Directory { get; set; } = "";

        public uint ScenarioId { get; private set; }
        public uint Version { get; private set; }
        public string Header { get; private set; } = "";
        public string Description { get; private set; } = "";
        public IMap GameMap { get; set; }


        public TMEDatabase(
            IVariables variables,
            IEntityContainer entityContainer,
            IEntityResolver entityResolver,
            ISerializeContext serializeContext,
            IDependencyContainer container)
        {
            _container = container.CurrentContainer;
            _variables = variables;
            _entityResolver = entityResolver;
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
                
                // TODO: DI
                var context = _serializeContext;
                context.Reader = reader;
                context.Version = Version;

                // Read objects
                if (_variables is ISerializable variables)
                {
                    if (!variables.Load(context))
                    {
                        throw new FileLoadException("Error in variables");
                    }
                }

                if (_entityContainer is ISerializable entities)
                {
                    if (!entities.Load(context))
                    {
                        throw new FileLoadException("Error in Entities");
                    }
                }



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
