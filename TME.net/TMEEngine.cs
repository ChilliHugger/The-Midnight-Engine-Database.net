using System;
using System.Runtime.CompilerServices;
using TME.Interfaces;

[assembly:InternalsVisibleTo("TME.UnitTests")]
[assembly:InternalsVisibleTo("TME.SpecTests")]

namespace TME
{
    public class TMEEngine : IEngine
    {
        private readonly IDatabase _database;

        public TMEEngine(IDatabase database)
        {
            _database = database;
        }

    }
}
