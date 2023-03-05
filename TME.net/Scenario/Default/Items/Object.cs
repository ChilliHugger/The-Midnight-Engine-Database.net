using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Items
{
    public partial class Object : Item, IObject
    {
        public new ObjectFlags Flags => (ObjectFlags) RawFlags;
        public ThingType Kills { get; internal set; } = ThingType.None;
        public string Name { get; internal set; } = "";
        public string Description { get; internal set; } = "";
        public uint UseDescription { get; internal set;  }
        public IItem? CarriedBy { get; internal set; }

        public ObjectPower ObjectPower { get; internal set; } = ObjectPower.None;
        public ObjectType ObjectType { get; internal set; } = ObjectType.None; 
        
        public Object() : base(EntityType.Object)
        {
        }
    }
}