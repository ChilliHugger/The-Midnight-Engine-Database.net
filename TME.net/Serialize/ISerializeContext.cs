using System;
using TME.Default.Interfaces;
using TME.Interfaces;

namespace TME.Serialize
{
    public interface ISerializeContext
    {
        double Version { get; set; }
        ISerializeReader Reader { get; set; }
        
        T? ReadEntity<T>() where T : IEntity;
    }
}
