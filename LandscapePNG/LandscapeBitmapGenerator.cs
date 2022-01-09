using System.IO;
using SkiaSharp;
using TME.Interfaces;
using TME.Landscaping;
using TME.Scenario.Default.Base;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;

namespace LandscapePNG
{
    public class LandscapeBitmapGenerator
    {
        private readonly IEntityResolver _entityResolver;
        private readonly LandscapeLand _landscapeLand;
        private readonly LandscapeSky _landscapeSky;
        private readonly LandscapeTerrain _landscapeTerrain;
        private readonly LandscapeGenerator _landscapeGenerator;
        
        public LandscapeBitmapGenerator(
            IEntityResolver entityResolver,
            LandscapeLand landscapeLand,
            LandscapeSky landscapeSky,
            LandscapeTerrain landscapeTerrain,
            LandscapeGenerator landscapeGenerator)
        {
            _entityResolver = entityResolver;
            _landscapeLand = landscapeLand;
            _landscapeSky = landscapeSky;
            _landscapeTerrain = landscapeTerrain;
            _landscapeGenerator = landscapeGenerator;
        }
        
        public void Build()
        {
            Generate();

            var imageInfo = new SKImageInfo((int)_landscapeGenerator.ViewportWidth, 768);
            using SKSurface surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            // BASED ON (0,0) top left
            
            var paint = new SKPaint {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Gray
            };          
            canvas.DrawRect(0,0,imageInfo.Width, Landscaping.HeaderHeight, paint );

            _landscapeSky.Draw(canvas);
            _landscapeLand.Draw(canvas);
            _landscapeTerrain.Draw(canvas);
            
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite("screenshot.png");
            data.SaveTo(stream);
        }

        private void Generate()
        {
            var luxor = _entityResolver.EntityBySymbol<ICharacter>("CH_LUXOR");
            if (luxor == null)
            {
                return;
            }

            var currentLoc = luxor.Location + Direction.South;
            var looking = Direction.North; // luxor.Looking;
            
            var lookAmount = (float) (looking-1) * Landscaping.DirAmount;
            if (lookAmount > Landscaping.FullWidth)
            {
                lookAmount -= Landscaping.FullWidth;
            }

            var options = new LandscapeOptions
            {
                Here = currentLoc,
                TimeOfDay = luxor.Time,
                LookAmount = lookAmount,
                CurrentDirection = looking,
                CurrentLocation = luxor.Location,
                AheadLocation = luxor.Location + looking,
                IsLooking = false,
                IsMoving = false,
                IsInTunnel = false,
                IsLookingDownTunnel = false,
                IsLookingOutTunnel = false,
                LookOffsetAdjustment = -512 // centre view in the middle
            };

            _landscapeGenerator.HorizontalOffset = 0;
            _landscapeGenerator.ViewportWidth = 2048;
            _landscapeGenerator.Build(options);
        }
    }
}