using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using TME.Scenario.Default.Actions;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Commands
{
    public class DropObject : BaseCommand
    {
        private static readonly Time Duration = Time.None;

        private readonly ICommandHistory _commandHistory;
        private readonly IVariables _variables;
        private readonly IAction _objectDropped;

        public DropObject(
            ICommandHistory commandHistory,
            IVariables variables,
            [KeyFilter(nameof(ObjectDropped))] IAction objectDropped)
        {
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
            if (args.FirstOrDefault() is ICharacterInternal lord
                && args.LastOrDefault() is IObject thing)
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

        public override Task<IResult> CanExecute(params object[] args)
        {
            if (args.FirstOrDefault() is ICharacter lord &&
                args.LastOrDefault() is IObject thing &&
                lord.Time + Duration > _variables.sv_time_night &&
                lord.Carrying.Contains(thing))
            {
                return Task.FromResult(Success.Default);
            }

            return Task.FromResult(Failure.Default);
        }
    }
}
