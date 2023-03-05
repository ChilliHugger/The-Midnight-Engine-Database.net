using System.Collections.Generic;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Interfaces
{
    public interface ICharacter : IItem
    {
        new LordFlags Flags { get; }
        Direction Looking { get; }
        Time Time { get; }
        Race Race { get; }
        Gender Gender { get; }

        string LongName { get; }
        string ShortName { get; }
        uint Energy { get; }
        uint Reckless { get; }

        BattleInfo BattleInfo { get; }
        Recruitment Recruitment { get; }

        IReadOnlyList<IObject> Carrying { get; }
        IObject? KilledBy { get; }

        WaitStatus WaitStatus { get; }

        MXId LastCommandId { get; }
        Command LastCommand { get; }

        IList<IUnit> Units { get; }

        ICharacter? Following { get; }
        uint Followers { get; }

        // lom
        uint Strength { get; }
        uint Cowardly { get; }
        uint Courage { get; }
        uint Fear { get; }

        uint Riders { get; }
        uint Warriors { get; }
        
        bool IsDawn { get; }
        bool IsNight { get; }
        
        // These are only initially used for DDR but are
        // supported in default characters
        Orders Orders { get; }
        Race Loyalty { get; }
        ICharacter? Foe { get; }
        ICharacter? Liege { get; }
        uint Despondency { get; }
        LordTraits Traits { get; }
        Loc LastLocation { get; }
        IObject? DesiredObject { get; }
        IStronghold? HomeStronghold { get; }
        ICharacter? FightingAgainst { get; }
        uint BattleLost { get; }
        MXId TargetId { get; }
        Loc TargetLocation { get; }
        //
    }
}
