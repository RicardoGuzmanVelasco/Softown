using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeOtherNamespacesIntoNamespaceTests
    {
        [Test]
        public void LeafNamespace_HasNoChildrenNamespace()
        {
            var sut = new NamespaceSummary("A.B2.C", typeof(A.B2.C.C1).Assembly.DefinedTypes);
            sut.DirectChildrenNamespaces.Should().BeEmpty();
        }
        
        [Test]
        public void InnerNamespace_HasChildrenNamespaces()
        {
            var sut = new NamespaceSummary("A.B2", typeof(A.B2.C.C1).Assembly.DefinedTypes);
            sut.DirectChildrenNamespaces.Should().HaveCount(1);
        }
        
        [Test]
        public void GlobalNamespace_AlwaysHasOtherRootNamespaces_AsChildrenNamespaces()
        {
            var sut = new NamespaceSummary(Namespace.Global, new[]{typeof(A.A1)});
            sut.DirectChildrenNamespaces.Should().HaveCountGreaterThan(0);
        }
        
        [Test]
        public void Parent_OfInnerNamespace_HasAsChildren_AlsoChildrenOfInnerNamespace()
        {
            var types = new []{typeof(A.B1.C.C1), typeof(A.B1.B11), typeof(A.A1)};
            new NamespaceSummary("A", types)
                .AllChildrenNamespaces
                .Should().Contain(new NamespaceSummary("A.B1.C", types));
        }

        [Test]
        public void Parent_OfInnerNamespace_HasNotDirectChildren_IfChildrenHaveNotLeafClasses()
        {
            var types = new []{typeof(A.B2.C.C1), typeof(A.A1)};
            new NamespaceSummary("A", types)
                .DirectChildrenNamespaces
                .Should().BeEmpty();
        }
        
        [Test]
        public void Parent_OfInnerNamespace_HasOnlyItsChildren_AsDirectChildren()
        {
            var types = new []{typeof(A.B1.C.C1), typeof(A.B1.B11)};
            new NamespaceSummary("A", types)
                .DirectChildrenNamespaces
                .Should().Contain(new NamespaceSummary("A.B1", types))
                .And.HaveCount(1);
        }
    }
}