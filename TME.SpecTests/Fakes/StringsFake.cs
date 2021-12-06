using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TME.Interfaces;
using TME.SpecTests.Context;
using TME.Types;

namespace TME.SpecTests.Fakes
{
    [Binding]
    public class StringsFake : IStrings
    {
        private readonly StringsContext _stringsContext;
        public IReadOnlyList<DatabaseString> Entries => _stringsContext.Entries;

        public StringsFake(StringsContext stringsContext)
        {
            _stringsContext = stringsContext;
        }
        
        public DatabaseString? GetById(MXId id)
        {
            return Entries.FirstOrDefault(s => s.Id == id.RawId);
        }

        public DatabaseString? GetBySymbol(string symbol)
        {
            return _stringsContext.Strings[symbol];
        }
    }
}