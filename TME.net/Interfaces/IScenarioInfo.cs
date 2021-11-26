namespace TME.Interfaces
{
    public interface IScenarioInfo
    {
        uint Id { get; }
        uint Version { get; }
        int DatabaseVersion { get; }
        string Symbol { get; }
        string Name { get; }
        string Website { get; }
        string Support { get; }
        string Author { get; }
        string Copyright { get; }
    }
}