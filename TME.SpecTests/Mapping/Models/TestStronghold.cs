using TME.Scenario.Default.Enums;

namespace TME.SpecTests.Mapping.Models
{
    public class TestStronghold
    {
        public Race Race { get; set; }
        public Race OccupyingRace { get; set; }
        public UnitType UnitType { get; set; }
        public uint Total { get; set; }
    }
}