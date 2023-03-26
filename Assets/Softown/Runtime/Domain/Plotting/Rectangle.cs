namespace Softown.Runtime.Domain.Plotting
{
    public interface Rectangle
    {
        int X { get; }
        int Y { get; }
        bool SameSizeThan(Foundation other);
        Foundation Rotate();
        int Max();
        void Deconstruct(out int x, out int y);
    }
}