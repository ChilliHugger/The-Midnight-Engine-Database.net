using TME.Scenario.Default.Base;
using TME.Scenario.Default.Flags;
using TME.Serialize;
using TME.Types;

namespace TMXMapExporter;

public class BinaryMap
{
    public readonly MapLoc[] Data;
    public MapFlags Flags { get; set; } = MapFlags.None;

    public Size Dimensions { get; }
    private readonly int _size;

    public BinaryMap(int width, int height)
    {
        Dimensions = new Size(width, height);
        
        _size = width * height;

        Data = new MapLoc[_size];
    }

    public void Save(string filename)
    {
        using var writer = new TMEBinaryWriter(File.Open(filename, FileMode.Create));
        
        writer.UInt32(TMEInfo.TMEMagicNo);        
        writer.UInt32(TMEInfo.TMEMapVersion);        
        writer.String(TMEInfo.TMEMapHeader);
        writer.Size(Dimensions);   
        writer.UInt32((uint)Flags); 

        for (var ii = 0; ii < _size; ii++)
        {
            writer.UInt64(Data[ii].Bits);
        }
        
    }
    
}