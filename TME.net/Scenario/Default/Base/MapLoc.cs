﻿using System;
using System.Diagnostics.CodeAnalysis;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
// ReSharper disable MemberCanBePrivate.Global

namespace TME.Scenario.Default.Base
{
    public struct MapLoc
    {
        [SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible")] 
        public static MapLoc None = new MapLoc
        {
            Terrain = Terrain.None,
            Variant = 0,
            Climate = 0,
            Thing = ThingType.None,
            Area = 0,
            Flags = LocationFlags.None
        };
        
        //
        //                                Flags (36:28)
        //                                |          Area (26:10)
        //                                |          |         Thing (17:9)
        //                                |          |         |       Climate (10:7)
        //                                |          |         |       |   Variant (7:3)
        //                                |          |         |       |   |      Terrain (0:3)
        //                                |          |         |       |   |      |
        // 00000000 00000000 00000000 00000000 00000000 00000000 000000000 00000000
        public ulong Bits;

        private const int TerrainPos = 0;
        private const int VariantPos = 7;
        private const int ClimatePos = 10;
        private const int ThingPos = 17;
        private const int AreaPos = 26;
        private const int FlagsPos = 36;

        private const ulong TerrainMask = 0x7Ful        << TerrainPos;   //u64 terrain       :  7 ; // 128 ( 16x8 )
        private const ulong VariantMask = 0x07ul        << VariantPos;   //u64 variant       :  3 ; // 8 ( 8 )
        private const ulong ClimateMask = 0x7Ful        << ClimatePos;   //u64 climate       :  7 ; // 128 ( 16x8 )
        private const ulong ThingMask   = 0x1FFul       << ThingPos;     //u64 object        :  9 ; // 512 ( 16x8 )
        private const ulong AreaMask    = 0x2FFul       << AreaPos;      //u64 area          : 10 ; // 1024 ( 64x16 )
        private const ulong FlagsMask   = 0x0FFFFFFFul  << FlagsPos;     //u64 flags	     : 28 ; // 
        
        public Terrain Terrain
        {
            get => (Terrain) (Bits & TerrainMask); 
            set => Bits = (Bits & ~TerrainMask) | (ulong)value;
        }
        
        public byte Variant
        {
            get => (byte)((Bits&VariantMask) >> VariantPos); 
            set => Bits = (Bits & ~VariantMask) | (ulong)value << VariantPos;
        }
        
        public byte Climate
        {
            get => (byte)((Bits&ClimateMask) >> ClimatePos); 
            set => Bits = (Bits & ~ClimateMask) | (ulong)value << ClimatePos;
        }

        public ThingType Thing
        {
            get => (ThingType)((Bits&ThingMask) >> ThingPos);
            set => Bits = (Bits & ~ThingMask) | (ulong)value << ThingPos;
        }

        public ushort Area
        {
            get => (ushort)((Bits&AreaMask) >> AreaPos); 
            set => Bits = (Bits & ~AreaMask) | (ulong)value << AreaPos;
        }
        
        public LocationFlags Flags
        {
            get => (LocationFlags)((uint)(Bits >> 36) & 0xFFFFFFF);
            set => Bits = (Bits & ~FlagsMask) | (ulong)value << FlagsPos;
        }

        internal void RemoveObject() { Thing = ThingType.None; }

        public bool IsVisible => Flags.HasFlag(LocationFlags.Seen);
        public bool IsInDomain => Flags.HasFlag(LocationFlags.Domain);
        public bool IsSpecial => Flags.HasFlag(LocationFlags.Special);
        public bool IsMisty => Flags.HasFlag(LocationFlags.Mist);
        public bool IsStronghold => Flags.HasFlag(LocationFlags.Stronghold);
        public bool IsRouteNode => Flags.HasFlag(LocationFlags.RouteNode);
        public bool IsTunnelVisible => Flags.HasFlag(LocationFlags.TunnelLookedAt);
        public bool HasTunnel => Flags.HasFlag(LocationFlags.Tunnel);
        public bool HasArmy => Flags.HasFlag(LocationFlags.Army);
        public bool HasLord => Flags.HasFlag(LocationFlags.Lord);
        
        public bool HasTunnelExit => Flags.HasFlag(LocationFlags.TunnelExit);
        public bool HasTunnelEntrance => Flags.HasFlag(LocationFlags.TunnelEntrance);
        public bool HasTunnelPassage => Flags.HasFlag(LocationFlags.Tunnel) && !Flags.HasFlag(LocationFlags.TunnelEntrance|LocationFlags.TunnelExit);


        public void SetFlags(LocationFlags flags,bool value)
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
