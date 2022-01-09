using SkiaSharp;
using TME.Landscaping;

namespace LandscapePNG
{
    public class LandscapeLand
    {
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
            canvas.DrawBitmap(_floor,
                new SKRect(
                0,
                Landscaping.HeaderHeight+Landscaping.SkyHeight,
                _landscapeGenerator.ViewportWidth,
                Landscaping.HeaderHeight+Landscaping.SkyHeight+Landscaping.FloorHeight));
        }
    }
}