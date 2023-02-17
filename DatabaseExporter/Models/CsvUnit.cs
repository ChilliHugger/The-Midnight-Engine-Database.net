using TME.Scenario.Default.Enums;

namespace DatabaseExporter.Models
{
    public class CsvUnit
    {
        public UnitType Type { get; set; }
        public uint Total { get; set; }
        public uint Energy { get; set; }
        public uint Lost { get; set;}
        public uint Killed { get; set; }
    }
}