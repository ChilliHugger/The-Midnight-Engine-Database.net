using System.Collections.Generic;
using TME.Default.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface ILord : IItem
    {
        Direction Looking { get; }
        Time Time { get; }
        Race Race { get; }
        Gender Gender { get; }

        string LongName { get; }
        string ShortName { get; }
        uint Energy { get; }
        uint Reckless { get; }

        IBattleInfo BattleInfo { get; }
        IRecruitment Recruitment { get; }

        IList<IThing> Carrying { get; }
        IThing? KilledBy { get; }

        WaitStatus WaitStatus { get; }

        MXId LastCommandId { get; }
        Command LastCommand { get; }

        List<IUnit> Units { get; }

        ILord? Following { get; }
        uint Followers { get; }

        // lom
        uint Strength { get; }
        uint Cowardly { get; }
        uint Courage { get; }
        uint Fear { get; }

        // ddr
        Orders Orders { get; }
        Race Loyalty { get; }
        ILord? Foe { get; }
        ILord? Liege { get; }
        uint Despondency { get; }
        uint Traits { get; }

    }
}
