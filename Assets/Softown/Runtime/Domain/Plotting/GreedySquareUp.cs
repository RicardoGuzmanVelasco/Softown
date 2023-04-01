using System.Collections.Generic;

namespace Softown.Runtime.Domain.Plotting
{
    class GreedySquareUp : Packing
    {
        public override Plot Order(IEnumerable<Foundation> foundations)
        {
            return new LineUp().Order(foundations);
        }
    }
}