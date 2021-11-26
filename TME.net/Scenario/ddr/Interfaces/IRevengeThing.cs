using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.ddr.Interfaces
{
    public interface IRevengeThing : IThing
    {
        ObjectPower ObjectPower { get; }
        ObjectType ObjectType { get; }
        bool IsSpecial { get; }
        bool IsRandomStart { get; }
        bool CanHelpRecruitment { get; }
    }
}