using System.Collections.Generic;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    internal interface ICharacterInternal : ICharacter
    {
        void UpdateTime(Time time);
        void RemoveCarriedObject(IObject thing);
        void SetCarrying(IEnumerable<IObject> carried);
    }
    
    public interface ICharacter : IItem
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

        IReadOnlyList<IObject> Carrying { get; }
        IObject? KilledBy { get; }

        WaitStatus WaitStatus { get; }

        MXId LastCommandId { get; }
        Command LastCommand { get; }

        List<IUnit> Units { get; }

        ICharacter? Following { get; }
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
        ICharacter? Foe { get; }
        ICharacter? Liege { get; }
        uint Despondency { get; }
        uint Traits { get; }

        bool IsFriendlyTo(ICharacter lord);
        bool IsOnSameSide(ICharacter lord);
        ICharacter? CommanderInChief { get; }
        
        // flags
        bool IsAlive { get; }
        bool IsHidden { get; }
        bool IsRecruited { get; }
        bool IsInTunnel { get; }
        
    }
}
