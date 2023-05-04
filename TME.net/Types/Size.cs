// ReSharper disable FieldCanBeMadeReadOnly.Global

using System;

namespace TME.Types
{
    public struct Size : IEquatable<Size>
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

        public bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public static bool operator ==(Size lhs, Size rhs) => lhs.Equals(rhs);

        public static bool operator !=(Size lhs, Size rhs) => !(lhs == rhs);
        
        public override bool Equals(object? obj)
        {
            return obj is Size other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }
    }
}
