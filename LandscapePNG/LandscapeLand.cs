using System.Drawing;
using SkiaSharp;
using TME.Landscaping;

namespace LandscapePNG
{
    public class LandscapeLand
    {
        public const float FloorHeight = 80 * LandscapeGenerator.LandscapeGScale;

        private readonly LandscapeGenerator _landscapeGenerator;
        private readonly SKBitmap? _floor;
        
        public LandscapeLand(
            LandscapeGenerator landscapeGenerator,
            ImageLoader imageLoader)
        {
            _landscapeGenerator = landscapeGenerator;
            _floor = imageLoader.Load("floor.png");
        }
        
        public void Draw(SKCanvas canvas)
        {
            var headerHeight = 57 * LandscapeGenerator.LandscapeGScale;
            canvas.DrawBitmap(_floor,
                new SKRect(
                0,
                headerHeight+LandscapeSky.SkyHeight,
                _landscapeGenerator.LandscapeScreenWidth,
                (headerHeight+LandscapeSky.SkyHeight*2)+FloorHeight));
        }
    }
}