using System.Threading.Tasks;

namespace TME.Scenario.Default.Interfaces
{
    public interface IAction
    {
        Task<IResult> Run(params object[] args);
    }
}
