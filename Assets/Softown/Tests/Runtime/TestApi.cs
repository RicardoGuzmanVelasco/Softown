using System.Threading.Tasks;
using FluentAssertions.Extensions;

namespace Softown.Tests.Runtime
{
    public static class TestApi
    {
        /// Tiempo de sobra según la animación actual.
        public static Task EnoughForRaiseAnyBuilding => Task.Delay(.5.Seconds());
    }
}