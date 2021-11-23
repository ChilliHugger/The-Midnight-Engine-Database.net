using System.Collections.Generic;
using TechTalk.SpecFlow;
using TME.SpecTests.Fakes;

namespace TME.SpecTests.Context
{
    [Binding]
    public class CommandHistoryContext
    {
        public IList<CommandHistoryItem> Items { get; set; } = new List<CommandHistoryItem>();
    }
}