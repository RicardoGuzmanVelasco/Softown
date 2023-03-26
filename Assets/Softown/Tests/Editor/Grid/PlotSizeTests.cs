using System.Collections.Generic;
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
            var sut = new Plot(new Dictionary<(int x, int y), Foundation>()
            {
                { (0, 0), Foundation.SquareOf(2) }
            });
            
            sut.X.Should().Be(2);
        }
    }
}