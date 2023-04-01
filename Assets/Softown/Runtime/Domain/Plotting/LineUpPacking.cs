using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain.Plotting
{
    public class LineUp : Packing
    {
        readonly int space;
        
        public LineUp(int inbetween = 0)
        {
            Assert.IsTrue(inbetween >= 0);
            this.space = inbetween;
        }
        
        public override Plot Order(IEnumerable<Foundation> foundations)
        {
            Assert.IsTrue(foundations.ToList().Any());
            
            (int x, int y) origin = (0, 0);
            var result = new Dictionary<(int x, int y), Foundation>();
            foreach(var foundation in foundations)
            {
                result.Add(origin, foundation);
                origin.x += foundation.Size.x + space;
            }
            
            Assert.IsTrue(result.Keys.All(k => k.Item2 == 0)); //a modelo.
            return new(result.Select(pair => new SettledFoundation(pair.Key, pair.Value)));
        }
    }
}