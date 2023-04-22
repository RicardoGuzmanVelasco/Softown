using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public record Plot : Block, IEnumerable<Block>
    {
        public IEnumerable<Settled> Settlements { get; }

        public override (int x, int y) Size => (Settlements.Max(x => x.AtLeftBottom.x + x.Block.Size.x),
            Settlements.Max(x => x.AtLeftBottom.y + x.Block.Size.y));

        public (float x, float y) Center
        {
            get
            {
                return (CenterBy(s => s.Further.x), CenterBy(s => s.Further.y));

                float CenterBy(Func<Settled, float> func) => Settlements.Max(func) / 2f;
            }
        }

        public Plot(Packing strategy, params Block[] blocks)
        {
            var plot = strategy.Order(blocks);
            Settlements = plot.Settlements;
        }

        public Plot(IEnumerable<Settled> settlements)
        {
            Assert.IsTrue(settlements.Any());
            Settlements = settlements;
        }

        Plot() { }

        public static Plot Blank { get; } = new();

        public IEnumerator<Block> GetEnumerator()
        {
            return Settlements.Select(s => s.Block).GetEnumerator();
        }

        public static Plot operator +(Plot left, Plot right)
        {
            if(left.Equals(Blank))
                return right;
            if(right.Equals(Blank))
                return left;

            var leftSettledFoundations = left.Settlements;
            var verticalOffset = left.Size.y;
            var rightSettledFoundations = right.TranslateInY(verticalOffset).Settlements;
            return new(leftSettledFoundations.Concat(rightSettledFoundations));
        }

        Plot TranslateInY(int verticalOffset)
        {
            if(this.Equals(Blank))
                return this;

            return new(Settlements.Select(x => new Settled((x.AtLeftBottom.x, x.AtLeftBottom.y + verticalOffset), x.Block)));
        }

        public override string ToString()
        {
            return $"Plot of size {Size} with:\n" + string.Join("\n", Settlements.Select(x => x.ToString()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}