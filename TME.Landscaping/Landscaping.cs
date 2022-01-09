namespace TME.Landscaping
{
    public static class Landscaping
    {

        // During the remake I based everything on the iPad resolution 1024x768
        // which is actually 4 times the Spectrum resolution
        public const float Scale = 4.0f;

        // spectrum screen was 256x192
        // Header was 57
        // Sky was 55  
        // floor was 80 height
        // location in front was at 48 pixels from the bottom
        // thus the panorama height was 32
        // we need a 3 pixel horizon adjustment to put the far locations on the horizon
        public const float HeaderHeight = 57 * Scale;
        public const float SkyHeight = 55 * Scale;
        public const float FloorHeight = 80 * Scale;
    
        public const float DirAmount = 400.0f;
        public const float FullWidth = DirAmount * 8;
        public const float DirSteps = 400.0f;
        public const float Dir = DirAmount / DirSteps;
        public const float DirLeft = -Dir;
        public const float DirRight = Dir;

        public const float HorizonCentreX = 256 * Landscaping.Scale / 2;
        public const float HorizonCentreY = 0 ; 
        public const float PanoramaWidth =  800.0f*Landscaping.Scale;
        public const float PanoramaHeight = 38.0f*Landscaping.Scale;
        public const float LocationHeight = 48.0f*Landscaping.Scale;
        public const float HorizonAdjust = 5*Landscaping.Scale;
        public const float HorizonOffset = Landscaping.HeaderHeight+Landscaping.SkyHeight;
    }
}