using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Interfaces
{
	internal interface IStrongholdInternal : IStronghold, IItemInternal
	{
	}
	
    public interface IStronghold : IItem
    {
		Race OccupyingRace { get; }
		Race Race { get; }
		UnitType UnitType { get; }
		ILord? Occupier { get; }
		ILord? Owner { get; }
		uint Total { get; }
		uint Min { get; }
		uint Max { get; }
		uint StrategicalSuccess { get; }
		uint OwnerSuccess { get; }
		uint EnemySuccess { get; }
		uint Influence { get; }
		uint Respawn { get; }
		Terrain Terrain { get; }
		uint Killed { get; }
		uint Lost { get; }
	}
}
