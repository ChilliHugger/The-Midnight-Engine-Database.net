using System.Threading.Tasks;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Commands
{
    public class BaseCommand : ICommand
    {
        public async Task<IResult> Execute(params object[] args)
        {
            if (await CanExecute(args) is not Success)
            {
                return Failure.Default;
            }

            return await OnExecute(args);
        }

        public virtual Task<IResult> CanExecute(params object[] args)
        {
            return Task.FromResult(Success.Default);
        }
        
        protected virtual Task<IResult> OnExecute(params object[] args)
        {
            return Task.FromResult(Success.Default);
        }
    }
}