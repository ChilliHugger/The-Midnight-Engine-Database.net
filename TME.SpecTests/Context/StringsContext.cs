using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TME.Types;

namespace TME.SpecTests.Context
{
    [Binding]
    public class StringsContext
    {
        public DatabaseString[] Entries => new[]
        {
            new DatabaseString(1, "SS_NUMBERS",
                "one|two|three|four|five|six|seven|eight|nine|ten|eleven|twelve|thirteen|fourteen|fifteen|sixteen|seventeen|eighteen|nineteen|twenty|thirty|forty|fifty|sixty|seventy|eighty|ninety|hundred|thousand"),
            new DatabaseString(67, "SS_ZEROTOKENS", "|no|none|zero"),
        };

        public Dictionary<string, DatabaseString> Strings { get; private set; }

        public StringsContext()
        {
            Strings = Entries.ToDictionary(k => k.Symbol, v => v);
        }
    }
}