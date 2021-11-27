using TME.Scenario.Default.Base;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;

namespace TME.Interfaces
{
    public interface IMap
    {
        bool LoadSaveMapFromStream(TMEBinaryReader stream);
        bool LoadFullMapFromStream(TMEBinaryReader stream);

        MapLoc GetAt(Loc location);
        void SetAt(Loc location, ref MapLoc mapLoc);
        void SetThing(Loc location, IThing thing);
        
        bool TunnelsEnabled { get; }
        bool MistEnabled { get; }
        
    }
}
