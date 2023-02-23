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
        
        public static bool IsZeroBased(this EntityType type)
        {
            switch (type)
            {
                case EntityType.AreaInfo:
                case EntityType.AttributeInfo:
                case EntityType.DirectionInfo:
                case EntityType.GenderInfo:
                case EntityType.RaceInfo:
                case EntityType.TerrainInfo:
                case EntityType.UnitInfo:
                case EntityType.CommandInfo:
                case EntityType.ObjectPower:
                case EntityType.ObjectType:
                case EntityType.String:
                    return true;
                
                case EntityType.None:
                case EntityType.LocationInfo:
                case EntityType.Unused1:
                case EntityType.ArmyTotal:
                case EntityType.MemoryItem:
                case EntityType.MapLocation:
                case EntityType.Location:
                case EntityType.Unit:
                case EntityType.Army:
                    return false;
                    
                case EntityType.Thing:
                case EntityType.Character:
                case EntityType.Regiment:
                case EntityType.RouteNode:
                case EntityType.Stronghold:
                case EntityType.Waypoint:
                case EntityType.Mission:
                case EntityType.Victory:
                    return false;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}