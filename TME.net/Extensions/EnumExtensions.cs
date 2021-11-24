using System;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Extensions
{
    public static class EnumExtension
    {
        public static uint Raw<T>(this T value) where T : Enum
        {
            return (uint)(object)value;
        }
    }
}