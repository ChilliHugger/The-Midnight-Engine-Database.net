using System;
using TME.Serialize;

namespace TME.Default.Interfaces
{
    public interface IRecruitment : ISerializable
    {
        uint Key { get; }
        uint By { get; }
    }
}
