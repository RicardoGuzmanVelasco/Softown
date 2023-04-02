using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeAssemblyTests
    {
        [Test]
        public void Obtain_PackageName_From_Assembly()
        {
            new AssemblySummary(typeof(SummarizeAssemblyTests).Assembly).Name.Should().StartWith("Softown");
        }

        [Test]
        public void Obtain_PackageClasses_From_Assembly()
        {
            new AssemblySummary(typeof(SummarizeAssemblyTests).Assembly).Classes.Should().BePositive();
        }

        
    }
}