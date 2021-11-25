using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TME.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TMEStrings : IStrings, ISerializable
    {
        private Dictionary<string, DatabaseString> _symbolMap = new Dictionary<string, DatabaseString>();
        private Dictionary<MXId, DatabaseString> _idMap = new Dictionary<MXId, DatabaseString>();
        public IReadOnlyList<DatabaseString> Entries { get; internal set; } = new List<DatabaseString>().AsReadOnly();

        public DatabaseString? GetById(MXId id) => _idMap[id];
        public DatabaseString? GetBySymbol(string symbol) => _symbolMap[symbol];

        public bool Load(ISerializeContext context)
        {
            var count = context.Reader.ReadUInt32();

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
        
        public bool Save()
        {
            throw new System.NotImplementedException();
        }
    }
}