using System;
using TME.Scenario.Default.Enums;

namespace TME.Extensions
{
    public static class EnumExtension
    {
        public static uint Raw<T>(this T value) where T : Enum
        {
            return (uint)(object)value;
        }

        public static Terrain Normalise(this Terrain terrain)
        {
            return terrain switch
            {
                Terrain.Plain or Terrain.Plains2 or Terrain.Plains3 => Terrain.Plains,
                Terrain.Forest2 or Terrain.Forest3 or Terrain.Forest => Terrain.Trees,
                Terrain.Mountain2 or Terrain.Mountain3 or Terrain.IcyMountain => Terrain.Mountain,
                Terrain.WatchTower => Terrain.Tower,
                Terrain.Hills3 or Terrain.Foothills => Terrain.Hills,
                _ => terrain
            };
        }

        public static bool IsTunnelPassage(this Terrain terrain)
        {
            return terrain.Normalise() 
                is Terrain.Plains
                or Terrain.Mountain 
                or Terrain.Trees 
                or Terrain.Hills;
        }
        
        public static bool IsTunnelEntrance(this Terrain terrain)
        {
            return terrain.Normalise() 
                is Terrain.Gate 
                or Terrain.Temple 
                or Terrain.Pit 
                or Terrain.Palace;
        }
        
        public static bool IsTunnelExit(this Terrain terrain)
        {
            return terrain.IsTunnelEntrance();
        }
    }
}