using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RaceInfo : Info
    {
        public string SoldiersName { get; internal set; } = "";
        public uint Success { get; internal set; }
        public uint InitialMovement { get; internal set; }
        public uint DiagonalModifier { get; internal set; }
        public uint RidingMultiplier { get; internal set; }
        public uint MovementMax { get; internal set; }
        public uint BaseRestAmount { get; internal set; }
        public uint StrongholdStartups { get; internal set; }

        public int MistTimeAffect { get; internal set; }
        public int MistDespondencyAffect { get; internal set; }
        public int BaseEnergyCost { get; internal set; }
        public int BaseEnergyCostHorse { get; internal set; }

        public float RidingMovementMultiplier => (float)RidingMultiplier / 10000.0f;

        public RaceInfo() : base(EntityType.RaceInfo)
        {
        }
    }
}
