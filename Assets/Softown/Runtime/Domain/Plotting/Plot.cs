using System.Collections.Generic;

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
                var result = 0;
                foreach(var foundation in Foundations)
                {
                    result += foundation.Value.X;
                }

                return result;
            }
        }
    }
}