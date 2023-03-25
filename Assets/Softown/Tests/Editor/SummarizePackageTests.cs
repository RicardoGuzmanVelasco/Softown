using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizePackageTests
    {
        [Test]
        public void Obtain_PackageName_From_Assembly()
        {
            new PackageSummary(typeof(SummarizePackageTests).Assembly).Name.Should().StartWith("Softown");
        }
        
        [Test]
        public void Obtain_PackageClasses_From_Assembly()
        {
            new PackageSummary(typeof(SummarizePackageTests).Assembly).Classes.Should().BePositive();
        }
    }
}