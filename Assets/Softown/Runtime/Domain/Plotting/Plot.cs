using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public class Plot
    {
        readonly int space = 1;

        public Plot() : this(0) { }
        
        public Plot(int inbetween)
        {
            Assert.IsTrue(inbetween >= 0);
            this.space = inbetween;
        }

        public Foundation Aggregate(IEnumerable<Foundation> foundations)
        {
            Assert.IsTrue(foundations.Any());
            
            if(foundations.Count() == 1)
                return foundations.First();
            
            var (x, y) = (0, foundations.Max(f => f.Y));
            foreach(var f in foundations)
                x += f.X + space;
            x -= space;

            return new(x, y);
        }
    }
}