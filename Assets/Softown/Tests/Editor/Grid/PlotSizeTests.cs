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
        public void Y_WithTwoFoundations_OfDifferentSizes()
        {
            var sut = new Plot
            (
                new((0, 0), Foundation.SquareOf(2)),
                new((2, 0), Foundation.SquareOf(3))
            );

            sut.Size.y.Should().Be(3);
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

        [Test]
        public void X_WithThreeFoundations_OfDifferentSizes_NorOrderedBySize()
        {
            var sut = new Plot
            (
                new((0, 0), Foundation.SquareOf(2)),
                new((0, 2), Foundation.SquareOf(3)),
                new((0, 5), Foundation.SquareOf(1))
            );

            sut.Size.x.Should().Be(3);
        }
    }
}
