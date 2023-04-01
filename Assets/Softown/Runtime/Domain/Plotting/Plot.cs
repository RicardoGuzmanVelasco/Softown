using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Plot
    {
        public IEnumerable<SettledFoundation> SettledFoundations { get; }
        public (int x, int y) Size { get; }
        
        public IReadOnlyDictionary<(int x, int y), Foundation> Foundations => SettledFoundations.ToDictionary(x => x.At, x => x.Foundation);
        public (float x, float y) Center => (Size.x / 2f, Size.y / 2f);

        public Plot(Packing strategy, params Foundation[] foundations)
        {
            var plot = strategy.Order(foundations);
            SettledFoundations = plot.SettledFoundations;
            Size = plot.Size;
        }
        
        public Plot(IEnumerable<SettledFoundation> settledFoundations)
        {
            Assert.IsTrue(settledFoundations.Any());
            SettledFoundations = settledFoundations;
            Size = (SettledFoundations.Max(x => x.At.x + x.Foundation.Size.x), SettledFoundations.Max(x => x.At.y + x.Foundation.Size.y));
        }

        public static Plot Blank { get; } = new();
        
        public bool SameSizeThan(Plot other) => Size == other.Size;
        public bool SameSizeThan(SettledFoundation other) => Size == other.Size;
        public bool SameSizeThan(Foundation other) => Size == other.Size;

        public override bool Equals(object obj)
        {
            return obj is Plot plot && Equals(plot);
        }
        
        public bool Equals(Plot other)
        {
            if(SettledFoundations is null)
                return other.SettledFoundations is null;
            if(other.SettledFoundations is null)
                return SettledFoundations is null;
            if(Size.Equals(other.Size) == false)
                return false;
            if(SettledFoundations.Count() != other.SettledFoundations.Count())
                return false;
            return SettledFoundations.SequenceEqual(other.SettledFoundations) && Size.Equals(other.Size);
        }

        public static Plot operator +(Plot left, Plot right)
        {
            if(left.Equals(Blank))
                return right;
            if(right.Equals(Blank))
                return left;
            
            var leftSettledFoundations = left.SettledFoundations;
            var verticalOffset = left.Size.y;
            var rightSettledFoundations = right.TranslateInY(verticalOffset).SettledFoundations;
            return new(leftSettledFoundations.Concat(rightSettledFoundations));
        }

        Plot TranslateInY(int verticalOffset)
        {
            if(this.Equals(Blank))
                return this;
            
            return new(SettledFoundations.Select(x => new SettledFoundation((x.At.x, x.At.y + verticalOffset), x.Foundation)));
        }

        public override string ToString()
        {
            return $"Plot of size {Size} with:\n" + string.Join("\n", SettledFoundations.Select(x => x.ToString()));
        }
    }
}