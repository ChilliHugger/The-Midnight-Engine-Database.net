using System;
using TME.Interfaces;
using TME.Scenario.Default.Interfaces;

namespace TME.Serialize
{
    public interface ISerializeContext
    {
        double Version { get; set; }
        ISerializeReader Reader { get; set; }
        
        T? ReadEntity<T>() where T : IEntity;
    }
}
