using System;
namespace TME.Types
{
    public struct Size
    {
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
