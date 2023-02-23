using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace DatabaseExporter.Converters;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class CsvImportConverter
{
    public IEntityResolver EntityResolver { get; set; }

    public CsvImportConverter(IEntityResolver entityResolver)
    {
        EntityResolver = entityResolver;
    }

    public MXId ToId(EntityType type, int id) => new MXId(type, (uint) id);

    public T ToFlags<T>(string flags)
        where T : Enum
    {
        if (string.IsNullOrWhiteSpace(flags)) return default;
        var input = flags.Replace("+", ",");
        return (T)Enum.Parse(typeof(T), input, ignoreCase: true);
    }

    public Loc ToLoc(string loc)
    {
        var values = loc.Split(",").Select(int.Parse).ToArray();
        return new Loc(values[0], values[1]);
    }

    public T[] ToArray<T>(string value)
        where T : IEntity
    {
        return value
            .Split(",")
            .Select(s => s.Trim())
            .Select(s => EntityResolver.EntityBySymbol<T>(s))
            .ToArray();
    }
}