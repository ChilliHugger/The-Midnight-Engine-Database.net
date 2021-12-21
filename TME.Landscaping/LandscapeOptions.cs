using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Landscaping
{
    public class LandscapeOptions
    {
        public bool ShowWater { get; set; }
        public bool ShowLand { get; set; }
        public bool ShowTerrain { get; set; }
        public bool ShowPeople { get; set; }
        public int DebugMode { get; set; }
        public bool DebugLand { get; set; }
        public float LandScaleX { get; set; }
        public float LandScaleY { get; set; }
        public Loc Here { get; set; }
        public Loc CurrentLocation { get; set; }
        public Loc AheadLocation { get; set; }
        public Direction CurrentDirection { get; set; }
        public bool IsMoving { get; set; }
        public Loc MoveFrom { get; set; }
        public Loc MoveTo { get; set; }
        public float MovementAmount { get; set; }
        public bool MoveLocationHasArmy { get; set; }
        public bool IsLooking { get; set; }
        public float LookAmount { get; set; }
        public float LookOffsetAdjustment { get; set; }
        public bool IsInTunnel { get; set; }
        public bool IsLookingDownTunnel { get; set; }
        public bool IsLookingOutTunnel { get; set; }
        public Time TimeOfDay { get; set; }
        public float ResScale { get; set; }
    }
}