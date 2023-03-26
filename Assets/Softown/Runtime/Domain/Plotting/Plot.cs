using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Plot
    {
        public IReadOnlyDictionary<(int x, int y), Foundation> Foundations { get; }

        public (int x, int y) Size => (X, Y);
        int X => Foundations.Keys.Last().x + Foundations.Values.Last().Size.x;
        int Y => Foundations.Values.First().Size.y;

        public Plot(params SettledFoundation[] foundations) : this((IEnumerable<SettledFoundation>) foundations) { }
        
        public Plot(IEnumerable<SettledFoundation> foundations)
        : this(foundations.ToDictionary(x => x.At, x => x.Foundation)) { }
        
        public Plot(IReadOnlyDictionary<(int x, int y), Foundation> foundations)
        {
            Foundations = foundations;
        }

        public static Plot Blank => new();
    }
}