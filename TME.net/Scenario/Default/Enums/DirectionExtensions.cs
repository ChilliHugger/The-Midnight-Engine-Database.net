namespace TME.Scenario.Default.Enums
{
    public static class DirectionExtensions
    {
        public static Direction Previous(this Direction lhs)
        {
            return (lhs == Direction.North) ? Direction.NorthWest : (Direction)(lhs.Raw() - 1);
        }

        public static Direction Next(this Direction lhs)
        {
            return (lhs == Direction.NorthWest) ? Direction.North : (Direction)(lhs.Raw() + 1);
        }

        public static int Raw(this Direction lhs)
        {
            return (int)lhs;
        }
    }
}