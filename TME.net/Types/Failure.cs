using TME.Scenario.Default.Interfaces;

namespace TME.Types
{
    public class Failure : IResult
    {
        public static readonly IResult Default = new Failure();
    }
}
