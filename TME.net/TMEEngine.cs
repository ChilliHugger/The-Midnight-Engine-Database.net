using System;
using TME.Interfaces;

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
