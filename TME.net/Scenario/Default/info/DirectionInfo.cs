using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DirectionInfo : Info
    {
        public DirectionInfo() : base(EntityType.DirectionInfo)
        {
        }
    }
}
