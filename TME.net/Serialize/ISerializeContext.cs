using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public interface ISerializeContext
    {
        double Version { get; set; }
        bool IsSaveGame { get; set; }
        bool IsDatabase { get; set; }
        DataSection Section { get; set; }
        IScenario? Scenario { get; set; }
        
        ISerializeReader Reader { get; set; }
        ISerializeWriter Writer { get; set; }
        
        T? ReadEntity<T>() where T : IEntity;
        void WriteEntity<T>(T? entity) where T : IEntity;
        
        IEntity? ReadEntity();
        DatabaseString? ReadString();

    }
}
