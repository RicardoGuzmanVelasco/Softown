using System;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Foundation
    {
        public (int x, int y) Size => (X, Y);
        public int X { get; }
        public int Y { get; }

        Foundation(int x) : this(x, x) { }
        public static Foundation SquareOf(int x)
        {
            return new(x);
        }

        Foundation(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Foundation RectangleOf(int x, int y)
        {
            return new(x, y);
        }

        public bool SameSizeThan(Foundation other) => X + Y == other.X + other.Y;
        
        public Foundation Rotate() => RectangleOf(Y, X);

        public int Max() => Math.Max(X, Y);

        public static implicit operator (int, int)(Foundation foundation)
        {
            return (foundation.X, foundation.Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}