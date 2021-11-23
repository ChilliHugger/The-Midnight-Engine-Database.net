using System.Collections;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.SpecTests.Context;
using TME.Types;

namespace TME.SpecTests.Fakes
{
    public record CommandHistoryItem(Command Command, Time Duration, IEnumerable Arguments);
    
    [Binding]
    public class CommandHistoryFake : ICommandHistory
    {
        private readonly CommandHistoryContext _commandHistoryContext;
        
        public CommandHistoryFake(CommandHistoryContext commandHistoryContext)
        {
            _commandHistoryContext = commandHistoryContext;
        }
        
        public Task<bool> Save(Command command, Time duration, params object[] args)
        {
           _commandHistoryContext.Items.Add(
               new CommandHistoryItem(command,duration,args) 
               );
           return Task.FromResult(true);
        }
    }
}