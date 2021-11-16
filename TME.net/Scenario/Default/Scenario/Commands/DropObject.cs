using System;
using System.Linq;
using System.Threading.Tasks;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Scenario.Default.Scenario.Commands
{
    public class DropObject : ICommand
    {
        private readonly static Time Duration = 0u;

        private readonly IDatabase _database;
        private readonly ICommandHistory _commandHistory;
        private readonly IVariables _variables;
        private readonly IAction _objectDropped;

        public DropObject(
            IDatabase database,
            ICommandHistory commandHistory,
            IVariables variables,
            IAction objectDropped)
        {
            _database = database;
            _commandHistory = commandHistory;
            _variables = variables;
            _objectDropped = objectDropped;
        }

        //
        // args[0] is Lord
        // args[1] is Object
        //
        public async Task<IResult> Run(params object[] args)
        {
            //
            // Lord is dropping a Thing
            //
            if (args.FirstOrDefault() is ILord lord
                && args.LastOrDefault() is IThing thing)
            {
                if ( lord.Time + Duration <= _variables.sv_time_night)
                {
                    var dropped = await _objectDropped.Run(thing);

                    if (dropped is Success)
                    {
                        var result = await _commandHistory.Save(Command.DropObject, Duration, lord, thing);

                        // Commands take time
                        lord.Time += Duration;

                        // lord no longer has the object
                        lord.Carrying.Remove(thing);

                        return Success.Default;
                    }
                }
            }

            return Failure.Default;
        }
    }
}
