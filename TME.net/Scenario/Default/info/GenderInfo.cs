using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    public class GenderInfo : Info
    {
        public string PersonalPronoun { get; internal set; } // He
        public string PossesivePronoun { get; internal set; } // His
        public string Pronoun3 { get; internal set; } // Him

        public GenderInfo() : base(EntityType.GenderInfo)
        {
        }
    }
}
