using UnityEngine.Assertions;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct SettledFoundation
    {
        public readonly (int x, int y) At; //en realidad esto debería ser el center.
        public readonly Foundation Foundation;
        
        public (int x, int y) Size => Foundation.Size;
        
        public SettledFoundation((int x, int y) at, Foundation foundation)
        {
            Assert.IsTrue(at.x  >= 0);
            Assert.IsTrue(at.y  >= 0);
            Assert.IsFalse(foundation.Equals(Zero));
            
            At = at;
            Foundation = foundation;
        }
        
        public bool SameSizeThan(Foundation other) => Foundation.SameSizeThan(other);
        public bool SameSizeThan(SettledFoundation other) => SameSizeThan(other.Foundation);
        
        public override string ToString() => $"At ({At.x}, {At.y}): {Foundation}";
    }
}