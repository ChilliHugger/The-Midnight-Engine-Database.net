using TME.Interfaces;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Rules.Interfaces;

namespace TME.Scenario.Default.Rules
{
    public interface ICharacterApproachRule: ICharacterRecruitRule
    {
    }
    
    public class CharacterApproachRule : ICharacterApproachRule
    {
        private readonly IEngine _engine;

        public CharacterApproachRule(IEngine engine)
        {
            _engine = engine;
        }
        
        public bool Check(ICharacter character, ICharacter target)
        {
            return _engine.Scenario.Info.IsFeature(FeatureFlags.Approach);
        }

        public bool Check(ICharacter character)
        {
            return false;
        }
    }
}