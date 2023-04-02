using UnityEngine.Assertions;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Runtime.Domain.Plotting
{
    public record Settled : Block
    {
        public readonly (int x, int y) At; //en realidad esto debería ser el center.
        public readonly Block Block;
        
        public override (int x, int y) Size => Block.Size;
        
        public Settled((int x, int y) at, Block block)
        {
            Assert.IsTrue(at.x  >= 0);
            Assert.IsTrue(at.y  >= 0);
            Assert.IsFalse(block.Equals(Zero));
            
            At = at;
            Block = block;
        }
        
        public override string ToString() => $"At ({At.x}, {At.y}): {Block}";
    }
}