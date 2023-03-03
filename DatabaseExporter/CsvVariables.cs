using System;
using System.Collections.Generic;
using System.Linq;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace DatabaseExporter;

public class CsvVariables : ISerializableSave
{
    public IEnumerable<DatabaseVariable> Entries { get; set; } = Array.Empty<DatabaseVariable>();
    
    public bool Save(ISerializeContext context)
    {
        if (context.Section == DataSection.VariableCount)
        {
            context.Writer.UInt32((uint)Entries.Count());
            return true;
        }
        
        if (context.Section != DataSection.Variables)
        {
            return true;
        }
        
        foreach (var v in Entries.OrderBy(v=> v.Symbol))
        {
            v.Save(context);
        }

        return true;
    }
}