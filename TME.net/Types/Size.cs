// ReSharper disable FieldCanBeMadeReadOnly.Global
namespace TME.Types
{
    public struct Size
    {
        public static readonly Size Zero = new Size(0,0);
        
        public int Width;
        public int Height;

        public Size(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public override string ToString() => $"({Width}, {Height})";
    }
}
