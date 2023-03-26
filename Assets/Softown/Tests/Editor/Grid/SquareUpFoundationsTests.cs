using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;

namespace Softown.Tests.Editor
{
    public class SquareUpFoundationsTests
    {
        [Test]
        public void Only_withOneFundation()
        {
            var sut = new FoundationsArranger();

            var result = sut.SquareUp(new[] { Foundation.SquareOf(2) });

            result.Should().Be(Foundation.SquareOf(2));
        }

        [Test]
        public void WithTwoFundations_OfSameSize_Puts_aSpaceInbetween()
        {
            var sut = new FoundationsArranger(inbetween: 1);

            var result = sut.SquareUp(new[] { Foundation.SquareOf(2), Foundation.SquareOf(2) });

            result.SameSizeThan(Foundation.RectangleOf(2, 2 + 1 + 2)).Should().BeTrue();
        }

        [Test]
        public void WithThreeFundations_DoesNot_PutInaRow()
        {
            var sut = new FoundationsArranger();

            var result = sut.SquareUp(new[] { Foundation.SquareOf(2), Foundation.SquareOf(2), Foundation.SquareOf(2) });

            result.Max().Should().BeLessThan(6);
        }
    }
}