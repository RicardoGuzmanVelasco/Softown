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
            new UrbanPlanning("Whatever", Enumerable.Repeat(new Blueprint(1, 1), 20))
                .Buildings
                .Should().Be(20);

            new UrbanPlanning("Whatever", new District[] { new(Enumerable.Repeat(new Blueprint(1, 1), 20)) })
                .Buildings
                .Should().Be(20);
        }
        
        [Test]
        public void Obtain_BuildingsAmount_Of2Districts()
        {
            new UrbanPlanning("Whatever", new District[] { new(Enumerable.Repeat(new Blueprint(1, 1), 20)), new(Enumerable.Repeat(new Blueprint(1, 1), 20)) })
                .Buildings
                .Should().Be(40);
        }
    }
}