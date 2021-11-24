using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

namespace TME.Scenario.Default.info
{
    public class TerrainInfo : Info
    {
        public string Preposition { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint Success { get; internal set; }
        public uint Visibility { get; internal set; }
        public uint Obstruction { get; internal set; }
        public int MovementCost { get; internal set; }

        public bool IsPlural => IsFlags(TerrainInfoFlags.Plural);
        public bool IsBlock => IsFlags(TerrainInfoFlags.Block);
        public bool IsInteresting => IsFlags(TerrainInfoFlags.Interesting);
        public bool IsArmyVisible => IsFlags(TerrainInfoFlags.Army);

        public TerrainInfo() : base(EntityType.TerrainInfo)
        {
        }
    }
}
