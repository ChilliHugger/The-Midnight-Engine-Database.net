// using System.Buffers.Binary;
// using System.Collections.Generic;
// using System.IO;
// using Microsoft.Extensions.Logging;
// using TME.Interfaces;
// using TME.Scenario.Default.Enums;
// using TME.Serialize;
// using TME.Types;
//
// namespace TME.Database
// {
//     public class DatabaseReader
//     {
//         private static readonly ID_4CC TMEMagicNo = ID_4CC.FromSig('T', 'M', 'E', '!');
//
//         private readonly ILogger _logger;
//         private readonly IScenario _scenario;
//         private readonly ISerializeContext _serializeContext;
//
//         public DatabaseReader(
//             ILogger logger,
//             IScenario scenario,
//             ISerializeContext serializeContext)
//         {
//             _logger = logger;
//             _scenario = scenario;
//             _serializeContext = serializeContext;
//         }
//         
//         public bool Load(string directory, DatabaseContainer container)
//         {
//             var scenarioInfo = _scenario.Info;
//             var path = Path.Combine(directory, "database");
//
//             // TODO: DI
//             using var reader = new TMEBinaryReader(File.Open(path, FileMode.Open));
//             
//             // chunk 0
//             _serializeContext.Section = DataSection.None;
//                 
//             var magicNo = reader.ReadUInt32();
//             if (magicNo != TMEMagicNo)
//             {
//                 var number = BinaryPrimitives.ReverseEndianness(TMEMagicNo);
//                 if (magicNo != number)
//                 {
//                     _logger.LogError($"Database MagicNo '{magicNo}' does not match '{TMEMagicNo}'");
//                     return false;
//                 }
//
//                 reader.EnableByteSwap = true;
//             }
//
//             // chunk 1
//             _serializeContext.Section = DataSection.Header;
//             
//             var scenarioId = reader.ReadUInt32();
//             if (scenarioId != scenarioInfo.Id)
//             {
//                 _logger.LogError($"Database ScenarioId '{scenarioId}' does not match '{scenarioInfo.Id}'");
//                 return false;
//             }
//             
//             var version = reader.ReadUInt32();
//             if (version < scenarioInfo.DatabaseVersion)
//             {
//                 _logger.LogError(
//                     $"Database Version '{version}' is less than scenario version '{scenarioInfo.DatabaseVersion}'");
//                 return false;
//             }
//             
//             container.Header.ScenarioId = scenarioId;
//             container.Header.Version = version;
//             container.Header.Header = reader.ReadString();
//             container.Header.Description = "";
//                 
//             // chunk 2
//             _serializeContext.Section = DataSection.SaveGameHeader;
//             _serializeContext.Reader = reader;
//             _serializeContext.Version = version;
//             _serializeContext.IsDatabase = true;
//             _serializeContext.IsSaveGame = false;
//             _serializeContext.Scenario = _scenario;
//                 
//             var readers = new List<object>()
//             {
//                 container.EntityContainer,
//                 container.Strings,
//                 container.Variables,
//             };
//
//             // TODO: Loop until data exhausted
//             var sections = new List<DataSection> 
//             {
//                 DataSection.Entities,
//                 DataSection.Strings,
//                 DataSection.Variables,
//                 DataSection.ObjectInfo
//             };
//                 
//             foreach (var section in sections)
//             {
//                 _serializeContext.Section = section;
//                 foreach (var r in readers)
//                 {
//                     if (r is ISerializable item)
//                     {
//                         if (!item.Load(_serializeContext))
//                         {
//                             throw new FileLoadException($"Error in {r}");
//                         }
//                     }
//                 }
//             }
//
//             _scenario.InitialiseAfterGameLoad();
//         }
//     }
// }