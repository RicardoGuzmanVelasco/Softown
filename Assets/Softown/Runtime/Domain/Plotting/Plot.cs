using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Plot
    {
        public IReadOnlyDictionary<(int x, int y), Foundation> Foundations { get; }

        public Plot(IReadOnlyDictionary<(int x, int y), Foundation> foundations)
        {
            Foundations = foundations;
        }

        public static Plot Blank => new();

        // X based on size of foundations
        public int X
        {
            get
            {
                return Foundations.Last().Key.x + Foundations.Last().Value.X;
            }
        }
    }
}