using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    public class UnitInfo : Info
    {
        public uint Success { get; internal set; }
        public uint BaseRestModifier { get; internal set; }

        public UnitInfo() : base(EntityType.UnitInfo)
        {
        }
    }
}
