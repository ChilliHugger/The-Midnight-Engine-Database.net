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

        public string Directory { get; set; } = "";

        public uint ScenarioId { get; private set; }
        public uint Version { get; private set; }
        public string Header { get; private set; } = "";
        public string Description { get; private set; } = "";
        public IMap GameMap { get; set; }

        public IEnumerable<ILord> Lords { get; private set; }
        public IEnumerable<IRouteNode> RouteNodes { get; private set; }
        public IEnumerable<IRegiment> Regiments { get; private set; } 
        public IEnumerable<IStronghold> Strongholds { get; private set; }

        private readonly IContainer _container;
        //private readonly IEntityResolver _entityResolver;

        public TMEDatabase(
            IDependencyContainer container,
            IVariables variables)
        {
            _container = container.CurrentContainer;
            _variables = variables;

            Lords = new List<ILord>();
            Regiments = new List<IRegiment>();
            Strongholds = new List<IStronghold>();
            RouteNodes = new List<IRouteNode>();
            
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
                var entityResolver = _container.Resolve<IEntityResolver>();

                var context = new SerializeContext(Version, reader, entityResolver);

                // Read objects
                if (_variables is ISerializable variables)
                {
                    variables.Load(context);
                }

                Lords = CreateCollection<ILord>(_variables.sv_characters);
                Regiments = CreateCollection<IRegiment>(_variables.sv_regiments);
                RouteNodes = CreateCollection<IRouteNode>(_variables.sv_routenodes);
                //Strongholds = CreateCollection<IStronghold>(_variables.sv_strongholds);

                ReadCollection(Lords, context);
                ReadCollection(Regiments, context);
                ReadCollection(RouteNodes, context);
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

        private IEnumerable<T> CreateCollection<T>(int count)
            where T: IEntity
        {
            var result = new T[count];
            for(var ii=0; ii<count; ii++)
            {
                result[ii] = _container.Resolve<T>();
            }
            return result;
        }

        private static void ReadCollection<T>(IEnumerable<T> list, ISerializeContext context)
        {
            var enumerable = list.ToArray();

            for(var ii=0; ii<enumerable.Count(); ii++)
            {
                var index = context.Reader.PeekInt32()-1;
                if ( enumerable.ElementAt(index) is ISerializable item )
                {
                    item.Load(context);
                }
                //_objectReader.Read(list.ElementAt(index), reader);
            }
        }
    }
}
