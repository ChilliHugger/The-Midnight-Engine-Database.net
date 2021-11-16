using System;
using TME.Default.Interfaces;
using TME.Interfaces;

namespace TME.Serialize
{
    public class SerializeContext : ISerializeContext
    {
        public double Version { get; }
        public ISerializeReader Reader { get; }
        private IEntityResolver Resolver { get; }

        public SerializeContext(double version, ISerializeReader reader, IEntityResolver resolver)
        {
            Version = version;
            Reader = reader;
            Resolver = resolver;
        }

        public T? ReadEntity<T>()
            where T : IEntity
        {
            var id = Reader.ReadUInt32();
            return Resolver.EntityById<T>((int)id);
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
