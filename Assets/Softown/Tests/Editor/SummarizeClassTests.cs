using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Tests.TestAPI;

namespace Softown.Tests.Editor
{
    public class SummarizeClassTests
    {
        [Test]
        public void Obtain_MethodsAmount_FromClass()
        {
            new ClassSummary(typeof(TwoMethods)).PublicMethods.Should().Be(2);
            new ClassSummary(typeof(ThreeMethods)).PublicMethods.Should().Be(3);
        }

        [Test]
        public void Obtain_MethodsAmount_Ignores_Properties()
        {
            new ClassSummary(typeof(TwoProperties)).PublicMethods.Should().Be(0);
        }

        [Test]
        public void Obtain_Properties_FromClass()
        {
            new ClassSummary(typeof(TwoProperties)).Properties.Should().Be(2);
            new ClassSummary(typeof(ThreeProperties)).Properties.Should().Be(3);
        }
    }
}