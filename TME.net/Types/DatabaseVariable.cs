using TME.Serialize;

namespace TME.Types
{
    public class DatabaseVariable : ISerializable
    {
        public string Symbol { get; set; } = "";
        public string Value { get; set; } = "";
        
        public bool Load(ISerializeContext context)
        {
            Symbol = context.Reader.String();
            Value = context.Reader.String();
            _ = context.Reader.Int32();
            return true;
        }

        public bool Save(ISerializeContext context)
        {
            context.Writer.String(Symbol);
            context.Writer.String(Value);
            context.Writer.Int32(0); // unused Type
            return true;
        }
    }
}