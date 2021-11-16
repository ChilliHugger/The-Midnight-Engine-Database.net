using System;
using TME.Scenario.Default.Base;
using TME.Serialize;

namespace TME.Default.Interfaces
{
    public interface IBattleInfo : ISerializable
    {
        Loc Location { get; set; }
        uint Slew { get; set; }
    }
}
