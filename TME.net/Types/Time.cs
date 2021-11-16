using System;
namespace TME.Types
{
    public class Time : CustomValueType<Time, uint>
    {
        public static readonly Time None = new Time(0);
        
        private Time(uint value) : base(value) { }
        public static implicit operator Time(uint value) { return new Time(value); }
        public static implicit operator uint(Time custom) { return custom.Value; }
    }
}