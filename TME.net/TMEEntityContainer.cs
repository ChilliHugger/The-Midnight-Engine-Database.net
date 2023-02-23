using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using TME.Interfaces;
using TME.Scenario.ddr;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TMEEntityContainer : IEntityContainer, ISerializable
    {
        private readonly IDependencyContainer _container;
        private readonly IVariables _variables;
        private IDictionary<string, IEntity> _internalSymbolCache = new Dictionary<string, IEntity>();
        
        public IReadOnlyDictionary<string,IEntity> SymbolCache { get; internal set; }

        public IReadOnlyList<ICharacter> Lords { get; internal set; }
        public IReadOnlyList<IRouteNode> RouteNodes { get; internal set; }
        public IReadOnlyList<IRegiment> Regiments { get; internal set; } 
        public IReadOnlyList<IStronghold> Strongholds { get; internal set; }
        public IReadOnlyList<IWaypoint> Waypoints { get; internal set; }
        public IReadOnlyList<IObject> Things { get; internal set; }
        public IReadOnlyList<IMission> Missions { get; internal set; }
        public IReadOnlyList<IVictory> Victories { get; internal set; }
        
        public IReadOnlyList<DirectionInfo> Directions { get; internal set; }
        public IReadOnlyList<UnitInfo> Units { get; internal set; }
        public IReadOnlyList<RaceInfo> Races { get; internal set; }
        public IReadOnlyList<GenderInfo> Genders { get; internal set; }
        public IReadOnlyList<TerrainInfo> Terrains { get; internal set; }
        public IReadOnlyList<AreaInfo> Areas { get; internal set; }
        public IReadOnlyList<CommandInfo> Commands { get; internal set; }
        public IReadOnlyList<ObjectTypeInfo> ObjectTypes { get; internal set; }
        public IReadOnlyList<ObjectPowerInfo> ObjectPowers { get; internal set; }
        
        public TMEEntityContainer(
            IVariables variables,
            IDependencyContainer container)
        {
            _container = container;
            _variables = variables;
            
            // Items
            Lords = new List<ICharacter>().AsReadOnly();
            Regiments = new List<IRegiment>().AsReadOnly();
            Strongholds = new List<IStronghold>().AsReadOnly();
            RouteNodes = new List<IRouteNode>().AsReadOnly();
            Waypoints = new List<IWaypoint>().AsReadOnly();
            Things = new List<IObject>().AsReadOnly();
            Missions = new List<IMission>().AsReadOnly();
            Victories = new List<IVictory>().AsReadOnly();

            // Info
            Directions = new List<DirectionInfo>().AsReadOnly();
            Units = new List<UnitInfo>().AsReadOnly();
            Races = new List<RaceInfo>().AsReadOnly();
            Genders = new List<GenderInfo>().AsReadOnly();
            Terrains = new List<TerrainInfo>().AsReadOnly();
            Areas = new List<AreaInfo>().AsReadOnly();
            Commands = new List<CommandInfo>().AsReadOnly();
            ObjectTypes = new List<ObjectTypeInfo>().AsReadOnly();
            ObjectPowers = new List<ObjectPowerInfo>().AsReadOnly();

            SymbolCache = new Dictionary<string, IEntity>();
        }


        public bool Load(ISerializeContext context)
        {
            switch (context.Section)
            {
                case DataSection.Entities:
                    _internalSymbolCache = new Dictionary<string, IEntity>();
                    SymbolCache = new ReadOnlyDictionary<string, IEntity>(_internalSymbolCache);
                    return LoadMainInfo(context);
                case DataSection.ObjectInfo:
                    return LoadObjectInfo(context);
                default:
                    return true;
            }
        }

        public bool LoadMainInfo(ISerializeContext context)
        {
            var characters = context.Reader.ReadInt32();
            var regiments = context.Reader.ReadInt32();
            var routenodes = context.Reader.ReadInt32();
            var strongholds = context.Reader.ReadInt32();
            var places = context.Reader.ReadInt32();
            var objects = context.Reader.ReadInt32();
            var missions = context.Reader.ReadInt32();
            var victories = context.Reader.ReadInt32();
            var directions = context.Reader.ReadInt32();
            var units = context.Reader.ReadInt32();
            var races = context.Reader.ReadInt32();
            var genders = context.Reader.ReadInt32();
            var terrains = context.Reader.ReadInt32();
            var areas = context.Reader.ReadInt32();
            var commands = context.Reader.ReadInt32();
            var variables = context.Reader.ReadInt32();

            _variables.sv_variables = variables;
            
            Lords = CreateCollection<ICharacter>(characters).ToList().AsReadOnly();
            Regiments = CreateCollection<IRegiment>(regiments).ToList().AsReadOnly();
            RouteNodes = CreateCollection<IRouteNode>(routenodes).ToList().AsReadOnly();
            Strongholds = CreateCollection<IStronghold>(strongholds).ToList().AsReadOnly();
            Waypoints = CreateCollection<IWaypoint>(places).ToList().AsReadOnly();
            Things = CreateCollection<IObject>(objects).ToList().AsReadOnly();
            Missions = CreateCollection<IMission>(missions).ToList().AsReadOnly();
            Victories = CreateCollection<IVictory>(victories).ToList().AsReadOnly();

            Directions = CreateCollection<DirectionInfo>(directions).ToList().AsReadOnly();
            Units = CreateCollection<UnitInfo>(units).ToList().AsReadOnly();
            Races = CreateCollection<RaceInfo>(races).ToList().AsReadOnly();
            Genders = CreateCollection<GenderInfo>(genders).ToList().AsReadOnly();
            Terrains = CreateCollection<TerrainInfo>(terrains).ToList().AsReadOnly();
            Areas = CreateCollection<AreaInfo>(areas).ToList().AsReadOnly();
            Commands = CreateCollection<CommandInfo>(commands).ToList().AsReadOnly();
            
            ReadCollection(Lords, context);
            ReadCollection(Regiments, context);
            ReadCollection(RouteNodes, context);
            ReadCollection(Strongholds,context);
            ReadCollection(Waypoints,context);
            ReadCollection(Things,context);
            ReadCollection(Missions,context);
            ReadCollection(Victories,context);
            
            ReadInfoCollection(Directions,context);
            ReadInfoCollection(Units,context);
            ReadInfoCollection(Races,context);
            ReadInfoCollection(Genders,context);
            ReadInfoCollection(Terrains,context);
            ReadInfoCollection(Areas,context);
            ReadInfoCollection(Commands,context);

            SymbolCache = new ReadOnlyDictionary<string, IEntity>(_internalSymbolCache);
            
            return true;
        }
        
        public bool LoadObjectInfo(ISerializeContext context)
        {
            // chunk 4
            if (context.Scenario is RevengeScenario && context.Version > 10)
            {
                var types = context.Reader.ReadInt32();
                ObjectTypes = CreateCollection<ObjectTypeInfo>(types).ToList().AsReadOnly();
                ReadInfoCollection(ObjectTypes, context);
                
                var powers = context.Reader.ReadInt32();
                ObjectPowers = CreateCollection<ObjectPowerInfo>(powers).ToList().AsReadOnly();
                ReadInfoCollection(ObjectPowers, context);
            }

            return true;
        }
        
        public bool Save()
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerable<T> CreateCollection<T>(int count)
            where T: IEntity
        {
            var result = new T[count];
            for(var ii=0; ii<count; ii++)
            {
                var entity = _container.CurrentScope != null
                    ? _container.CurrentScope.Resolve<T>()
                    : _container.CurrentContainer.Resolve<T>();

                if (entity is IEntityInternal entityInternal)
                {
                    entityInternal.SetId( new MXId(entityInternal.Type, (uint)ii + 1));
                }
                
                result[ii] = entity;
            }
            return result;
        }

        private void ReadCollection<T>(IEnumerable<T> list, ISerializeContext context)
            where T: IEntity
        {
            var enumerable = list.ToArray();

            foreach (var _ in enumerable)
            {
                var index = context.Reader.PeekInt32()-1;
                var entity = enumerable.ElementAt(index);
                if ( entity is ISerializable item )
                {
                    item.Load(context);
                }
                
                _internalSymbolCache.Add( entity.Symbol, entity);
            }
        }
        
        private void ReadInfoCollection<T>(IEnumerable<T> list, ISerializeContext context)
            where T: IEntity
        {
            var enumerable = list.ToArray();

            foreach (var _ in enumerable)
            {
                var index = context.Reader.PeekInt32();
                var entity = enumerable.ElementAt(index);
                if ( entity is ISerializable item )
                {
                    item.Load(context);
                }
                
                _internalSymbolCache.Add( entity.Symbol, entity);
            }
        }
    }
}