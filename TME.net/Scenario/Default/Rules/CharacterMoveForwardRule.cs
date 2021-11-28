using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Rules.Interfaces;

namespace TME.Scenario.Default.Rules
{
    public interface ICharacterMoveForwardRule : ICharacterRule
    {
    }
    
    public class CharacterMoveForwardRule : ICharacterMoveForwardRule
    {
        private readonly IVariables _variables;

        public CharacterMoveForwardRule(IVariables variables)
        {
            _variables = variables;
        }
        
        public bool Check(ICharacter character)
        {
            // dead men don't walk!
            // or should be sleeping!
            // completely and utterly shattered?
            if (character.IsDead || 
                character.IsNight || 
                character.Energy <= _variables.sv_energy_cannot_continue )
            {
                return false;
            }

            // hidden under a rock?
            // if auto unhide turned on then we must unhide and carry on
            if (character.IsHidden && !_variables.sv_auto_unhide)
            {
                if (!_variables.sv_auto_unhide)
                    return false;
            }

            return true;
        }
    }
}