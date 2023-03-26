using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;

namespace Softown.Tests.Editor
{
    public class PlotSizeTests
    {
        [Test]
        public void X_WithOneFoundation()
        {
            var sut = new Plot(new SettledFoundation((0, 0), Foundation.SquareOf(2)));

            sut.Size.x.Should().Be(2);
        }

        [Test]
        public void X_WithTwoFoundations()
        {
            var sut = new Plot
            (
                new((0, 0), Foundation.SquareOf(2)),
                new((2, 0), Foundation.SquareOf(2))
            );

            sut.Size.x.Should().Be(4);
        }

        [Test]
        public void X_WithTwoFoundations_WithSpaceBetweenThem()
        {
            var sut = new Plot
            (
                new((0, 0), Foundation.SquareOf(2)),
                new((10, 0), Foundation.SquareOf(2))
            );

            sut.Size.x.Should().Be(12);
        }
    }
}