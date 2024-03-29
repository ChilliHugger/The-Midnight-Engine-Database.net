using System.Collections.Generic;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace DatabaseExporter;

public class CsvStrings : IStrings, ISerializableSave
{
    private Dictionary<string, DatabaseString> _symbolMap = new();
    private Dictionary<MXId, DatabaseString> _idMap = new();
    private IReadOnlyList<DatabaseString> _strings;

    public IReadOnlyList<DatabaseString> Entries { 
        get => _strings;
        set
        {
            _strings = value.ToList().AsReadOnly();
            _symbolMap = _strings.ToDictionary( a=>a.Symbol, b=>b );
            _idMap = _strings.ToDictionary( a=>a.Id, b=>b );
        }
    }
        
    public DatabaseString GetById(MXId id)
    {
        return _idMap.TryGetValue(id, out var result) ? result : default;
    }
        
    public DatabaseString GetBySymbol(string symbol)
    {
        return _symbolMap.TryGetValue(symbol, out var result) ? result : default;
    }

    public bool Save(ISerializeContext context)
    {
        if (context.Section != DataSection.Strings)
        {
            return true;
        }
        
        context.Writer.UInt32((uint)Entries.Count);
        
        foreach (var v in Entries.OrderBy(v=>v.Id.RawId))
        {
            v.Save(context);
        }

        return true;
    }
}