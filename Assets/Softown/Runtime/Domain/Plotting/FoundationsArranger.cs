using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public class FoundationsArranger
    {
        readonly int space = 1;

        public FoundationsArranger() : this(0) { }

        public FoundationsArranger(int inbetween)
        {
            Assert.IsTrue(inbetween >= 0);
            this.space = inbetween;
        }

        public Foundation SquareUp(IReadOnlyList<Foundation> foundations)
        {
            Assert.IsTrue(foundations.Any());

            if(foundations.Count == 1)
                return foundations.First();

            var firstTwo = Compound2(foundations.Take(2).ToArray());
            if(foundations.Count == 2)
                return firstTwo;

            return Foundation.RectangleOf(firstTwo.Size.x, firstTwo.Size.y + space + foundations[2].Size.y);
        }

        public Plot LineUpTemp(IReadOnlyList<Foundation> foundations)
        {
            Assert.IsTrue(foundations.Any());
            
            (int x, int y) origin = (0, 0);
            var result = new Dictionary<(int x, int y), Foundation>();
            foreach(var foundation in foundations)
            {
                result.Add(origin, foundation);
                origin.x += foundation.Size.x + space;
            }
            
            Assert.IsTrue(result.Keys.All(k => k.Item2 == 0)); //a modelo.
            return new(result);
        }

        Foundation Compound2(IReadOnlyCollection<Foundation> foundations)
        {
            Assert.IsTrue(foundations.Count == 2);

            var (x, y) = (0, foundations.Max(f => f.Size.y));
            foreach(var f in foundations)
                x += f.Size.x + space;
            x -= space;

            return Foundation.RectangleOf(x, y);
        }
    }
}