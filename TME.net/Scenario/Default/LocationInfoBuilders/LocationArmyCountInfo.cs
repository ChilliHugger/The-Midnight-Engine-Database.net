using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
    public class LocationArmyCountInfo
    {
        public Loc Location { get; internal set; } = Loc.Zero;
        public bool Tunnel { get; internal set; }
        public uint Friends { get; internal set; }
        public uint Foes { get; internal set; }
        public uint Doomdark { get; internal set; }
    }
}