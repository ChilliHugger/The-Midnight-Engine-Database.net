using System.Collections.Generic;
using TME.Types;

namespace TME.Interfaces
{
    public interface IStrings
    {
        IReadOnlyList<DatabaseString> Entries { get; }
        DatabaseString? GetById(MXId id);
        DatabaseString? GetBySymbol(string symbol);
    }

    public class NoopStrings : IStrings
    {
        public IReadOnlyList<DatabaseString> Entries => new List<DatabaseString>();
        public DatabaseString? GetById(MXId id) => default;
        public DatabaseString? GetBySymbol(string symbol) => default;
    }
}