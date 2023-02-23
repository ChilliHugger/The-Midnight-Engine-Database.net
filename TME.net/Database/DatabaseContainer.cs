using TME.Interfaces;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Database
{
    public class DatabaseHeader
    {
        public uint ScenarioId { get; set; }
        public uint Version { get; set; }
        public string Header { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public interface IDatabaseEntityContainer : ISerializable
    {
    }
    
    public interface IDatabaseStrings : ISerializable
    {
    }

    public interface IDatabaseVariables : ISerializable
    {
    }
    
    public class DatabaseContainer
    {
        // public DatabaseHeader Header { get; init; } = new DatabaseHeader();
        // public IEntityContainer EntityContainer { get; init; }
        // public IStrings Strings { get; init; }
        // public IVariables Variables { get; init; }
    }

    // public class TMEDatabaseStrings : IDatabaseStrings
    // {
    //     private readonly IStrings _strings;
    //
    //     public TMEDatabaseStrings(IStrings strings)
    //     {
    //         _strings = strings;
    //     }
    //
    //     public bool Load(ISerializeContext context)
    //     {
    //         return _strings.Load(context);
    //     }
    //
    //     public bool Save()
    //     {
    //         throw new System.NotImplementedException();
    //     }
    // }
    
    
    
}