using System;
using System.Threading.Tasks;

namespace TME.Scenario.Default.Interfaces
{
    public interface ICommand
    {
        Task<IResult> Execute(params object[] args);
    }
}
