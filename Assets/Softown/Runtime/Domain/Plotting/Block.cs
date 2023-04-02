namespace Softown.Runtime.Domain.Plotting
{
    public abstract record Block
    {
        public virtual (int x, int y) Size { get; protected init; }

        public bool SameSizeThan(Block other) => Size == other.Size;
    }
}