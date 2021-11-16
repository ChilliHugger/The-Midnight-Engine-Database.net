using TME.Default.Interfaces;
using TME.Scenario.Default.Base;
using TME.Serialize;

namespace TME.Interfaces
{
    public interface IMap
    {
        bool LoadSaveMapFromStream(TMEBinaryReader stream);
        bool LoadFullMapFromStream(TMEBinaryReader stream);

        MapLoc GetAt(Loc loc);
        void SetThing(Loc location, IThing thing);
    }
}
