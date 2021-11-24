using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    public class AreaInfo : Info
    {
        public string Prefix { get; internal set; } = "";

        public AreaInfo() : base(EntityType.AreaInfo) 
        {
        }
    }
}
