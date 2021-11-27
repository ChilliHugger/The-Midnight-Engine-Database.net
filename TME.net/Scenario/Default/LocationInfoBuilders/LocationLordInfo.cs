using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.LocationInfoBuilders
{
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class LocationLordInfo
    {
        public Loc Location { get; internal set; } = Loc.Zero;
        public bool Tunnel { get; internal set; }
        public IReadOnlyList<ICharacter> Recruited { get; internal set; } = new List<ICharacter>();
        public IReadOnlyList<ICharacter> UnRecruited { get; internal set; } = new List<ICharacter>();
        public IReadOnlyList<ICharacter> Lords { get; internal set; } = new List<ICharacter>();
        public IReadOnlyList<ICharacter> Recruitable { get; internal set; } = new List<ICharacter>();
    }
}