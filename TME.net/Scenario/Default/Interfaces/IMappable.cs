using TME.Scenario.Default.Base;

namespace TME.Scenario.Default.Interfaces
{
    internal interface IMappableInternal
    {
        Loc Location { set; }
    }
    
    public interface IMappable
    {
        Loc Location { get; }
    }
}
