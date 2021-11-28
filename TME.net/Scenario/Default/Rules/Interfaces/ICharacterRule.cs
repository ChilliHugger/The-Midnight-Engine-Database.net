using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Rules.Interfaces
{
    public interface ICharacterRule : IRule
    {
        bool Check(ICharacter character);
    }
}