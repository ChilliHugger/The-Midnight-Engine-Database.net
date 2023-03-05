using System.Diagnostics.CodeAnalysis;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;

namespace TME.Scenario.ddr.Items
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class RevengeCharacter : Character, IRevengeLord
    {
        public IUnit? Unit =>
            ArmyType switch
            {
                UnitType.Rider => Units[1],
                UnitType.Warrior => Units[0],
                _ => null
            };

        public UnitType ArmyType =>
            IsFlags(LordFlags.AllowedRiders) 
                ? UnitType.Rider : IsFlags(LordFlags.AllowedWarriors) 
                    ? UnitType.Warrior 
                    : UnitType.None;

        public uint ArmySize => Unit?.Total ?? 0;
        
        public RevengeCharacter(
            IVariables variables) : base(variables)
        {
        }
    }
}