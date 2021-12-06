using TME.Enums;

namespace TME.Processors
{
    public interface INumberText
    {
        string Describe(int number, ZeroMode zeroMode);
    }
}