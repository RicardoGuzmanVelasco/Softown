using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain.Plotting;
using static Softown.Runtime.Domain.Plotting.Foundation;

namespace Softown.Tests.Editor
{
    public class SquareUpFoundationsTests
    {
        [Test]
        public void ForLessThanThree_BehavesAsLiningUp()
        {
            new GreedySquareUp()
                .Order(new []{SquareOf(1)})
                .SameSizeThan(new LineUp().Order(new []{SquareOf(1)}))
                .Should().BeTrue();
            
            new GreedySquareUp()
                .Order(new []{SquareOf(1), SquareOf(1)})
                .SameSizeThan(new LineUp().Order(new []{SquareOf(1), SquareOf(1)}))
                .Should().BeTrue();
        }

        [Test]
        public void WithThreeFundations_MakesTwoRows()
        {
            new GreedySquareUp()
                .Order(new []{SquareOf(1), SquareOf(1), SquareOf(1)})
                .SameSizeThan(new LineUp().Order(new []{SquareOf(1), SquareOf(1), SquareOf(1)}))
                .Should().BeFalse();
        }
    }
}