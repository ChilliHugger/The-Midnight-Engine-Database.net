using System;
using TME.Scenario.Default.Enums;

namespace TME.Default.Interfaces
{
    public interface IThing : IItem
    {
        ThingType Kills { get; }
        string Name { get; }
        string Description { get; }
        uint UseDescription { get; }
        IThing CarriedBy { get; set; }

        bool IsUnique { get; }
    }
}
