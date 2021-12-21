using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TME.Extensions;
using TME.Interfaces;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.info;

namespace TME.Landscaping
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class LandscapeGenerator
    {
        private readonly IEntityResolver _entityResolver;
        private readonly IMap _map;

        // adjustment for the furthest of our visible locations being on the horizon
        public const float ViewportNear = 0.25f;
        public const float ViewportFar = 6.5f;
        public const float BaseHeight = 768;
        public const float BaseWidth = 1024;
        public const int PeopleWindowHeight = 256;
        public const float LandscapeDirAmount = 400.0f;
        public const float LandscapeDirSteps = 400.0f;
        public const float LandscapeDir = LandscapeDirAmount / LandscapeDirSteps;
        public const float LandscapeDirLeft = -LandscapeDir;
        public const float LandscapeDirRight = LandscapeDir;
        public const float LandscapeGScale = 4.0f;
        public const float LandscapeFullWidth = LandscapeDirAmount * 8;
        private const double Pi2 = Math.PI * 2.0;
        
        private LandscapeOptions _options;

        public float HorizonCentreX { get; private set; }
        public float HorizonCentreY { get; private set; }
        public float PanoramaWidth { get; private set; }
        public float PanoramaHeight { get; private set; }
        public float LocationHeight { get; private set; }
        public float HorizonAdjust { get; private set; }
        public float HorizonOffset { get; private set; }
        public float HorizontalOffset { get; set; }
        public float LandscapeScreenWidth { get; set; }
        
        private Loc _loc;
        private int _looking;
        public List<LandscapeItem> Items { get; set; } = new List<LandscapeItem>();
        
        // Resolution adjuster
        private static float LRES(float value) => value * 1.0f;


        public LandscapeGenerator(
            IEntityResolver entityResolver,
            IMap map)
        {
            _entityResolver = entityResolver;
            _map = map;
        }
        
        
        public void Build(LandscapeOptions options)
        {
            _options = options;
            
            HorizonCentreX = LRES(256*LandscapeGScale/2) - options.LookOffsetAdjustment;
            HorizonCentreY = 0 ; 
            PanoramaWidth =  LRES(800.0f*LandscapeGScale);
            PanoramaHeight = LRES(38.0f*LandscapeGScale);
            LocationHeight = LRES(48.0f*LandscapeGScale);
            HorizonAdjust = LRES(5*LandscapeGScale);
            HorizonOffset = LRES(112*LandscapeGScale);
            
            // Options.Here will be the x,y coordinate of the map location 
            // multiplied by LandscapeDirSteps
            // this allows for the tween when moving
            _loc = _options.Here;
            _looking = 0;
    
            Items.Clear();

            if (!_options.IsInTunnel)
            {
                BuildPanorama();
            }
        }

        private void BuildPanorama()
        {
            var x = _loc.X / LandscapeDirSteps;
            var y = _loc.Y / LandscapeDirSteps;
            const int qDim = 8;
    
            for ( var y1=y-qDim; y1<=y+qDim; y1++ ) {
                for ( var x1=x-qDim; x1<=x+qDim; x1++ ) {
                    var cell = ProcessLocation((int)x1, (int)y1);
                    if ( cell != null )
                        Items.Add(cell);
                }
            }
            
            Items = Items.OrderByDescending( i => i.Position.Z ).ToList();
        }
        
        private LandscapeItem ProcessLocation(int x, int y)
        {
            
            var mapLoc = _map.GetAt(new Loc(x, y));
            var terrainInfo = _entityResolver.EntityById<TerrainInfo>((int)mapLoc.Terrain.Raw());
            if (terrainInfo == null) {
                return null;
            }
            
            var item = new LandscapeItem {
                Loc = new Loc(x,y),
                Floor = GetFloorTypeFromTerrain(mapLoc.Terrain),
                Army = mapLoc.HasArmy && terrainInfo.IsArmyVisible,
                Mist = mapLoc.IsMisty,
                Position = Vector3.Zero,
                Terrain = mapLoc.Terrain
            };
        
            if ( !_options.IsMoving && item.Army ) {
                if (item.Loc == _options.CurrentLocation) {
                    item.Army = false;
                }
            }
    
            // check the current lords army temporarily
            // popping up as we move in or out of a location
            if (_options.IsMoving && item.Army) {
                if (item.Loc == _options.MoveFrom ||
                    item.Loc == _options.MoveTo
                    && !_options.MoveLocationHasArmy) {
                    item.Army = false;
                }
            }

    
            return CalcCylindricalProjection(item);
    
        }

        private static FloorType GetFloorTypeFromTerrain(Terrain terrain)
        {
            return terrain switch {
                Terrain.Lake3 => FloorType.Lake,
                Terrain.River => FloorType.River,
                Terrain.Sea or Terrain.Bay => FloorType.Sea,
                Terrain.IcyWaste or Terrain.FrozenWaste => FloorType.Snow,
                _ => FloorType.Normal
            };
        }

        // spectrum screen was 256x192
        // Sky was 112  height
        // floor was 80 height
        // location in front was at 48 pixels from the bottom
        // thus the panorama height was 32
        // we need a 3 pixel horizon adjustment to put the far locations on the horizon

        LandscapeItem CalcCylindricalProjection(LandscapeItem item)
        {
            var x = ( item.Loc.X*LandscapeDirSteps - _loc.X) / LandscapeDirSteps;
            var y = ( item.Loc.Y*LandscapeDirSteps - _loc.Y) / LandscapeDirSteps;
            
            // TODO: I think _looking is now always 0
            // because of how we are generating the whole landscape
            // so this can be optimised away! Therefore ViewAngle will always be 0
            var viewAngle = RadiansFromFixedPointAngle( _looking );
	
            var objAngle = Math.Atan2(x, -y);
            
            var angle = objAngle - viewAngle;

            if (angle > Pi2) {
                angle -= Pi2;
            }

            if (angle < -Pi2) {
                angle += Pi2;
            }

            //	convert angle to horizon centre xOffset (cylindrical projection)
            var xOff = (float)(angle*PanoramaWidth/Pi2);
    
            //	now do the horizon centre yOffset perspective projection
            var newZ = (float)Math.Sqrt(x*x + y*y);
    
            item.Scale = 1.0f / newZ;
    
            var yOff = PanoramaHeight * item.Scale;
    
            var newX = xOff + HorizonCentreX;
            var newY = yOff + HorizonCentreY - HorizonAdjust;
    
            // We are running a panorama that runs from N to NW along a linear
            // so place all locations to the right
            if (newX <= LRES(-225)) {
                newX += PanoramaWidth;
            }

            item.Position = new Vector3(newX, newY, newZ);
            
            return item;
        }

        private double RadiansFromFixedPointAngle(int value)
        {
            var angle = (double)value;
            return angle*Pi2/4096.0d;
        }
        
        //
        // X coordinate is in Scaled Panorama units (ie: real screen units)
        //
        public float NormaliseXPosition(float x)
        {
            var newX = x-LRES(HorizontalOffset) ;
    
            // Boundary in panoramic units
            const float boundary = LandscapeDirSteps*3;
            var maxScreenX = LandscapeScreenWidth+LRES(512);
            var minScreenX = 0 - LRES(-512);
    
            // to the left
            if ( HorizontalOffset<boundary && newX>maxScreenX ) {
                newX -= PanoramaWidth;
            }

            // to the right
            if ( HorizontalOffset>=boundary && newX<minScreenX ) {
                newX += PanoramaWidth;
            }

            return newX;
        }

    }
}