using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TMEStrings : IStrings, ISerializableLoad
    {
        private Dictionary<string, DatabaseString> _symbolMap = new();
        private Dictionary<MXId, DatabaseString> _idMap = new();
        public IReadOnlyList<DatabaseString> Entries { get; internal set; } = new List<DatabaseString>().AsReadOnly();
        
        public DatabaseString? GetById(MXId id)
        {
            return _idMap.TryGetValue(id, out var result) ? result : default;
        }
        
        public DatabaseString? GetBySymbol(string symbol)
        {
            return _symbolMap.TryGetValue(symbol, out var result) ? result : default;
        }

        public bool Load(ISerializeContext context)
        {
            if (context.Section != DataSection.Strings)
            {
                return true;
            }
            
            var count = context.Reader.UInt32();

            Entries = Enumerable.Range(0, (int) count)
                .Select( _ => Create(context) )
                .Where( s=> s is not null)
                .Cast<DatabaseString>()
                .ToList().AsReadOnly();

            _symbolMap = Entries.ToDictionary(s => s.Symbol, s => s);
            _idMap = Entries.ToDictionary(s => s.Id, s => s);
            return true;
        }

        private static DatabaseString? Create(ISerializeContext context)
        {
            var s = new DatabaseString();
            return s.Load(context) ? s : null;
        }
    }
}