using TME.Interfaces;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Rules.Interfaces;

namespace TME.Scenario.Default.Rules
{
    public interface ICharacterRecruitRule: ICharacterRule
    {
        bool Check(ICharacter character, ICharacter target);
    }
    
    public class CharacterRecruitRule : ICharacterRecruitRule
    {
        private readonly IEngine _engine;

        public CharacterRecruitRule(IEngine engine)
        {
            _engine = engine;
        }
        
        public bool Check(ICharacter character, ICharacter target)
        {
            return _engine.Scenario.Info.IsFeature(FeatureFlags.Approach) || 
                 (character.Recruitment.By & target.Recruitment.Key) != 0;
        }

        public bool Check(ICharacter character)
        {
            return false;
        }
    }
}