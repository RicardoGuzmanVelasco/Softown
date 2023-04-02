using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace Softown.Runtime.Domain.Plotting
{
    class GreedySquareUp : Packing
    {
        public override Plot Order(IEnumerable<Block> blocks)
        {
            if(blocks.Count() <= 2)
                return new LineUp().Order(blocks);

            var count = blocks.Count();
            var pows = Range(1, 31).Select(i => (int) Math.Pow(2, i)).Where(i => i <= count).ToList();
            var candidatePow = pows.Last();
            
            return AccPlot(blocks.Take(candidatePow)) + AccPlot(blocks.Skip(candidatePow));
        }

        static Plot AccPlot(IEnumerable<Block> blocks)
        {
            if(!blocks.Any())
                return Plot.Blank;
            
            var count = blocks.Count();

            var dividers = Range(1, count).Where(i => count % i == 0).ToList();

            var (x, y) = dividers.Count % 2 == 0
                ? (dividers[dividers.Count / 2 - 1], dividers[dividers.Count / 2])
                : (dividers[dividers.Count / 2], dividers[dividers.Count / 2]);

            var t = blocks.ToList();
            var groups = Range(0, x).Select(i => t.Skip(i * y).Take(y)).ToList();

            var plots = groups.Select(g => new LineUp().Order(g)).ToList();

            var accPlot = plots[0];
            for(var i = 1; i < plots.Count; i++)
                accPlot += plots[i];

            return accPlot;
        }
    }
}