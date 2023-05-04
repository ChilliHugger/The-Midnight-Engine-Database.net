using System;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;
// ReSharper disable MemberCanBePrivate.Global

namespace TME
{
    public class TMEMap : IMap
    {
        private static readonly ID_4CC TMEMagicNo = ID_4CC.FromSig('T', 'M', 'E', '!');
        private const string MapHeader = "MidnightEngineMap";
        private const uint MapVersion = 3;

        public static MapLoc Zero = new();

        public MapLoc[] Data { get; private set; } = Array.Empty<MapLoc>();
        public Size Dimensions { get; private set; }
        private int _size;
        private uint _version;
        private MapFlags _flags;

        private Loc _topVisible;
        private Loc _bottomVisible;
    
        public bool TunnelsEnabled { get; internal set; }
        public bool MistEnabled { get; internal set; }

        //public MapLoc[] Data => _data;

        public bool LoadFullMapFromStream(TMEBinaryReader stream)
        {
            var magicNo = stream.UInt32();

            if (magicNo == TMEMagicNo)
            {
            }
            else if (magicNo == TMEMagicNo.Reverse())
            {
            }
            else
            {
                return false;
            }

            _version = stream.UInt32();
            
            var header = stream.String();
            if (header != MapHeader)
            {
                return false;
            }
            
            return LoadSaveMapFromStream(stream);
        }


        public bool LoadSaveMapFromStream(TMEBinaryReader stream)
        {
            Dimensions = stream.Size();
            _size = Dimensions.Width * Dimensions.Height;
            Data = new MapLoc[_size];
            
            if (_version >= 3)
            {
                _flags = (MapFlags) stream.UInt32();
            }

            for (var ii = 0; ii < _size; ii++)
            {
                Data[ii].Bits = stream.UInt64();
            }

            CalculateVisibleArea();
            UpdateTunnelsAndMist();

            return true;
        }

        private void CalculateVisibleArea()
        {
            _topVisible = new Loc(Dimensions.Width, Dimensions.Height);
            _bottomVisible = new Loc(0,0);

            var loc = new Loc();

            for (var y = 0; y < Dimensions.Height; y++)
            {
                for (var x = 0; x < Dimensions.Width; x++)
                {
                    loc.X = x;
                    loc.Y = y;
                    if (IsLocationVisible(loc))
                    {
                        CheckVisibleRange(loc);
                    }
                }
            }
        }

        private void CheckVisibleRange(Loc l)
        {
            if (l.X <= _topVisible.X)
            {
                _topVisible.X = Math.Max(l.X, 0);
            }
            if (l.X >= _bottomVisible.X)
            {
                _bottomVisible.X = Math.Min(l.X, Dimensions.Width);
            }
            if (l.Y <= _topVisible.Y)
            {
                _topVisible.Y = Math.Max(l.Y, 0);
            }
            if (l.Y >= _bottomVisible.Y)
            {
                _bottomVisible.Y = Math.Min(l.Y, Dimensions.Height);
            }
        }

        public bool IsLocationVisible(Loc loc)
        {
            return GetAt(loc).IsVisible;
        }

        public bool IsLocOnMap(Loc loc)
        {
            return loc.X >= 0 && loc.X < Dimensions.Width 
                && loc.Y >= 0 && loc.Y < Dimensions.Height;
        }

        public void SetThing(Loc location, IObject thing)
        {
            var loc = GetAt(location);

            loc.Thing = (ThingType)thing.RawId;

            if (thing.IsUnique() && thing is IMappableInternal mappable )
            {
                loc.HasObject = true;
                mappable.Location = location;
            }
        }

        public MapLoc GetAt(Loc loc)
        {
            if (Data == null)
            {
                return Zero;
            }
            
            // out of bounds map reference
            if (!IsLocOnMap(loc))
            {
                return Zero;
            }

            var offset = loc.Y * Dimensions.Width + loc.X;

            return Data[offset];
        }
        
        public void SetAt(Loc loc, ref MapLoc mapLoc)
        {
            if (Data == null)
            {
                return;
            }
            
            // out of bounds map reference
            if (!IsLocOnMap(loc))
            {
                return ;
            }

            var offset = loc.Y * Dimensions.Width + loc.X;

            Data[offset] = mapLoc;
        }

        private void UpdateTunnelsAndMist()
        {           
            if (Data == null)
            {
                return;
            }

            for (var y = 0; y < Dimensions.Height; y++)
            {
                for (var x = 0; x < Dimensions.Width; x++)
                {
                    var offset = y * Dimensions.Width + x;
                    var terrain = Data[offset].Terrain;
                    
                    if (Data[offset].HasTunnel)
                    {
                        if (!_flags.HasFlag(MapFlags.TunnelEndpoints))
                        {
                            if (terrain.IsTunnelExit())
                            {
                                Data[offset].SetFlags(LocationFlags.TunnelExit, true);
                            }

                            if (terrain.IsTunnelEntrance())
                            {
                                Data[offset].SetFlags(LocationFlags.TunnelEntrance, true);
                            }
                        }

                        if (terrain.IsTunnelPassage())
                        {
                            Data[offset].SetFlags(LocationFlags.TunnelPassageway, true);
                        }
                    }
                }
            }
            
        }
        
    }
}
