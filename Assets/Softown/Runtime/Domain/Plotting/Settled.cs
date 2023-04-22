using UnityEngine.Assertions;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Runtime.Domain.Plotting
{
    public record Settled : Block
    {
        public readonly (int x, int y) AtLeftBottom;
        public readonly Block Block;

        public override (int x, int y) Size => Block.Size;
        public (float x, float y) Center => (AtLeftBottom.x + (float)Size.x / 2, AtLeftBottom.y + (float)Size.y / 2);
        public (int x, int y) Further => (AtLeftBottom.x + Size.x, AtLeftBottom.y + Size.y);

        public Settled((int x, int y) atLeftBottom, Block block)
        {
            Assert.IsTrue(atLeftBottom.x >= 0);
            Assert.IsTrue(atLeftBottom.y >= 0);
            Assert.IsFalse(block.Equals(Zero));

            AtLeftBottom = atLeftBottom;
            Block = block;
        }

        public override string ToString() => $"AtLeftBottom ({AtLeftBottom.x}, {AtLeftBottom.y}): {Block}";
    }
}