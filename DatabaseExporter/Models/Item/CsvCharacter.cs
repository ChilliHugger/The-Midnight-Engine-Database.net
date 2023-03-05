// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DatabaseExporter.Converters;
using TME.Scenario.ddr.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Serialize;

namespace DatabaseExporter.Models.Item
{
    public class CsvCharacter : CsvItem
    {
        public string Looking { get; set; }
        public uint Time { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        [Name("Long Name")]
        public string LongName { get; set; }
        [Name("Short Name")]
        public string ShortName { get; set; }
        public string Carrying { get; set; }
        public string Following { get; set; }
        public uint Energy { get; set; }
        public uint Reckless { get; set; }
        public uint Strength { get; set; }
        public uint Cowardly { get; set; }
        public uint Courage { get; set; }
        public uint Fear { get; set; }
        public Orders Orders { get; set; } 
        public string Loyalty { get; set; }
        public string Foe { get; set; }
        public string Liege { get; set; }
        public uint Despondency { get; set; }
        public string Traits { get; set; }
        public uint Warriors { get; set; }
        public uint Riders { get; set; }
        [Name("Recruit")]
        public uint RecruitmentKey { get; set; }
        [Name("Group")]
        public uint RecruitmentBy { get; set; }
        
        // ddr
        [Name("Home"), Optional]
        public string Home { get; set; }
        [Name("Desired Object"), Optional]
        public string DesiredObject { get; set; }
        
        public override Bundle ToBundle(CsvImportConverter converter)
        {
            return new Bundle {
                {nameof(Entity.Id), converter.ToId(EntityType.Character,Id)},
                {nameof(Entity.Symbol), Symbol},
                {nameof(Entity.Flags), converter.ToFlags<LordFlags>(Flags)},
                {nameof(Character.Location), converter.ToLoc(Location)},
                
                {nameof(Character.LongName), LongName},
                {nameof(Character.ShortName), ShortName},
                {nameof(Character.Looking), converter.ToEnum<Direction>(Looking)},
                {nameof(Character.Time), Time},
                
                {nameof(Character.Race), converter.ToEnum<Race>(Race)},
                {nameof(Character.Gender), converter.ToEnum<Gender>(Gender)},
                {nameof(Character.Carrying), converter.ToArray<IObject>(Carrying)},
                {nameof(Character.Following), converter.ToEntity<ICharacter>(Following)},
                {nameof(Character.Energy), Energy},
                {nameof(Character.Reckless), Reckless},
                {nameof(Character.Strength), Strength},
                {nameof(Character.Cowardly), Cowardly},
                {nameof(Character.Courage), Courage},
                {nameof(Character.Fear), Fear},
                {nameof(Character.Orders), Orders},
                {nameof(Character.Loyalty), converter.ToEnum<Race>(Loyalty)},
                {nameof(Character.Foe), converter.ToEntity<ICharacter>(Foe)},
                {nameof(Character.Liege), converter.ToEntity<ICharacter>(Liege)},
                {nameof(Character.Despondency), Despondency},
                {nameof(Character.Traits), converter.ToFlags<LordTraits>(Traits)},
                {nameof(Character.Warriors), Warriors},
                {nameof(Character.Riders), Riders},
                {nameof(Character.Recruitment.Key), RecruitmentKey},
                {nameof(Character.Recruitment.By), RecruitmentBy},
                {nameof(Character.HomeStronghold), converter.ToEntity<IStronghold>(Home)},
                {nameof(Character.DesiredObject), converter.ToEntity<IObject>(DesiredObject)},
            };
        }
    }
    
    public sealed class OutCharacterMap<T> : ClassMap<T>
        where T : ICharacter
    {
        public OutCharacterMap()
        {
            // CsvRecord
            Map().Constant(1).Index(0).Name("Version");
            Map(m => m.Id).Index(1);
            Map(m => m.Symbol).Index(2);
            // CsvEntity
            Map(m => m.Flags).Index(3);
            // // CsvItem
            Map(m => m.Location).Index(4);
            // CsvRegiment
            Map(m => m.LongName).Index(5).Name("Long Name");
            Map(m => m.ShortName).Index(6).Name("Short Name");
            
            Map(m => m.Recruitment.Key).Index(7).Name("Recruit");
            Map(m => m.Recruitment.By).Index(8).Name("Group");

            Map(m => m.Looking).Index(9);
            Map(m => m.Time).Index(10);
            Map(m => m.Race).Index(11);
            Map(m => m.Gender).Index(12);
            Map(m => m.Loyalty).Index(13);
            Map(m => m.Energy).Index(14);
            Map(m => m.Reckless).Index(15);
            Map(m => m.Strength).Index(16);
            Map(m => m.Cowardly).Index(17);
            Map(m => m.Courage).Index(18);
            Map(m => m.Despondency).Index(19);
            Map(m => m.Fear).Index(20);
            Map(m => m.Orders).Index(21);
            Map(m => m.Carrying).Index(22);
            Map(m => m.WaitStatus).Ignore();
            Map(m => m.Units).Ignore();
            Map(m => m.Warriors).Index(23);
            Map(m => m.Riders).Index(24);
            Map(m => m.Following).Index(25);
            Map(m => m.Foe).Index(26);
            Map(m => m.Liege).Index(27);
            Map(m => m.Traits).Index(28);
            
            // ddr
            Map(m => m.HomeStronghold).Index(29).Name("Home");
            Map(m => m.DesiredObject).Index(30).Name("Desired Object");
            
            //Map(m => m.KilledBy.Symbol).Index(12).Name("KilledBy");
            //Map(m => m.LastCommandId.Symbol).Index(14).Name("KilledBy");
            //Map(m => m.Followers).Index(15);
        }
    }
}