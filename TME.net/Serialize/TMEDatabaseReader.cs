using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Enums;
using Microsoft.Extensions.Logging;
using TME.Database;

namespace TME.Serialize
{
    public class TMEDatabaseReader : ISerializableLoad
    {
        private readonly ILogger<TMEDatabase> _logger;
        private readonly IEnumerable<object> _readers;

        public uint ScenarioId { get; private set; }
        public uint Version { get; private set; }
        
        public TMEDatabaseReader(
            ILogger<TMEDatabase> logger,
            IEnumerable<object> readers)
        {
            _logger = logger;
            _readers = readers;
        }

        private bool ReadMagicNo(ISerializeContext ctx)
        {
            var magicNo = ctx.Reader.UInt32();
            if (magicNo == TMEInfo.TMEMagicNo) return true;
            
            var number = BinaryPrimitives.ReverseEndianness(TMEInfo.TMEMagicNo);
            if (magicNo != number)
            {
                _logger.LogError("Database MagicNo '{magicNo}' does not match '{TMEMagicNo}'",
                    magicNo, TMEInfo.TMEMagicNo);
                return false;
            }

            //ctx.Reader.EnableByteSwap = true;
            _logger.LogError("Database Byte Order swapping required!");
            return false;
        }

        private bool ReadHeader(ISerializeContext ctx)
        {
            var validScenarioId = ctx.Scenario?.Info.Id ?? 0;
            var validDatabaseVersion = ctx.Scenario?.Info.DatabaseVersion ?? 0;
            
            ScenarioId = ctx.Reader.UInt32();
            if (ScenarioId != validScenarioId)
            {
                _logger.LogError("Database ScenarioId '{ScenarioId}' does not match '{validScenarioId}'",
                    ScenarioId, validScenarioId);
                return false;
            }

            Version = ctx.Reader.UInt32();
            if (Version < validDatabaseVersion)
            {
                _logger.LogError(
                    "Database Version '{Version}' is less than scenario version '{validDatabaseVersion}'",
                    Version, validDatabaseVersion);
                return false;
            }

            ctx.Version = Version;
            
            var header = ctx.Reader.String();
            if(header != TMEInfo.TMEDatabaseHeader)
            {
                _logger.LogError(
                    "Database Header '{Header}' is less than scenario version '{DatabaseHeader}'",
                    header, TMEInfo.TMEDatabaseHeader);
                return false;
            }

            return true;
        }
        
        public bool Load(ISerializeContext ctx)
        {
            // chunk 0
            ctx.Section = DataSection.None;
            if (!ReadMagicNo(ctx)) return false;
            
            // chunk 1
            ctx.Section = DataSection.Header;
            if (!ReadHeader(ctx)) return false;
            
            // chunk 2
            ctx.Section = DataSection.SaveGameHeader;
            // skip
            
            foreach (var section in TMEInfo.Sections)
            {
                ctx.Section = section;
                foreach (var r in _readers.OfType<ISerializableLoad>())
                {
                    if (r.Load(ctx)) continue;
                    _logger.LogError("Error in {r}", r);
                    return false;
                }
            }

            return true;
        }
        
    }
}