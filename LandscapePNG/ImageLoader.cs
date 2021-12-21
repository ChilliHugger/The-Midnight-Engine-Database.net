using System.IO;
using System.Reflection;
using SkiaSharp;

namespace LandscapePNG
{
    public class ImageLoader
    {
        public SKBitmap? Load(string resourceId)
        {
            var resourceStream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LandscapePNG.Resources."+resourceId);
            if (resourceStream == null)
            {
                return null;
            }
            using Stream stream = resourceStream;
            return SKBitmap.Decode(stream);
        }
    }
}