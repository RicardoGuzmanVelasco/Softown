using System;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Foundation
    {
        public (int x, int y) Size { get; }

        public static Foundation SquareOf(int x) => new(x);
        Foundation(int x) : this(x, x) { }

        public static Foundation RectangleOf(int x, int y) => new(x, y);
        Foundation(int x, int y)
        {
            Assert.IsTrue(x > 0);
            Assert.IsTrue(y > 0);
            Size = (x, y);
        }
        
        public static Foundation Zero => new();

        public bool SameSizeThan(Foundation other) => Size.x + Size.y == other.Size.x + other.Size.y;

        public int Max() => Math.Max(Size.x, Size.y);

        public static implicit operator (int, int)(Foundation foundation)
        {
            return (X: foundation.Size.x, Y: foundation.Size.y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = Size.x;
            y = Size.y;
        }
    }
}