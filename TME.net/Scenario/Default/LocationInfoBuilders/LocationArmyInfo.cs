using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class LocationArmyInfo
    {
        public Loc Location { get; internal set; } = Loc.Zero;
        public bool Tunnel { get; internal set; }
        public IArmyTotals Friends { get; internal set; } = new ArmyTotals();
        public IArmyTotals Foe { get; internal set; } = new ArmyTotals();
        public IArmyTotals Doomdark { get; internal set; } = new ArmyTotals();
        public IReadOnlyList<IArmy> Armies { get; internal set; } = new List<IArmy>();
        public IReadOnlyList<IStronghold> Strongholds { get; internal set; } = new List<IStronghold>();
        public IReadOnlyList<IRegiment> Regiments { get; internal set; } = new List<IRegiment>();
    }

}