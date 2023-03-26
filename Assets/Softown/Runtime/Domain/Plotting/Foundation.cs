namespace Softown.Runtime.Domain.Plotting
{
    public readonly struct Foundation
    {
        public int X { get; }
        public int Y { get; }
        
        public Foundation(int x) : this(x, x) { }
        
        public Foundation(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public bool SameSizeThan(Foundation other) => X + Y == other.X + other.Y;

        public static implicit operator (int, int)(Foundation foundation)
        {
            return (foundation.X, foundation.Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}