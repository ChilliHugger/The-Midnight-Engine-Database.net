using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Scenario
{
    public class Warriors : Unit
    {
        public Warriors() : base(UnitType.Warrior)
        {
        }
        
        public Warriors(uint total) : base(UnitType.Rider,total)
        {
        }
    }
}