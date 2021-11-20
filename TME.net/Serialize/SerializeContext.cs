using System;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

namespace TME.Serialize
{
    public class SerializeContext : ISerializeContext
    {
        public double Version { get; set; }
        public ISerializeReader Reader { get; set; }
        private readonly IEntityResolver _entityResolver;

        public SerializeContext(IEntityResolver entityResolver)
        {
            _entityResolver = entityResolver;
            Reader = new NullReader();
        }

        public T? ReadEntity<T>()
            where T : IEntity
        {
            var id = Reader.ReadUInt32();
            return _entityResolver.EntityById<T>((int)id);
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
