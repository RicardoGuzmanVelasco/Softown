using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public record Foundation : Block
    {
        public static Foundation SquareOf(int x) => new(x);
        Foundation(int x) : this(x, x) { }
        Foundation() { }
        public static Foundation RectangleOf(int x, int y) => new(x, y);

        Foundation(int x, int y)
        {
            Assert.IsTrue(x > 0);
            Assert.IsTrue(y > 0);
            Size = (x, y);
        }

        public static Foundation Zero => new();

        public static implicit operator (int, int)(Foundation foundation)
        {
            return (foundation.Size.x, foundation.Size.y);
        }

        public override string ToString() => $"({Size.x}, {Size.y})";
    }
}