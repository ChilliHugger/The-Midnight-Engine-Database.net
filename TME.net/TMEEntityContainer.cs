using System.Collections.Generic;
using System.Linq;
using Autofac;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME
{
    public class TMEEntityContainer : IEntityContainer, ISerializable
    {
        private readonly IContainer _container;
        private readonly IVariables _variables;
        
        public IEnumerable<ILord> Lords { get; private set; }
        public IEnumerable<IRouteNode> RouteNodes { get; private set; }
        public IEnumerable<IRegiment> Regiments { get; private set; } 
        public IEnumerable<IStronghold> Strongholds { get; private set; }

        public TMEEntityContainer(
            IVariables variables,
            IDependencyContainer container)
        {
            _container = container.CurrentContainer;
            _variables = variables;
            
            Lords = new List<ILord>();
            Regiments = new List<IRegiment>();
            Strongholds = new List<IStronghold>();
            RouteNodes = new List<IRouteNode>();
        }


        public bool Load(ISerializeContext context)
        {
            Lords = CreateCollection<ILord>(_variables.sv_characters);
            Regiments = CreateCollection<IRegiment>(_variables.sv_regiments);
            RouteNodes = CreateCollection<IRouteNode>(_variables.sv_routenodes);
            Strongholds = CreateCollection<IStronghold>(_variables.sv_strongholds);
            
            ReadCollection(Lords, context);
            ReadCollection(Regiments, context);
            ReadCollection(RouteNodes, context);
            ReadCollection(Strongholds,context);

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