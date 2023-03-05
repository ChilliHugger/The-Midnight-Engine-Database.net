using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Enums;
using TME.Types;
using Microsoft.Extensions.Logging;
using TME.Database;
using TME.Interfaces;

namespace TME.Serialize
{
    public class TMEDatabaseWriter : ISerializableSave
    {
        private readonly ILogger<TMEDatabase> _logger;
        private readonly IEnumerable<object> _writers;

        
        public TMEDatabaseWriter(
            ILogger<TMEDatabase> logger,
            IEnumerable<object> writers)
        {
            _logger = logger;
            _writers = writers;
        }

        public bool Save(ISerializeContext ctx)
        {
            var scenarioId = ctx.Scenario?.Info.Id ?? 0;
            var databaseVersion = (uint)(ctx.Scenario?.Info.DatabaseVersion ?? 0);

            // chunk 0
            ctx.Section = DataSection.None;
            ctx.Writer.UInt32(TMEInfo.TMEMagicNo);
            
            // chunk 1
            ctx.Section = DataSection.Header;
            ctx.Writer.UInt32(scenarioId);
            ctx.Writer.UInt32(databaseVersion);
            ctx.Writer.String(TMEInfo.TMEDatabaseHeader);
            
            // chunk 2
            ctx.Section = DataSection.SaveGameHeader;
            // skip
            
            foreach (var section in TMEInfo.Sections)
            {
                ctx.Section = section;
                foreach (var r in _writers.OfType<ISerializableSave>())
                {
                    if (r.Save(ctx)) continue;
                    _logger.LogError("Error in {r}", r);
                    return false;
                }
            }

            return true;
        }
        
    }
}