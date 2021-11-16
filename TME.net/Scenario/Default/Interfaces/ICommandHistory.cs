using System;
using System.Threading.Tasks;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface ICommandHistory
    {
        Task<bool> Save(Command command, Time Duration, params object[] args);
    }
}
