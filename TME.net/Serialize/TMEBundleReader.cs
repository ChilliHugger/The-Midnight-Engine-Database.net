using System;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Serialize
{
    public class TMEBundleReader : IBundleReader
    {
        private readonly IEntityResolver _entityResolver;

        public TMEBundleReader(Bundle bundle, IEntityResolver entityResolver)
        {
            _entityResolver = entityResolver;
            Raw = bundle;
        }

        public Bundle Raw { get; }

        private T Get<T>(string name, T def)
        {
            if (Raw.TryGetValue(name, out var value))
            {
                return (T)value!;
            }

            return def;
        }
        
        public ulong UInt64(string name) => Get<ulong>(name,0);

        public string String(string name) => Get<string>(name,"");
        
        public Size Size(string name) => Get(name,Types.Size.Zero);

        public MXId Id(string name) => Get(name,MXId.None);

        public T Flags<T>(string name) where T : Enum =>Enum<T>(name);

        public Loc Loc(string name) =>  Get(name, Scenario.Default.Base.Loc.Zero);
        
        public Time Time(string name) => Get<uint>(name,0);

        public Direction Direction(string name) => Enum<Direction>(name);

        public Race Race(string name) => Enum<Race>(name);

        public Gender Gender(string name) => Enum<Gender>(name);

        public WaitStatus WaitStatus(string name) => Enum<WaitStatus>(name);

        public Orders Orders(string name) => Enum<Orders>(name);

        public ArmyType ArmyType(string name) => Enum<ArmyType>(name);

        public UnitType UnitType(string name) => Enum<UnitType>(name);

        public ThingType ThingType(string name) => Enum<ThingType>(name);

        public Terrain Terrain(string name) => Enum<Terrain>(name);

        public uint UInt32(string name) => Get<uint>(name,0);

        public T Enum<T>(string name) where T : Enum
        {
            if (Raw.TryGetValue(name, out var value))
            {
                return (T)value!;
            }
            return default;
        }

        public short Int16(string name) => Get<short>(name,0);

        public ushort UInt16(string name) => Get<ushort>(name,0);

        public int Int32(string name) => Get(name,0);
        
        public T? Entity<T>(string name)
            where T : IEntity
        {
            if (Raw.TryGetValue(name, out var value))
            {
                return (T)value!;
            }

            return default(T);
        }

        public T[] EntityArray<T>(string name)
            where T : IEntity
        {
            if (Raw.TryGetValue(name, out var value))
            {
                if (value is T[] nodes)
                {
                    return nodes;
                }
            }

            return Array.Empty<T>();
        }
    }
}