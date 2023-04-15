using A.B1;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeAssemblyTests
    {
        static AssemblySummary Sut => new(typeof(A.B1.C.C1).Assembly);

        [Test]
        public void Obtain_PackageName_From_Assembly()
        {
            new AssemblySummary(typeof(SummarizeAssemblyTests).Assembly).Name.Should().StartWith("Softown");
        }

        [Test]
        public void Obtain_PackageClasses_From_Assembly()
        {
            Sut.Classes.Should().BePositive();
        }

        [Test]
        public void Store_RootNamespaces()
        {
            Sut.NamespacesChildrenOfGlobal.Should().HaveCount(1);
        }

        [Test]
        public void AsemblySummary_OnlyContains_UniqueChilds()
        {
            var sut = Sut;
                sut.Should().OnlyHaveUniqueItems();
        }
    }
}