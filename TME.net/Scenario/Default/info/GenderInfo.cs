using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    public class GenderInfo : Info
    {
        public string PersonalPronoun { get; internal set; } = "";        // He / She
        public string PossessivePronoun { get; internal set; } = "";      // His / Her
        public string SingularPronoun { get; internal set; } = "";        // Him / Her

        public GenderInfo() : base(EntityType.GenderInfo)
        {
        }
    }
}
