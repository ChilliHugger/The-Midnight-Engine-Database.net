using TME.Scenario.Default.Base;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IMappableInternal : IMappable
    {
        void UpdateLocation(Loc location);
    }
    
    public interface IMappable
    {
        Loc Location { get; }
    }
}
