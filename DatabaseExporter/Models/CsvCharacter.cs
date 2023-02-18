// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;
using System.Linq;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Types;

namespace DatabaseExporter.Models
{
    public class CsvCharacter : CsvItem
    {
        public Direction Looking { get; set; }
        public Time Time { get; set; }
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public List<CsvId> Carrying { get; set; }
        //public CsvId KilledBy { get; set; }
        public WaitStatus WaitStatus { get; set; } 
        //public CsvId LastCommandId { get; set; }
        //public Command LastCommand { get; set; }
        public List<CsvUnit> Units { get; set; } 
        public CsvId Following { get; set; }
        public uint Energy { get; set; }
        public uint Reckless { get; set; }
        //public uint Followers { get; set; }
        public uint Strength { get; set; }
        public uint Cowardly { get; set; }
        public uint Courage { get; set; }
        public uint Fear { get; set; }
        public Orders Orders { get; set; } 
        public Race Loyalty { get; set; }
        public CsvId Foe { get; set; }
        public CsvId Liege { get; set; }
        public uint Despondency { get; set; }
        public LordTraits Traits { get; set; }

        // for export
        public CsvUnit Warriors => Units.First(u => u.Type == UnitType.Warrior);
        public CsvUnit Riders => Units.First(u => u.Type == UnitType.Rider);
        public LordFlags LordFlags => (LordFlags) Flags;
        
        // ddr
        public CsvId Home { get; set; }
        public CsvId DesiredObject { get; set; }
        //public Loc LastLocation { get; internal set; } = Loc.Zero;
        //public IRevengeLord? FightingAgainst { get; internal set; }
        //public uint BattleLost { get; internal set; }
        //public MXId TargetId { get; internal set; } = MXId.None;
        //public Loc TargetLocation { get; internal set; } = Loc.Zero;
    }
    
    public sealed class CsvCharacterMap : CsvClassMap<CsvCharacter>
    {
        public CsvCharacterMap()
        {
            // CsvRecord
            Map(m => m.Version).Index(0);
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Convert(m=>ConvertFlags(m.Value.LordFlags)).Index(3);
            // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRegiment
            Map(m => m.Looking).Index(5);
            Map(m => m.Time).Index(6);
            Map(m => m.Race).Index(7);
            Map(m => m.Gender).Index(8);
            Map(m => m.LongName).Index(9);
            Map(m => m.ShortName).Index(10);
            Map(m => m.Loyalty).Index(11);
            Map(m => m.Energy).Index(12);
            Map(m => m.Reckless).Index(13);
            Map(m => m.Strength).Index(14);
            Map(m => m.Cowardly).Index(15);
            Map(m => m.Courage).Index(16);
            Map(m => m.Despondency).Index(17);
            Map(m => m.Fear).Index(18);
            Map(m => m.Orders).Index(19);
            Map(m => m.Carrying).Convert(c=>c.Value.Carrying.FirstOrDefault()?.Symbol ?? "None").Index(20).Name("Carrying");
            Map(m => m.WaitStatus).Index(21);
            Map(m => m.Warriors.Total).Index(22).Name("Warriors");
            Map(m => m.Riders.Total).Index(22).Name("Riders");
            Map(m => m.Following.Symbol).Index(24).Name("Following");
            Map(m => m.Foe.Symbol).Index(25).Name("Foe");
            Map(m => m.Liege.Symbol).Index(26).Name("Liege");
            Map(m => m.Traits).Convert(m=>ConvertFlags(m.Value.Traits)).Index(27);
            
            // ddr
            Map(m => m.Home.Symbol).Index(28).Name("Home");  
            Map(m => m.DesiredObject.Symbol).Index(29).Name("DesiredObject");
            
            //Map(m => m.KilledBy.Symbol).Index(12).Name("KilledBy");
            //Map(m => m.LastCommandId.Symbol).Index(14).Name("KilledBy");
            //Map(m => m.Followers).Index(15);
        }
    }
}