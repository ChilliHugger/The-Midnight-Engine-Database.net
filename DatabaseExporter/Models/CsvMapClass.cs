using System;
using CsvHelper.Configuration;

namespace DatabaseExporter.Models
{
    public abstract class CsvClassMap<TClass> : ClassMap<TClass>
    {
        protected string ConvertFlags<TEnum>(TEnum flags) where TEnum : Enum
        {
            return flags.ToString("F").Replace(", ", "+").ToUpper();
        }
    }
}