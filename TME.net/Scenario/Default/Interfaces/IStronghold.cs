using System;
using TME.Scenario.Default.Enums;

namespace TME.Default.Interfaces
{
    public interface IStronghold : IItem
    {
		Race OccupyingRace { get; }
		Race Race { get; }
		UnitType type { get; }
		uint Total { get; }
		uint Min { get; }
		uint Max { get; }
		uint StrategicalSuccess { get; }
		uint OwnerSuccess { get; }
		uint EnemySuccess { get; }
		uint Influence { get; }
		uint Respawn { get; }
		ILord Occupier { get; }
		ILord Iwner { get; }
		Terrain Terrain { get; }
		uint Killed { get; }
		uint Lost { get; }
	}
}
