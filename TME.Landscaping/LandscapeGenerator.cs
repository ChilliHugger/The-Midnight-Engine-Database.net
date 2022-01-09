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
        public const int PeopleWindowHeight = 256;
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
        public float ViewportWidth { get; set; }
        
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
            
            HorizonCentreX = LRES(Landscaping.HorizonCentreX) - options.LookOffsetAdjustment;
            HorizonCentreY = LRES(Landscaping.HorizonCentreY) ; 
            PanoramaWidth =  LRES(Landscaping.PanoramaWidth);
            PanoramaHeight = LRES(Landscaping.PanoramaHeight);
            LocationHeight = LRES(Landscaping.LocationHeight);
            HorizonAdjust = LRES(Landscaping.HorizonAdjust);
            HorizonOffset = LRES(Landscaping.HorizonOffset);
            
            // Options.Here will be the x,y coordinate of the map location 
            // multiplied by LandscapeDirSteps
            // this allows for the tween when moving
            _loc = new Loc(
                options.Here.X * (int) Landscaping.DirSteps,
                options.Here.Y * (int) Landscaping.DirSteps);
            
            _looking = 0;
    
            Items.Clear();

            if (!_options.IsInTunnel)
            {
                BuildPanorama();
            }
        }

        private void BuildPanorama()
        {
            var x = _loc.X / Landscaping.DirSteps;
            var y = _loc.Y / Landscaping.DirSteps;
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
        
        LandscapeItem CalcCylindricalProjection(LandscapeItem item)
        {
            var x = ( item.Loc.X*Landscaping.DirSteps - _loc.X) / Landscaping.DirSteps;
            var y = ( item.Loc.Y*Landscaping.DirSteps - _loc.Y) / Landscaping.DirSteps;
            
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
            const float boundary = Landscaping.DirSteps*3;
            var maxScreenX = ViewportWidth+LRES(512);
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