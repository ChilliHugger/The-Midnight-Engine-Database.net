using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TME.Extensions;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Scenario;
using TME.Types;

namespace TME.Scenario.Default.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class Character : Item, ICharacter
    {

        #region "DI"
        private readonly IVariables _variables = null!;
        public BattleInfo BattleInfo { get; internal set; }
        public Recruitment Recruitment { get; internal set; }
        #endregion

        public new LordFlags Flags => (LordFlags) RawFlags;
        public Direction Looking { get; internal set; } = Direction.None;
        public Time Time { get; internal set; } = Time.None;
        public Race Race { get; internal set; } = Race.None;
        public Gender Gender { get; internal set; } = Gender.None;
        public string LongName { get; internal set; } = "";
        public string ShortName { get; internal set; } = "";
        public IReadOnlyList<IObject> Carrying { get; internal set; }
        public IObject? KilledBy { get; internal set; }
        public WaitStatus WaitStatus { get; internal set; } = WaitStatus.None;
        public MXId LastCommandId { get; internal set; } = MXId.None;
        public Command LastCommand { get; internal set; } = Command.None;
        public IList<IUnit> Units { get; internal set; } 
        public ICharacter? Following { get; internal set; }
        public uint Energy { get; internal set; }
        public uint Reckless { get; internal set; }
        public uint Followers { get; internal set; }
        public uint Strength { get; internal set; }
        public uint Cowardly { get; internal set; }
        public uint Courage { get; internal set; }
        public uint Fear { get; internal set; }
        public uint Riders
        {
            get => Units[1].Total;
            internal set => ((IUnitInternal)Units[1]).SetTotal(value);
        }
        public uint Warriors
        {
            get => Units[0].Total;
            internal set => ((IUnitInternal)Units[0]).SetTotal(value);
        }
        public Orders Orders { get; internal set; } = Orders.None;
        public Race Loyalty { get; internal set; } = Race.None;
        public ICharacter? Foe { get; internal set; }
        public ICharacter? Liege { get; internal set; }
        public uint Despondency { get; internal set; }
        public LordTraits Traits { get; internal set; }

        public bool IsDawn => Time == _variables.sv_time_dawn && !this.IsResting();
        public bool IsNight => Time == _variables.sv_time_night || this.IsResting();
        
        // Revenge
        public Loc LastLocation { get; internal set; } = Loc.Zero;
        public IObject? DesiredObject { get; internal set; }
        public IStronghold? HomeStronghold { get; internal set; }
        public ICharacter? FightingAgainst { get; internal set; }
        public uint BattleLost { get; internal set; }
        public MXId TargetId { get; internal set; } = MXId.None;
        public Loc TargetLocation { get; internal set; } = Loc.Zero;
        //
        
        internal Character() : base(EntityType.Character)
        {
            BattleInfo = new BattleInfo();
            Recruitment = new Recruitment();
            Units = new List<IUnit>();
            Carrying = new List<IObject>();
        }

        public Character(
            IVariables variables) : base(EntityType.Character)
        {
            _variables = variables;
            BattleInfo = new BattleInfo();
            Recruitment = new Recruitment();
            Units = new List<IUnit> { new Warriors(), new Riders()};
            Carrying = new List<IObject>();
        }
    }
}
