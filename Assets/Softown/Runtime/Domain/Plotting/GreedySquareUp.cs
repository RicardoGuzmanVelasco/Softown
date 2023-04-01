using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace Softown.Runtime.Domain.Plotting
{
    class GreedySquareUp : Packing
    {
        public override Plot Order(IEnumerable<Foundation> foundations)
        {
            if(foundations.Count() <= 2)
                return new LineUp().Order(foundations);

            var count = foundations.Count();
            var pows = Range(1, 31).Select(i => (int) Math.Pow(2, i)).Where(i => i <= count).ToList();
            var candidatePow = pows.Last();
            
            return AccPlot(foundations.Take(candidatePow)) + AccPlot(foundations.Skip(candidatePow));
        }

        static Plot AccPlot(IEnumerable<Foundation> foundations)
        {
            if(!foundations.Any())
                return Plot.Blank;
            
            var count = foundations.Count();

            var dividers = Range(1, count).Where(i => count % i == 0).ToList();

            var (x, y) = dividers.Count % 2 == 0
                ? (dividers[dividers.Count / 2 - 1], dividers[dividers.Count / 2])
                : (dividers[dividers.Count / 2], dividers[dividers.Count / 2]);

            var t = foundations.ToList();
            var groups = Range(0, x).Select(i => t.Skip(i * y).Take(y)).ToList();

            var plots = groups.Select(g => new LineUp().Order(g)).ToList();

            var accPlot = plots[0];
            for(var i = 1; i < plots.Count; i++)
                accPlot += plots[i];

            return accPlot;
        }
    }
}