using System;
using System.Threading.Tasks;

namespace TME.Scenario.Default.Interfaces
{
    public interface ICommand
    {
        Task<IResult> Run(params object[] args);
    }
}
