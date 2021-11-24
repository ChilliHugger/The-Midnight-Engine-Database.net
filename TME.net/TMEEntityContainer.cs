using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using TME.Interfaces;
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
        
        public TMEEntityContainer(
            IVariables variables,
            IDependencyContainer container)
        {
            _container = container.CurrentContainer;
            _variables = variables;
            
            Lords = new List<ILord>().AsReadOnly();
            Regiments = new List<IRegiment>().AsReadOnly();
            Strongholds = new List<IStronghold>().AsReadOnly();
            RouteNodes = new List<IRouteNode>().AsReadOnly();
            Waypoints = new List<IWaypoint>().AsReadOnly();
            Things = new List<IThing>().AsReadOnly();
            Missions = new List<IMission>().AsReadOnly();
            Victories = new List<IVictory>().AsReadOnly();
            // TODO:
            // directions
            // units
            // races
            // gender
            // terrain
            // areas
            // commands
            // variables
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

            ReadCollection(Lords, context);
            ReadCollection(Regiments, context);
            ReadCollection(RouteNodes, context);
            ReadCollection(Strongholds,context);
            ReadCollection(Waypoints,context);
            ReadCollection(Things,context);
            ReadCollection(Missions,context);
            ReadCollection(Victories,context);
            
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