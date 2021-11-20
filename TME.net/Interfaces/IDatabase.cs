using System.Collections.Generic;
using TME.Scenario.Default.Interfaces;
using TME.Types;

namespace TME.Interfaces
{
    public interface IDatabase
    {
        string Directory { get; set; }
        IMap GameMap { get; set; }

        bool Load();
        bool Unload();
    }
}
