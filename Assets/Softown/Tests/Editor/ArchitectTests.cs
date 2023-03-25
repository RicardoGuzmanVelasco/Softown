using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Tests.TestAPI;

namespace Softown.Tests.Editor
{
    public class ArchitectTests
    {
        [Test]
        public void Design_aBlueprint()
        {
            new Architect()
                .Design(new ClassSummary(typeof(TwoMethods)))
                .Floors.Should().Be(2);
        }

        [Test]
        public void Design_UrbanPlan()
        {
            new Architect()
                .Design(new PackageSummary(typeof(TwoMethods).Assembly))
                .Buildings.Should().Be(5);
        }
    }
}