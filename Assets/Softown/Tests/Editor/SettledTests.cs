using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;

namespace Softown.Tests.Editor
{
    public class SettledTests
    {
        [Test]
        public void Calculate_SettledCenter()
        {
            new Settled((0, 0), Foundation.SquareOf(10)).Center.Should().Be((5, 5));
            new Settled((2, 0), Foundation.SquareOf(1)).Center.Should().Be((2.5f, 0.5f));
            new Settled((2, 1), Foundation.SquareOf(2)).Center.Should().Be((3, 2));
        }

        [Test]
        public void Calculate_SettledFurther()
        {
            new Settled((0, 0), Foundation.SquareOf(10)).Further.Should().Be((10,10));
            new Settled((2, 2), Foundation.SquareOf(1)).Further.Should().Be((3, 3));
        }
        
        [Test]
        public void Center_OfPlot_WithOneSettled_IsCenterOfSettled()
        {
            var settled = new Settled((0, 0), Foundation.SquareOf(2));
            var sut = new Plot(new GreedySquareUp(), settled);

            sut.Center.Should().Be(settled.Center);
        }

        [Test]
        public void Center_OfPlot_WithTwoSettled()
        {
            var settled = new Settled((0, 0), Foundation.SquareOf(2));
            var sut = new Plot(new GreedySquareUp(), settled, settled);

            sut.Center.Should().Be((2, 1));
        }

        [Test]
        public void Center_OfPlot_OfTestown()
        {
            var settled = new Settled((0, 0), Foundation.SquareOf(1));
            var sut = new Plot(new GreedySquareUp(), settled, settled, settled, settled, settled, settled);

            sut.Center.Should().Be((1.5f, 1f), "hay tres casas de (1x1) en X y dos en cada Y");
        }
    }
}