using System;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.Default.Base
{
    public struct MapLoc
    {
        //
        //                                Flags (36:28)
        //                                |          Area (26:10)
        //                                |          |         Thing (17:9)
        //                                |          |         |       Climate (10:7)
        //                                |          |         |       |   Variant (7:3)
        //                                |          |         |       |   |      Terrain (0:3)
        //                                |          |         |       |   |      |
        // 00000000 00000000 00000000 00000000 00000000 00000000 000000000 00000000
        public ulong bits;

        private const int TerrainPos = 0;
        private const int VariantPos = 7;
        private const int ClimatePos = 10;
        private const int ThingPos = 17;
        private const int AreaPos = 26;
        private const int FlagsPos = 36;

        private const ulong TerrainMask = 0x7Fu        << TerrainPos;   //u64 terrain       :  7 ; // 128 ( 16x8 )
        private const ulong VariantMask = 0x07u        << VariantPos;   //u64 variant       :  3 ; // 8 ( 8 )
        private const ulong ClimateMask = 0x7Fu        << ClimatePos;   //u64 climate       :  7 ; // 128 ( 16x8 )
        private const ulong ThingMask   = 0x1FFu       << ThingPos;     //u64 object        :  9 ; // 512 ( 16x8 )
        private const ulong AreaMask    = 0x2FFu       << AreaPos;      //u64 area          : 10 ; // 1024 ( 64x16 )
        private const ulong FlagsMask   = 0x0FFFFFFFu  << FlagsPos;     //u64 flags	        : 28 ; // 

        public Terrain Terrain => (Terrain)(bits & TerrainMask);

        public byte Variant => (byte)((bits&Variant) >> VariantPos);

        public byte Climate => (byte)((bits&ClimateMask) >> ClimatePos);

        public ThingType Thing
        {
            get => (ThingType)((bits&ThingMask) >> ThingPos);
            set => bits = (bits & ~ThingMask) | (ulong)value;
        }

        public UInt16 Area => (UInt16)((bits&AreaMask) >> AreaPos);

        public LocationFlags Flags
        {
            get => (LocationFlags)((UInt32)(bits >> 36) & (UInt32)0xFFFFFFF);
            set => bits = (bits & ~FlagsMask) | (ulong)value;
        }

        public void RemoveObject() { Thing = ThingType.None; }

        public bool IsVisible => Flags.HasFlag(LocationFlags.Seen);
        public bool IsInDomain => Flags.HasFlag(LocationFlags.Domain);
        public bool IsSpecial => Flags.HasFlag(LocationFlags.Special);
        public bool IsMisty => Flags.HasFlag(LocationFlags.Mist);
        public bool IsStronghold => Flags.HasFlag(LocationFlags.Stronghold);
        public bool IsRouteNode => Flags.HasFlag(LocationFlags.Routenode);
        public bool IsTunnelVisible => Flags.HasFlag(LocationFlags.TunnelLookedAt);
        public bool HasTunnel => Flags.HasFlag(LocationFlags.Tunnel);
        public bool HasArmy => Flags.HasFlag(LocationFlags.Army);
        public bool HasLord => Flags.HasFlag(LocationFlags.Lord);

        private void SetFlags(LocationFlags flags,bool value)
        {
            if (value)
            {
                Flags |= flags;
            }
            else
            {
                Flags &= ~flags;
            }
        }

        public bool HasObject
        {
            get => Flags.HasFlag(LocationFlags.Object);
            set => SetFlags(LocationFlags.Object, value);
        }
    }
}
