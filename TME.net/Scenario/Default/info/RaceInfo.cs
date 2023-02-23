// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    public partial class RaceInfo : Info
    {
        public const float RidingMultiplierFactor = 10000.0f;
            
        private const uint MovementNone = 0;
        private const uint MovementDownsDoomguard = MovementNone;
        private const uint MovementDowns = 1;
        private const uint MovementMountain = 4;
        private const uint MovementForestDoomguard = 4;
        private const uint MovementForestFey = MovementNone;
        private const uint MovementForest = 3;
        
        public string DefaultSoldiersName { get; internal set; } = "";
        public string SoldiersName => string.IsNullOrEmpty(DefaultSoldiersName) 
            ? Name 
            : SoldiersName;
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

        public Race Race => (Race) RawId;
        
        public float RidingMovementMultiplier => (float)RidingMultiplier / RidingMultiplierFactor;
        
        // TODO require race/terrain table here
        public uint TerrainMovementModifier( Terrain terrain )
        {
            // TODO: SHOULD BE DONE THROUGH THE DATABASE
            return terrain switch
            {
                Terrain.Downs => Race == Race.Doomguard 
                    ? MovementDownsDoomguard 
                    : MovementDowns,
                Terrain.Mountain => MovementMountain,
                Terrain.Forest => Race switch
                {
                    Race.Doomguard => MovementForestDoomguard,
                    Race.Fey => MovementForestFey,
                    _ => MovementForest
                },
                _ => MovementNone
            };
        }
        
        public RaceInfo() : base(EntityType.RaceInfo)
        {
        }

    }
}
