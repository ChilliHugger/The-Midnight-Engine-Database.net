using System.Threading.Tasks;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Scenario.Actions
{
    public class BaseAction : IAction
    {
        public async Task<IResult> Execute(params object[] args)
        {
            if (await CanExecute(args) is not Success)
            {
                return Failure.Default;
            }

            return await OnExecute(args);
        }

        protected virtual Task<IResult> CanExecute(params object[] args)
        {
            return Task.FromResult(Success.Default);
        }
        
        protected virtual Task<IResult> OnExecute(params object[] args)
        {
            return Task.FromResult(Success.Default);
        }
    }
}