using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;

namespace Softown.Tests.Editor
{
    public class SquareUpFoundationsInPlotTests
    {
        [Test]
        public void Only_withOneFundation()
        {
            var sut = new Plot();

            var result = sut.SquareUp(new[] { new Foundation(2) });

            result.Should().Be(new Foundation(2));
        }

        [Test]
        public void WithTwoFundations_OfSameSize_Puts_aSpaceInbetween()
        {
            var sut = new Plot(inbetween: 1);

            var result = sut.SquareUp(new[] { new Foundation(2), new Foundation(2) });

            result.SameSizeThan(new(2, 2 + 1 + 2)).Should().BeTrue();
        }

        [Test]
        public void WithThreeFundations_DoesNot_Puts_InaRow()
        {
            var sut = new Plot();

            var result = sut.SquareUp(new[] { new Foundation(2), new Foundation(2), new Foundation(2) });

            result.Max().Should().BeLessThan(6);
        }
    }
}