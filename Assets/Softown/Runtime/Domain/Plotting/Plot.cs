using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Plot
    {
        public IReadOnlyDictionary<(int x, int y), Foundation> Foundations => SettledFoundations.ToDictionary(x => x.At, x => x.Foundation);
        public IEnumerable<SettledFoundation> SettledFoundations { get; }

        public (int x, int y) Size { get; }
        public (float x, float y) Center => (Size.x / 2f, Size.y / 2f);

        public Plot(params SettledFoundation[] foundations) : this((IEnumerable<SettledFoundation>) foundations) { }
        
        public Plot(IEnumerable<SettledFoundation> foundations)
        : this(foundations.ToDictionary(x => x.At, x => x.Foundation)) { }
        
        public Plot(IReadOnlyDictionary<(int x, int y), Foundation> foundations)
        {
            SettledFoundations = foundations.Select(x => new SettledFoundation(x.Key, x.Value));
            Size = 
            (
                x: SettledFoundations.Max(s => s.At).x + SettledFoundations.Max(v => v.Foundation.Size.x),
                y: SettledFoundations.Max(s => s.At).y + SettledFoundations.Max(v => v.Foundation.Size.y)
            );
        }

        public static Plot Blank => new();
    }
}