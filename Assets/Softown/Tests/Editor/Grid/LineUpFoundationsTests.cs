using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Tests.Editor
{
    public class LineUpFoundationsTests
    {
        [Test]
        public void LineUpTemp_OneFundation()
        {
            var sut = new FoundationsArranger();

            var result = sut.LineUp(new[] { SquareOf(2) });

            result.Foundations.Should().ContainSingle().And.ContainValue(SquareOf(2));
        }
        
        [Test]
        public void LineUpTemp_TwoFundations()
        {
            var sut = new FoundationsArranger();

            var result = sut.LineUp(new[] { SquareOf(2), SquareOf(1) });

            result.Foundations.Should()
                .HaveCount(2)
                .And.Contain((0, 0), SquareOf(2))
                .And.Contain((2, 0), SquareOf(1));
        }

        [Test]
        public void LineUpTemp_TwoFundations_WithSpaceInbetween()
        {
            var sut = new FoundationsArranger(inbetween: 5);

            var result = sut.LineUp(new[] { SquareOf(2), SquareOf(1) });

            result.Foundations.Should()
                .HaveCount(2)
                .And.Contain((0, 0), SquareOf(2))
                .And.Contain((7, 0), SquareOf(1));
        }
        
        [Test]
        public void LineUpTemp_ThreeFundations()
        {
            var sut = new FoundationsArranger();

            var result = sut.LineUp(new[] { SquareOf(2), SquareOf(1), SquareOf(3) });

            result.Foundations.Should()
                .HaveCount(3)
                .And.Contain((0, 0), SquareOf(2))
                .And.Contain((2, 0), SquareOf(1))
                .And.Contain((3, 0), SquareOf(3));
        }
        
        [Test]
        public void LineUpTemp_ThreeFundations_WithSpaceInbetween()
        {
            var sut = new FoundationsArranger(inbetween: 1);

            var result = sut.LineUp(new[] { SquareOf(2), SquareOf(1), SquareOf(3) });

            result.Foundations.Should()
                .HaveCount(3)
                .And.Contain((0, 0), SquareOf(2))
                .And.Contain((3, 0), SquareOf(1))
                .And.Contain((5, 0), SquareOf(3));
        }
    }
}