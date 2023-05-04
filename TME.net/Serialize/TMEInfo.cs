using System.Collections.Generic;
using TME.Scenario.Default.Enums;
using TME.Types;

namespace TME.Serialize
{
    public static class TMEInfo
    {
        public static readonly ID_4CC TMEMagicNo = ID_4CC.FromSig('T', 'M', 'E', '!');
        public const string TMEDatabaseHeader = "MidnightEngineDatabase";
        public const string TMESaveGameHeader = "MidnightEngineSaveGame";
        public const uint TMESaveGameVersion = 20; // 14;
        public const string TMEMapHeader = "MidnightEngineMap";
        public const uint TMEMapVersion = 3;
        public const string TMEDiscoveryHeader = "MidnightEngineDiscovery";
        public const uint TMEDiscoveryVersion = 1;
        
        public static readonly IEnumerable<DataSection> Sections = new List<DataSection> 
        {
            DataSection.Counts,
            DataSection.VariableCount,
            DataSection.Entities,
            DataSection.Strings,
            DataSection.Variables,
            DataSection.ObjectInfo
        };
    }
}