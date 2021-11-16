using System;
using TME.Default.Interfaces;
using TME.Interfaces;

namespace TME.Serialize
{
    public interface ISerializeContext
    {
        double Version { get; }
        ISerializeReader Reader { get; }
        
        T? ReadEntity<T>() where T : IEntity;
    }
}
