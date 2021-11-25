using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using TME.Interfaces;
using TME.Scenario.Default.info;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TMEEntityContainer : IEntityContainer, ISerializable
    {
        private readonly IContainer _container;
        private readonly IVariables _variables;
        
        public IReadOnlyList<ILord> Lords { get; internal set; }
        public IReadOnlyList<IRouteNode> RouteNodes { get; internal set; }
        public IReadOnlyList<IRegiment> Regiments { get; internal set; } 
        public IReadOnlyList<IStronghold> Strongholds { get; internal set; }
        public IReadOnlyList<IWaypoint> Waypoints { get; internal set; }
        public IReadOnlyList<IThing> Things { get; internal set; }
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
            _container = container.CurrentContainer;
            _variables = variables;
            
            // Items
            Lords = new List<ILord>().AsReadOnly();
            Regiments = new List<IRegiment>().AsReadOnly();
            Strongholds = new List<IStronghold>().AsReadOnly();
            RouteNodes = new List<IRouteNode>().AsReadOnly();
            Waypoints = new List<IWaypoint>().AsReadOnly();
            Things = new List<IThing>().AsReadOnly();
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
        }


        public bool Load(ISerializeContext context)
        {
            Lords = CreateCollection<ILord>(_variables.sv_characters).ToList().AsReadOnly();
            Regiments = CreateCollection<IRegiment>(_variables.sv_regiments).ToList().AsReadOnly();
            RouteNodes = CreateCollection<IRouteNode>(_variables.sv_routenodes).ToList().AsReadOnly();
            Strongholds = CreateCollection<IStronghold>(_variables.sv_strongholds).ToList().AsReadOnly();
            Waypoints = CreateCollection<IWaypoint>(_variables.sv_places).ToList().AsReadOnly();
            Things = CreateCollection<IThing>(_variables.sv_objects).ToList().AsReadOnly();
            Missions = CreateCollection<IMission>(_variables.sv_missions).ToList().AsReadOnly();
            Victories = CreateCollection<IVictory>(_variables.sv_victories).ToList().AsReadOnly();

            Directions = CreateCollection<DirectionInfo>(_variables.sv_directions).ToList().AsReadOnly();
            Units = CreateCollection<UnitInfo>(_variables.sv_units).ToList().AsReadOnly();
            Races = CreateCollection<RaceInfo>(_variables.sv_races).ToList().AsReadOnly();
            Genders = CreateCollection<GenderInfo>(_variables.sv_genders).ToList().AsReadOnly();
            Terrains = CreateCollection<TerrainInfo>(_variables.sv_terrains).ToList().AsReadOnly();
            Areas = CreateCollection<AreaInfo>(_variables.sv_areas).ToList().AsReadOnly();
            Commands = CreateCollection<CommandInfo>(_variables.sv_commands).ToList().AsReadOnly();
            
            ObjectTypes = CreateCollection<ObjectTypeInfo>(_variables.sv_object_types).ToList().AsReadOnly();
            ObjectPowers = CreateCollection<ObjectPowerInfo>(_variables.sv_object_powers).ToList().AsReadOnly();
            
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

            // if (context.Version > 10)
            // {
            //     ReadCollection(ObjectTypes, context);
            //     ReadCollection(ObjectPowers, context);
            // }

            return true;
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
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
            where T: IEntity
        {
            var enumerable = list.ToArray();

            foreach (var _ in enumerable)
            {
                var index = context.Reader.PeekInt32()-1;
                if ( enumerable.ElementAt(index) is ISerializable item )
                {
                    item.Load(context);
                }
            }
        }
        
        private static void ReadInfoCollection<T>(IEnumerable<T> list, ISerializeContext context)
            where T: IEntity
        {
            var enumerable = list.ToArray();

            foreach (var _ in enumerable)
            {
                var index = context.Reader.PeekInt32();
                if ( enumerable.ElementAt(index) is ISerializable item )
                {
                    item.Load(context);
                }
            }
        }
    }
}