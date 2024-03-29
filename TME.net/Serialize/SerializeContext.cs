﻿using System;
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
        public DataSection Section { get; set; } = DataSection.None;
        public IScenario? Scenario { get; set; } = default;
        
        public ISerializeReader Reader { get; set; }
        public ISerializeWriter Writer { get; set; }
        
        private readonly IEntityResolver _entityResolver;
        private readonly IStrings _strings;

        public SerializeContext(IEntityResolver entityResolver, IStrings strings)
        {
            _entityResolver = entityResolver;
            _strings = strings;
            Reader = new NullReader();
            Writer = new NullWriter();
        }

        public T? ReadEntity<T>()
            where T : IEntity
        {
            var id = Reader.UInt32();
            return _entityResolver.EntityById<T>(id);
        }

        public void WriteEntity<T>(T? entity)
            where T : IEntity
        {
            Writer.UInt32(entity?.RawId ?? 0);
        }
        
        public IEntity? ReadEntity()
        {
            var id = Reader.UInt32();
            return (IEntity?)_entityResolver.EntityById(id);
        }

        public DatabaseString? ReadString()
        {
            var id = Reader.UInt32();
            return _strings.GetById(new MXId(EntityType.String, id));
        }
    }
}
