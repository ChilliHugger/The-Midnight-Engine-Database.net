using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Types;

namespace TME.Serialize
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Bundle : Dictionary<string, object?>
    {
        public string String(string name) => (string) (this[name] ?? "");
        public MXId Id(string name) => (MXId) (this[name] ?? MXId.None);
        public T Flags<T>(string name) => (T) (this[name] ?? 0);
        public Loc Loc(string name) => (Loc) (this[name] ?? Scenario.Default.Base.Loc.Zero);

        public uint UInt32(string name) => (uint) (this[name] ?? 0);
        public int Int32(string name)=> (int) (this[name] ?? 0);

    }

    public interface IBundleReader
    {
        bool Load(Bundle bundle);
    }
}