using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.info
{
    public class CommandInfo : Info
    {
        public uint SuccessTime { get; internal set; }
        public uint FailureTime { get; internal set; }

        public CommandInfo() : base(EntityType.CommandInfo)
        {
        }
    }
}
