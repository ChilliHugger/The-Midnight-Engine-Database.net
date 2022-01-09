using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using TME.Extensions;
using TME.Landscaping;
using TME.Scenario.Default.Enums;

namespace LandscapePNG
{
    public class LandscapeTerrain
    {
        private readonly LandscapeGenerator _landscapeGenerator;

        private readonly Dictionary<Terrain, string> _terrain = new Dictionary<Terrain, string>
        {
            {Terrain.Cavern, "t_cavern0.png"},
            {Terrain.Citadel, "t_citadel0.png"},
            {Terrain.Downs, "t_downs0.png"},
            {Terrain.Forest, "t_forest0.png"},
            {Terrain.FrozenWaste, "t_frozenwaste0.png"},
            {Terrain.Henge, "t_henge0.png"},
            {Terrain.Keep, "t_keep0.png"},
            {Terrain.Lake, "t_lake0.png"},
            {Terrain.Lith, "t_lith0.png"},
            {Terrain.Mountain, "t_mountain0.png"},
            {Terrain.Ruin, "t_ruin0.png"},
            {Terrain.Snowhall, "t_snowhall0.png"},
            {Terrain.Tower, "t_tower0.png"},
            {Terrain.Village, "t_village0.png"},
        };

        private readonly Dictionary<Terrain, SKBitmap> _terrainImages = new Dictionary<Terrain, SKBitmap>();
        private readonly SKBitmap _army;
        private SKCanvas _canvas = null!;

        
        public LandscapeTerrain(
            LandscapeGenerator landscapeGenerator,
            ImageLoader imageLoader)
        {
            _landscapeGenerator = landscapeGenerator;
            foreach (var (key, value) in _terrain)
            {
                var image = imageLoader.Load(value);
                _terrainImages[key] = image ?? throw new FileNotFoundException(value);
            }
            
            _army = imageLoader.Load("t_army0.png") ?? throw new FileNotFoundException("t_army0.png");
        }

        public void Draw(SKCanvas canvas)
        {
            _canvas = canvas;
            
            foreach (var item in _landscapeGenerator.Items)
            {
                if (item.Position.Z 
                    is >= LandscapeGenerator.ViewportNear 
                    and < LandscapeGenerator.ViewportFar)
                { 
                    var alpha = 1.0f;
            
                    // fade anything too close - ie as we move through the locations
                    if (item.Position.Z<0.75f ) {
                        alpha = (item.Position.Z - LandscapeGenerator.ViewportNear )*2.0f;
                    }
                    
                    var y = item.Position.Y + _landscapeGenerator.HorizonOffset ;
                    var x = item.Position.X;
                    var scale = item.Scale;
            
                    if ( item.Position.Z >= 8.5f  && y < 0 ) {
                        
                        var range = LandscapeGenerator.ViewportFar - 8.5f ;
                        var diff = LandscapeGenerator.ViewportFar - item.Position.Z ;
                        alpha = -1.0f + (2.0f*(diff/range));
                        
                        y = 0 ;

                        if (item.Terrain.Normalise() == Terrain.Mountain
                            || item.Terrain.Normalise() == Terrain.FrozenWaste)
                        {
                            alpha = 1.0f;
                        }

                    }
            
                    // terrain
                    DrawGraphic(GetTerrainImage(item.Terrain),x,y,alpha,scale,item);
    
                    
                    
                    if ( item.Mist ) {
                        
                        //std::string mist = "t_mist";
                        //AddGraphic(GetImage(mist),x-LRES(76)*scale,y,tint1,tint2,1.0f,scale,item);
                        //AddGraphic(GetImage(mist),x+LRES(76+76)*scale,y,tint1,tint2,1.0f,scale,item);
                    }
                    else if ( item.Army ) {
        //#if defined(_DDR_)
        //                std::string army = "t_army2";
        //                AddGraphic(GetImage(army),x-LRES(76)*scale,y,tint1,tint2,1.0f,scale,item);
        //                AddGraphic(GetImage(army),x+LRES(76+76)*scale,y,tint1,tint2,1.0f,scale,item);
        //#else
                        DrawGraphic(_army,x,y,alpha,scale,item);
        //#endif
                    }
                }
 
            }
        }

        private SKBitmap? GetTerrainImage(Terrain terrain)
        {
            return _terrainImages[terrain];
        }
        
        private void DrawGraphic( SKBitmap? graphic, float x, float y, float alpha, float scale, LandscapeItem item)
        {
            if (graphic == null)
            {
                return;
            }

            var newScale = scale * 0.5f; // we have loaded hi-res images
            var width = (graphic.Width * newScale);
            var height = (graphic.Height * newScale);
            
            var newX = _landscapeGenerator.NormaliseXPosition(x) - (width/2.0f);
            var newY = y  - (graphic.Height*newScale);
            
            _canvas.DrawBitmap(graphic,
                new SKRect(
                    newX,
                    newY, 
                    newX + width, 
                    newY + height));
        }
    }
}