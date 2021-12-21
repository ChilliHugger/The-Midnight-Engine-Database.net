using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Landscaping
{
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class LandscapeItem
    {
        public Loc Loc { get; set; }
        public Terrain Terrain { get; set; }
        public FloorType Floor { get; set; }
        public bool Army { get; set; }
        public bool Mist { get; set; }
        public Vector3 Position { get; set; }
        public float Scale { get; set; }
    }
}