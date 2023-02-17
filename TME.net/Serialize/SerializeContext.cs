using System;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public class SerializeContext : ISerializeContext
    {
        public double Version { get; set; }
        public bool IsSaveGame { get; set; }
        public bool IsDatabase { get; set; }
        public ISerializeReader Reader { get; set; }
        private readonly IEntityResolver _entityResolver;
        private readonly IStrings _strings;

        public SerializeContext(IEntityResolver entityResolver, IStrings strings)
        {
            _entityResolver = entityResolver;
            _strings = strings;
            Reader = new NullReader();
        }

        public T? ReadEntity<T>()
            where T : IEntity
        {
            var id = Reader.ReadUInt32();
            return _entityResolver.EntityById<T>((int)id);
        }

        public IEntity? ReadEntity()
        {
            var id = Reader.ReadUInt32();
            return (IEntity?)_entityResolver.EntityById(id);
        }

        public DatabaseString? ReadString()
        {
            var id = Reader.ReadUInt32();
            return _strings.GetById(new MXId(EntityType.String, id));
        }

        
        //public void ReadCollection<T>(IEnumerable<T> list, ISerializeContext context)
        //{
        //    for (int ii = 0; ii < list.Count(); ii++)
        //    {
        //        var index = context.Reader.ReadInt32() - 1;
        //        if (list.ElementAt(index) is ISerializable item)
        //        {
        //            item.Load(context);
        //        }
        //        //_objectReader.Read(list.ElementAt(index), reader);
        //    }
        //}

    }
}
