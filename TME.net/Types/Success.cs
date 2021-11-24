using TME.Scenario.Default.Interfaces;

namespace TME.Types
{
    public class Success : IResult
    {
        public static readonly IResult Default = new Success();
    }
}
