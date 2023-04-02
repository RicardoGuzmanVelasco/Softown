using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public record Plot : Block, IEnumerable<Block>
    {
        public IEnumerable<Settled> Blocks { get; }

        public override (int x, int y) Size => (Blocks.Max(x => x.At.x + x.Block.Size.x),
            Blocks.Max(x => x.At.y + x.Block.Size.y));

        public (float x, float y) Center => (Size.x / 2f, Size.y / 2f);

        public Plot(Packing strategy, params Block[] blocks)
        {
            var plot = strategy.Order(blocks);
            Blocks = plot.Blocks;
        }

        public Plot(IEnumerable<Settled> blocks)
        {
            Assert.IsTrue(blocks.Any());
            Blocks = blocks;
        }

        Plot() { }

        public static Plot Blank { get; } = new();

        public bool SameSizeThan(Plot other) => Size == other.Size;

        public IEnumerator<Block> GetEnumerator()
        {
            return Blocks.Select(s => s.Block).GetEnumerator();
        }

        public static Plot operator +(Plot left, Plot right)
        {
            if(left.Equals(Blank))
                return right;
            if(right.Equals(Blank))
                return left;

            var leftSettledFoundations = left.Blocks;
            var verticalOffset = left.Size.y;
            var rightSettledFoundations = right.TranslateInY(verticalOffset).Blocks;
            return new(leftSettledFoundations.Concat(rightSettledFoundations));
        }

        Plot TranslateInY(int verticalOffset)
        {
            if(this.Equals(Blank))
                return this;

            return new(Blocks.Select(x => new Settled((x.At.x, x.At.y + verticalOffset), x.Block)));
        }

        public override string ToString()
        {
            return $"Plot of size {Size} with:\n" + string.Join("\n", Blocks.Select(x => x.ToString()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}