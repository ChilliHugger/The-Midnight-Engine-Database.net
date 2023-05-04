using System.Diagnostics;
using System.Text;
using TiledCS;
using TME;
using TME.Serialize;
using TMXMapExporter;

// running from ~/Projects/GitHub/The-Midnight-Engine-Database.net/data
var tileMap = new TiledMap("../../The-Lords-Of-Midnight/all/maps/lom_map_novel.tmx");
var processor = new MapProcessor(tileMap);
var output = processor.Process();
output.Save("./lom_novel/map");


/*
var tmeMap = new TMEMap();
using var reader = new TMEBinaryReader(File.Open("./lom/map", FileMode.Open));
tmeMap.LoadFullMapFromStream(reader);


if (tmeMap.Dimensions != output.Dimensions)
{
    Console.Out.WriteLine("Dimensions do not agree");
    //System.exit(-1);
}


var area1 = new StringBuilder();
var area2 = new StringBuilder();

var size = output.Dimensions.Width * output.Dimensions.Height;
var error = false;
for (int ii = 0; ii < size; ii++)
{
    var y = ii / output.Dimensions.Width;
    var x = ii % output.Dimensions.Width;

    var m1 = output.Data[ii];
    var m2 = tmeMap.Data[ii];

    if (m1.Terrain != m2.Terrain)
    {
        Console.Out.WriteLine($"Data[{ii}] ({x},{y}) Terrain inconsistent {m1.Terrain} != {m2.Terrain}");
    }
    if (m1.Area != m2.Area)
    {
        Console.Out.WriteLine($"Data[{ii}] ({x},{y}) Area inconsistent {m1.Area} != {m2.Area}");
        error = true;
    }
    
    if (m1.Thing != m2.Thing)
    {
        Console.Out.WriteLine($"Data[{ii}] ({x},{y}) Thing inconsistent {m1.Thing} != {m2.Thing}");
    }
    
    if (m1.Flags != m2.Flags)
    {
        Console.Out.WriteLine($"Data[{ii}] ({x},{y}) Flags inconsistent {m1.Flags} != {m2.Flags}");
    }
    
    //area1.AppendFormat("{0:x2}", m1.Area);
    //area2.AppendFormat("{0:x2}", m2.Area);
 
    //area1.AppendFormat("{0},", m1.Area);
    // if (m2.Area != 0)
    // {
    //     area2.AppendFormat("{0},", m2.Area+176);
    // }
    // else
    // {
    //     area2.AppendFormat("{0},", m2.Area);
    // }


    if (x == output.Dimensions.Width-1)
    {
        if (error)
        {
            //Console.Out.WriteLine(area1.ToString());
            Console.Out.WriteLine(area2.ToString());
            error = false;
        }
        
        area1.Clear();
        area2.Clear();
    }

    
}
*/

Console.Out.WriteLine("Complete!");
