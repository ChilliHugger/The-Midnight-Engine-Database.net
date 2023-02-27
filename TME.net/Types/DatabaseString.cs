using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Types
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DatabaseString : ISerializable
    {
        public MXId Id { get; set; } = MXId.None;
        public string Symbol { get; set; } = "";
        public string Text { get; set; } = "";

        public DatabaseString()
        {
        }
        
        internal DatabaseString(uint id, string symbol, string text)
        {
            Id = new MXId(EntityType.String, id);
            Symbol = symbol;
            Text = text;
        }
        
        public bool Load(ISerializeContext context)
        {
            Id = context.Reader.MXId(EntityType.String);
            Symbol = context.Reader.String();
            Text = context.Reader.String();
            return true;
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            var idx = Id.RawId.ToString("0000");
            return $"{idx} : '{Symbol}'";
        }
    }
}