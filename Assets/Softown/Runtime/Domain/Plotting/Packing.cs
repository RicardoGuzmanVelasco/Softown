using System.Collections.Generic;

namespace Softown.Runtime.Domain.Plotting
{
    public abstract class Packing
    {
        public abstract Plot Order(IEnumerable<Foundation> foundations);
    }
}