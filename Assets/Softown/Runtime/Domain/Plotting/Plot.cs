using System.Collections.Generic;
using System.Linq;

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
            SettledFoundations = settledFoundations;
            Size = (SettledFoundations.Max(x => x.At.x + x.Foundation.Size.x), SettledFoundations.Max(x => x.At.y + x.Foundation.Size.y));
        }

        public static Plot Blank { get; } = new();
        
        public override string ToString() => $"Plot of {SettledFoundations.Count()} foundations";
    }
}