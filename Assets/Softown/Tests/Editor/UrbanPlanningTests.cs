using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class UrbanPlanningTests
    {
        [Test]
        public void Obtain_BuildingsAmount()
        {
            new UrbanPlanning(Enumerable.Repeat(new Blueprint(1, 1), 20))
                .Buildings
                .Should().Be(20);
        }
    }
}