using System;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;

namespace TME.Scenario.Default.info
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class RaceInfo : Info
    {
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
        
        public float RidingMovementMultiplier => (float)RidingMultiplier / 10000.0f;
        
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
        
        public override bool Load(ISerializeContext ctx)
        {
            if (!base.Load(ctx)) return false;

            DefaultSoldiersName = ctx.Reader.ReadString();
            Success = ctx.Reader.ReadUInt32();
            InitialMovement = ctx.Reader.ReadUInt32();
            DiagonalModifier = ctx.Reader.ReadUInt32();
            RidingMultiplier = ctx.Reader.ReadUInt32();
            MovementMax = ctx.Reader.ReadUInt32();
            BaseRestAmount = ctx.Reader.ReadUInt32();
            StrongholdStartups = ctx.Reader.ReadUInt32();
            
            MistTimeAffect = ctx.Reader.ReadInt32();
            MistDespondencyAffect = ctx.Reader.ReadInt32();
            BaseEnergyCost = ctx.Reader.ReadInt32();
            BaseEnergyCostHorse = ctx.Reader.ReadInt32();

            // TODO: Fix database error correctly
            if (IsSymbol("RA_MORKIN"))
            {
                BaseEnergyCost = 2;
                BaseEnergyCostHorse = 4;
            }
            
            if ( ctx.IsDatabase )
            {
                (BaseEnergyCost, BaseEnergyCostHorse) = (BaseEnergyCostHorse, BaseEnergyCost);
            }
            //
            
            return true;
        }
        
// #if defined(_DDR_)
//    L7354 - correct
//    DB TERRAIN_CITY,   TERRAIN_CITY          ;moonprince
//    DB TERRAIN_PLAINS, TERRAIN_PLAINS        ;free
//    DB TERRAIN_TOWER,  TERRAIN_TOWER         ;wise
//    DB TERRAIN_FOREST, TERRAIN_FOREST        ;fey
//    DB TERRAIN_HILLS,  TERRAIN_HILLS         ;barbarian
//    DB TERRAIN_PLAINS, TERRAIN_PLAINS        ;icelord
//    DB TERRAIN_FOREST                        ;fey
//    DB TERRAIN_MOUNTAINS                     ;giant
//    DB TERRAIN_PLAINS                        ;heartstealer
//    DB TERRAIN_FROZENWASTE                   ;dwarf
    
//    L7354A - C64?
//    DB TERRAIN_MOUNTAINS, TERRAIN_MOUNTAINS  ;moonprince
//    DB TERRAIN_PLAINS,    TERRAIN_PLAINS     ;free
//    DB TERRAIN_FOREST,    TERRAIN_FOREST     ;wise
//    DB TERRAIN_FOREST,    TERRAIN_FOREST     ;fey
//    DB TERRAIN_FOREST,    TERRAIN_FOREST     ;barbarian
//    DB TERRAIN_PLAINS,    TERRAIN_PLAINS     ;icelord
//    DB TERRAIN_FOREST                        ;fey
//    DB TERRAIN_MOUNTAINS                     ;giant
//    DB TERRAIN_PLAINS                        ;heartstealer
//    DB TERRAIN_FROZENWASTE                   ;dwarf
    }
}
