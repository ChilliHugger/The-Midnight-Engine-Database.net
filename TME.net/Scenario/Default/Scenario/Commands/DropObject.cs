using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario.Actions;
using TME.Types;

namespace TME.Scenario.Default.Scenario.Commands
{
    public class DropObject : BaseCommand
    {
        private static readonly Time Duration = Time.None;

        private readonly IDatabase _database;
        private readonly ICommandHistory _commandHistory;
        private readonly IVariables _variables;
        private readonly IAction _objectDropped;

        public DropObject(
            IDatabase database,
            ICommandHistory commandHistory,
            IVariables variables,
            [KeyFilter(nameof(ObjectDropped))] IAction objectDropped)
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
        protected override async Task<IResult> OnExecute(params object[] args)
        {
            //
            // Lord is dropping a Thing
            //
            if (args.FirstOrDefault() is ILordInternal lord
                && args.LastOrDefault() is IThing thing)
            {
                    var dropped = await _objectDropped.Execute(thing);

                    if (dropped is Success)
                    {
                        var result = await _commandHistory.Save(Command.DropObject, Duration, lord, thing);
                        if (result)
                        {
                            // Commands take time
                            lord.UpdateTime(lord.Time + Duration);

                            // lord no longer has the object
                            lord.RemoveCarriedObject(thing);

                            return Success.Default;
                        }
                    }
            }

            return Failure.Default;
        }

        protected override Task<IResult> CanExecute(params object[] args)
        {
            return args.FirstOrDefault() switch
            {
                ILord lord when args.LastOrDefault() is IThing  
                                && lord.Time + Duration <= _variables.sv_time_night
                    => Task.FromResult(Success.Default),
                    _ => Task.FromResult(Failure.Default)
            };
        }
    }
}
