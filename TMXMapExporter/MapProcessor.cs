namespace TMXMapExporter;

using TiledCS;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;

internal enum Flags
{
    Domain,
    Tunnel,
    Mist,
    Creature,
    Unused1,
    Unused2,
    Unused3,
    Unused4,
    Path,
    Impassable,
    Respawn,
    TunnelEntrance,
    TunnelExit,
    TunnelEntranceExit,
    TunnelSmall,
}

public class MapProcessor
{
/*
 TileSets should have names so they can be located, not in TiledCS yet - so workaround for now
 TileSet[0] = terrain
 TileSet[1] = flags
 TileSet[2] = things
 TileSet[3] = areas
 */

/*
 *   lf_tunnel
 *   lf_tunnel_exit
 *   lf_tunnel_entrance
 * 
 *   IsTunnelPassageWay
 *   mapsqr.HasTunnel() && (t ==TN_PLAINS2 || t==TN_MOUNTAIN2 || t==TN_FOREST2 || t==TN_HILLS ) ;
 *
 *  TN_GATE             =    20, // 4 tunnel exit/entrance
 *  TN_TEMPLE           =    21, // 5 tunnel exit/entrance
 *  TN_PIT              =    22, // 6 tunnel exit/entrance
 *  TN_PALACE           =    23, // 7 tunnel exit/entrance
 */

    private readonly BinaryMap _outputMap;
    private TiledMap map;

    public MapProcessor(TiledMap map)
    {
        this.map = map;
        _outputMap = new BinaryMap(map.Width, map.Height);
    }

    public BinaryMap Process()
    {
        var terrain = map.Layers.First(l => l.name == "Terrain");
        var domain = map.Layers.First(l => l.name == "Domain");
        var tunnel = map.Layers.First(l => l.name == "Tunnels");
        var mist = map.Layers.First(l => l.name == "Mist");
        var creature = map.Layers.First(l => l.name == "Creature");
        var thing = map.Layers.First(l => l.name == "Things");
        var area = map.Layers.First(l => l.name == "Area");

        var terrainGId = map.Tilesets[0].firstgid;
        var areaGId = map.Tilesets[3].firstgid;
        var thingGId = map.Tilesets[2].firstgid;

        CheckFlags(domain);
        CheckFlags(tunnel);
        CheckFlags(creature);
        CheckFlags(mist);

        var index = 0;
        // read terrain
        foreach (var t in terrain.data)
        {
            _outputMap.Data[index].Terrain = (Terrain) (t - terrainGId + 1);
            index++;
        }

        // read area
        index = 0;
        foreach (var a in area.data)
        {
            if (a != 0)
            {
                _outputMap.Data[index].Area = (ushort) (a - areaGId + 1);
            }

            index++;
        }

        // read thing
        index = 0;
        foreach (var t in thing.data)
        {
            if (t != 0)
            {
                _outputMap.Data[index].Thing = (ThingType) (t - thingGId + 1);
            }

            index++;
        }

        return _outputMap;
    }

    private void CheckFlags(TiledLayer layer)
    {
        var flagsGId = map.Tilesets[1].firstgid;

        // read tunnels
        var index = 0;
        foreach (var t in layer.data)
        {
            if (t != 0)
            {
                var flags = (Flags) (t - flagsGId);

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (flags)
                {
                    case Flags.Domain:
                        _outputMap.Data[index].SetFlags(LocationFlags.Domain, true);
                        break;
                    case Flags.Tunnel:
                        _outputMap.Data[index].SetFlags(LocationFlags.Tunnel, true);
                        break;
                    case Flags.Mist:
                        _outputMap.Data[index].SetFlags(LocationFlags.Mist, true);
                        break;
                    case Flags.Impassable:
                        _outputMap.Data[index].SetFlags(LocationFlags.Impassable, true);
                        break;
                    case Flags.Respawn:
                        _outputMap.Data[index].SetFlags(LocationFlags.Respawn, true);
                        break;
                    case Flags.Creature:
                        _outputMap.Data[index].SetFlags(LocationFlags.Creature, true);
                        break;
                    case Flags.TunnelSmall:
                        _outputMap.Data[index].SetFlags(LocationFlags.TunnelSmall|LocationFlags.Tunnel, true);
                        break;
                    case Flags.TunnelEntrance:
                        _outputMap.Data[index].SetFlags(LocationFlags.TunnelEntrance|LocationFlags.Tunnel, true);
                        _outputMap.Flags |= MapFlags.TunnelEndpoints;
                        break;
                    case Flags.TunnelExit:
                        _outputMap.Data[index].SetFlags(LocationFlags.TunnelExit|LocationFlags.Tunnel, true);
                        _outputMap.Flags |= MapFlags.TunnelEndpoints;
                        break;
                    case Flags.TunnelEntranceExit:
                        _outputMap.Data[index].SetFlags(LocationFlags.TunnelEntrance|LocationFlags.TunnelExit|LocationFlags.Tunnel, true);
                        _outputMap.Flags |= MapFlags.TunnelEndpoints;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            index++;
        }
    }
}