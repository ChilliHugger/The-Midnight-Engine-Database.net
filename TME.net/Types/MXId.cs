using System;
using TME.Scenario.Default.Enums;

namespace TME.Types
{
    public class MXId : CustomValueType<MXId,uint>
    {
        public static readonly MXId None = new MXId(EntityType.None,0);
        private MXId(uint value) : base(value) { }
        public static implicit operator MXId(uint value) { return new MXId(value); }
        public static implicit operator uint(MXId custom) { return custom.Value; }


        public EntityType Type => (EntityType)((Value >> 24) & 0xff);
        public int RawId => (int)(Value & 0x00ffffff);

        public MXId(EntityType type, uint id) : base((((uint)(type) << 24) | id)) { }

        public override string ToString() => $"{Type}({(int)Type}):{RawId} - {Value:x}";

        //#define MAKE_ID(type,id)			(mxid)(((type)<<24)|id)
        //#define ID_TYPE(id) 				(id_type_t)((id>>24)&0xff)
        //#define GET_ID(id) 					(id&0x00ffffff)
    }
}
