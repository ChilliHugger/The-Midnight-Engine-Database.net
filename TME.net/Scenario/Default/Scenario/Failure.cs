using System;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Scenario
{
    public class Failure : IResult
    {
        public static IResult Default = new Failure();

        public Failure()
        {
        }
    }
}
