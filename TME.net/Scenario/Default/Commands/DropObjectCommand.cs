using System.Linq;
using TME.Scenario.Default.Actions.Interfaces;
using TME.Scenario.Default.Commands.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.Default.Commands
{
    public class DropObjectCommand : IDropObjectCommand
    {
        private static readonly Time Duration = Time.None;

        private readonly ICommandHistory _commandHistory;
        private readonly IVariables _variables;
        private readonly IObjectDroppedAction _objectDropped;

        public DropObjectCommand(
            ICommandHistory commandHistory,
            IVariables variables,
            IObjectDroppedAction objectDropped)
        {
            _commandHistory = commandHistory;
            _variables = variables;
            _objectDropped = objectDropped;
        }

        //
        // args[0] is Lord
        // args[1] is Object
        //
        public IResult Execute(ICharacter character, IObject carriedObject)
        {
            //
            // Lord is dropping a Thing
            //
            if (!CanExecute(character, carriedObject))
            {
                return Failure.Default;
            }

            if (_objectDropped.Execute(carriedObject) is not Success)
            {
                return Failure.Default;
            }

            if (!_commandHistory.Save(Command.DropObject, Duration, character, carriedObject))
            {
                return Failure.Default;
            }

            if (character is not ICharacterInternal characterInternal)
            {
                return Failure.Default;
            }
            
            // Commands take time
            characterInternal.UpdateTime(character.Time + Duration);

            // lord no longer has the object
            characterInternal.RemoveCarriedObject(carriedObject);
                            
            return Success.Default;

        }

        public bool CanExecute(ICharacter character, IObject carriedObject)
        {
            return 
                character.Time + Duration > _variables.sv_time_night &&
                character.Carrying.Contains(carriedObject);
        }
    }
}
