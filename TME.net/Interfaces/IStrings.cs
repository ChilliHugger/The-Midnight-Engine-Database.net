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
}