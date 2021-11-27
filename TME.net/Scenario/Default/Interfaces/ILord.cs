using System.Collections.Generic;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface ILordInternal : ILord
    {
        void UpdateTime(Time time);
        void RemoveCarriedObject(IThing thing);
        void SetCarrying(IEnumerable<IThing> carried);
    }
    
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

        IReadOnlyList<IThing> Carrying { get; }
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

        // These are only initially used for DDR but are
        // supported in default characters
        Orders Orders { get; }
        Race Loyalty { get; }
        ILord? Foe { get; }
        ILord? Liege { get; }
        uint Despondency { get; }
        uint Traits { get; }

        bool IsFriendlyTo(ILord lord);
        bool IsOnSameSide(ILord lord);
        ILord? CommanderInChief { get; }
        
        // flags
        bool IsAlive { get; }
        bool IsHidden { get; }
        bool IsRecruited { get; }
        bool IsInTunnel { get; }
        
    }
}
