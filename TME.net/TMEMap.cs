using System;
using TME.Default.Interfaces;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Serialize;
using TME.Types;

namespace TME
{
    public class TMEMap : IMap
    {
        private readonly static ID_4CC TME_MAGIC_NO = ID_4CC.FromSig('T', 'M', 'E', '!');

        private const string MAPHEADER = "MidnightEngineMap";
        private const uint MAPVERSION = 2;

        public static MapLoc ZERO = new MapLoc();

        private MapLoc[] _data;
        private Size _size = new Size(0, 0);
        private int _totalSize = 0;

        private Loc _topVisible = new Loc();
        private Loc _bottomVisible = new Loc();

        public bool LoadFullMapFromStream(TMEBinaryReader stream)
        {
            var magicNo = stream.ReadUInt32();

            if (magicNo == TME_MAGIC_NO)
            {
            }
            else if (magicNo == TME_MAGIC_NO.Reverse())
            {
            }
            else
            {
                return false;
            }

            var version = stream.ReadUInt32();
            if (version != MAPVERSION)
            {
                return false;
            }

            var header = stream.ReadString();
            if (header != MAPHEADER)
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

            for (int ii = 0; ii < _totalSize; ii++)
            {
                _data[ii].bits = stream.ReadUInt64();
            }

            CalculateVisibleArea();

            return true;
        }

        private void CalculateVisibleArea()
        {
            _topVisible = new Loc(_size.Width, _size.Height);
            _bottomVisible = new Loc(0,0);

            var loc = new Loc();

            for (int y = 0; y < _size.Height; y++)
            {
                for (int x = 0; x < _size.Width; x++)
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

        bool IsLocationVisible(Loc loc)
        {
            return GetAt(loc).IsVisible;
        }

        public bool IsLocOnMap(Loc loc)
        {
            return (loc.X >= 0 && loc.X < _size.Width) && (loc.Y >= 0 && loc.Y < _size.Height);
        }

        public void SetThing(Loc location, IThing thing)
        {
            var loc = GetAt(location);

            loc.Thing = (ThingType)thing.RawId;

            if (thing.IsUnique)
            {
                loc.HasObject = true;
                thing.Location = location;
            }
        }

        public MapLoc GetAt(Loc loc)
        {
            // out of bounds map reference
            if (!IsLocOnMap(loc))
            {
                return ZERO;
            }

            int offset = ((loc.Y) * _size.Width) + (loc.X);

            return _data[offset];
        }
    }
}
