using System.Linq;
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
            new AssemblySummary(typeof(A.B1.C.C1).Assembly).GlobalNamespace.AllChildrenClasses.Count().Should().BePositive();
        }

        [Test]
        public void AsemblySummary_OnlyContains_UniqueChilds()
        {
            new AssemblySummary(typeof(A.B1.C.C1).Assembly).GlobalNamespace.AllChildrenClasses.Should().OnlyHaveUniqueItems();
        }
    }
}