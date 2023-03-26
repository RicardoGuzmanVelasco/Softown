using UnityEngine.Assertions;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct SettledFoundation
    {
        public readonly (int x, int y) At; //en realidad esto debería ser el center.
        public readonly Foundation Foundation;
        
        public SettledFoundation((int x, int y) at, Foundation foundation)
        {
            Assert.IsTrue(at.x  >= 0);
            Assert.IsTrue(at.y  >= 0);
            Assert.IsFalse(foundation.Equals(Zero));
            
            At = at;
            Foundation = foundation;
        }
    }
}