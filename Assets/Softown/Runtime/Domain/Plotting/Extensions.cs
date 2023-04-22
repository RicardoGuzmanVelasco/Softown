namespace Softown.Runtime.Domain.Plotting
{
    public static class Extensions
    {
        public static Settled AtZero(this Block block)
        {
            return new((0, 0), block);
        }
    }
}