using System;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Scenario
{
    public class Success : IResult
    {
        public static IResult Default = new Success();

        public Success()
        {
        }
    }
}
