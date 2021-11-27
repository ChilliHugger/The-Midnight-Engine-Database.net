using System;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Flags;
using TME.Scenario.Default.Interfaces;
using TME.Serialize;
using TME.Types;

namespace TME
{
    public class TMEMap : IMap
    {
        private static readonly ID_4CC TMEMagicNo = ID_4CC.FromSig('T', 'M', 'E', '!');
        private const string MapHeader = "MidnightEngineMap";
        private const uint MapVersion = 2;

        public static MapLoc Zero = new();

        private MapLoc[]? _data;
        private Size _size;
        private int _totalSize;

        private Loc _topVisible;
        private Loc _bottomVisible;
    
        public bool TunnelsEnabled { get; internal set; }
        public bool MistEnabled { get; internal set; }
        
        public bool LoadFullMapFromStream(TMEBinaryReader stream)
        {
            var magicNo = stream.ReadUInt32();

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

            var version = stream.ReadUInt32();
            if (version != MapVersion)
            {
                return false;
            }

            var header = stream.ReadString();
            if (header != MapHeader)
            {
                return false;
            }

            return LoadSaveMapFromStream(stream);
        }


        public bool LoadSaveMapFromStream(TMEBinaryReader stream)
        {
            _size = stream.ReadSize();
            _totalSize = _size.Width * _size.Height;

            _data = new MapLoc[_totalSize];

            for (var ii = 0; ii < _totalSize; ii++)
            {
                _data[ii].Bits = stream.ReadUInt64();
            }

            CalculateVisibleArea();
            UpdateTunnelsAndMist();

            return true;
        }

        private void CalculateVisibleArea()
        {
            _topVisible = new Loc(_size.Width, _size.Height);
            _bottomVisible = new Loc(0,0);

            var loc = new Loc();

            for (var y = 0; y < _size.Height; y++)
            {
                for (var x = 0; x < _size.Width; x++)
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
                _bottomVisible.X = Math.Min(l.X, _size.Width);
            }
            if (l.Y <= _topVisible.Y)
            {
                _topVisible.Y = Math.Max(l.Y, 0);
            }
            if (l.Y >= _bottomVisible.Y)
            {
                _bottomVisible.Y = Math.Min(l.Y, _size.Height);
            }
        }

        public bool IsLocationVisible(Loc loc)
        {
            return GetAt(loc).IsVisible;
        }

        public bool IsLocOnMap(Loc loc)
        {
            return loc.X >= 0 && loc.X < _size.Width 
                && loc.Y >= 0 && loc.Y < _size.Height;
        }

        public void SetThing(Loc location, IThing thing)
        {
            var loc = GetAt(location);

            loc.Thing = (ThingType)thing.RawId;

            if (thing.IsUnique && thing is IMappableInternal mappable )
            {
                loc.HasObject = true;
                mappable.UpdateLocation(location);
            }
        }

        public MapLoc GetAt(Loc loc)
        {
            if (_data == null)
            {
                return Zero;
            }
            
            // out of bounds map reference
            if (!IsLocOnMap(loc))
            {
                return Zero;
            }

            var offset = loc.Y * _size.Width + loc.X;

            return _data[offset];
        }
        
        public void SetAt(Loc loc, ref MapLoc mapLoc)
        {
            if (_data == null)
            {
                return;
            }
            
            // out of bounds map reference
            if (!IsLocOnMap(loc))
            {
                return ;
            }

            var offset = loc.Y * _size.Width + loc.X;

            _data[offset] = mapLoc;
        }

        private void UpdateTunnelsAndMist()
        {           
            if (_data == null)
            {
                return;
            }

            for (var y = 0; y < _size.Height; y++)
            {
                for (var x = 0; x < _size.Width; x++)
                {
                    var offset = y * _size.Width + x;
                    var terrain = _data[offset].Terrain;

                    var tunnel = _data[offset].HasTunnel;
                    if (tunnel && terrain.IsTunnelExit())
                    {
                        _data[offset].SetFlags(LocationFlags.TunnelExit,true);
                    }
                    if (tunnel && terrain.IsTunnelEntrance())
                    {
                        _data[offset].SetFlags(LocationFlags.TunnelEntrance,true);
                    }
                    if (tunnel && terrain.IsTunnelPassage())
                    {
                        _data[offset].SetFlags(LocationFlags.TunnelPassageway,true);
                    }
                }
            }
            
        }
        
    }
}
