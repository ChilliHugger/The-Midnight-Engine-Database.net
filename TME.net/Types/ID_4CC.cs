using System;
namespace TME.Types
{
    public class ID_4CC : CustomValueType<ID_4CC, uint>
    {
        public static ID_4CC FromSig(char a, char b, char c, char d)
        {
            byte[] components = new byte[]
            {
                Convert.ToByte(a),
                Convert.ToByte(b),
                Convert.ToByte(c),
                Convert.ToByte(d),
           };

           return new ID_4CC(BitConverter.ToUInt32(components,0));
        }

        private ID_4CC(uint value) : base(value) { }
        public static implicit operator ID_4CC(uint value) { return new ID_4CC(value); }
        public static implicit operator uint(ID_4CC custom) { return custom.Value; }

        public ID_4CC Reverse()
        {
            byte[] bytes = BitConverter.GetBytes( (uint) this);

            Array.Reverse(bytes);

            return (ID_4CC)BitConverter.ToUInt32(bytes, 0);
        }

    }
}
