using SkiaSharp;
using TME.Landscaping;

namespace LandscapePNG
{
    public class LandscapeSky
    {
        public const float SkyHeight = 55 * LandscapeGenerator.LandscapeGScale;

        private readonly LandscapeGenerator _landscapeGenerator;
        private readonly SKBitmap? _sky;
        
        public LandscapeSky(
            LandscapeGenerator landscapeGenerator,
            ImageLoader imageLoader)
        {
            _landscapeGenerator = landscapeGenerator;
            _sky = imageLoader.Load("sky.png");
        }
        
        public void Draw(SKCanvas canvas)
        {
            var headerHeight = 57 * LandscapeGenerator.LandscapeGScale;
            canvas.DrawBitmap(_sky,
                new SKRect(
                    0,
                    headerHeight, 
                    _landscapeGenerator.LandscapeScreenWidth, 
                    headerHeight+SkyHeight));
        }
    }
}