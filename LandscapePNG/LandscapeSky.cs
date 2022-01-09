using SkiaSharp;
using TME.Landscaping;

namespace LandscapePNG
{
    public class LandscapeSky
    {
    
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
            canvas.DrawBitmap(_sky,
                new SKRect(
                    0,
                    Landscaping.HeaderHeight, 
                    _landscapeGenerator.ViewportWidth, 
                    Landscaping.HeaderHeight+Landscaping.SkyHeight));
        }
    }
}