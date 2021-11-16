using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Base
{
    public class Info : Entity
    {
        public string Name { get; protected set; } = "";

        protected Info(EntityType type) : base(type)
        {
        }

    }
}
