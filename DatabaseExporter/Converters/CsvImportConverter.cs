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
    private readonly IStrings _strings;
    private readonly IEntityResolver _entityResolver;

    public CsvImportConverter(IEntityResolver entityResolver, IStrings strings )
    {
        _strings = strings;
        _entityResolver = entityResolver;
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

    public IEntity ToEntity<T>(string symbol)
        where T : IEntity
    {
        return _entityResolver.EntityBySymbol<T>(symbol);
    }

    public DatabaseString ToString(string symbol)
    {
        return _strings.GetBySymbol(symbol);
    }

    
    public T[] ToArray<T>(string value)
        where T : IEntity
    {
        return value
            .Split(',','|')
            .Select(s => s.Trim())
            .Select(s => _entityResolver.EntityBySymbol<T>(s))
            .ToArray();
    }
    
    public T ToEnum<T>(string symbol)
        where T : Enum
    {
        if (string.IsNullOrWhiteSpace(symbol)) return default;
        
        var info = _entityResolver.EntityBySymbol<IInfo>(symbol);
        return (T) (object)(info?.Id.RawId ?? 0);

    }
}