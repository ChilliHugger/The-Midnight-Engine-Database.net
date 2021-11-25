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
        
        public bool Load(ISerializeContext context)
        {
            Id = context.Reader.ReadMXId(EntityType.String);
            Symbol = context.Reader.ReadString();
            Text = context.Reader.ReadString();
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