using TechTalk.SpecFlow;

namespace TME.SpecTests.Transformations
{
    [Binding]
    public class StandardStringTransformations
    {
        [StepArgumentTransformation(@"(should|should not)")]
        public bool ShouldShouldNotTransform(string shouldShouldNot)
        {
            return shouldShouldNot == "should";
        }
    }
}