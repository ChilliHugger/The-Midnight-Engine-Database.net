using TME.Serialize;

namespace TME.Scenario.Default.Interfaces
{
    public interface IRecruitment : ISerializable
    {
        uint Key { get; }
        uint By { get; }
    }
}
