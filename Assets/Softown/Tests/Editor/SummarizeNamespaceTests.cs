using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using static Softown.Runtime.Domain.NamespaceSummary;

namespace Softown.Tests.Editor
{
    public class SummarizeNamespaceTests
    {
        [Test]
        public void Obtain_Namespace_FromClass_WithNoNamespace()
        {
            typeof(A.B1.C.C1).Assembly.AllNamespaces().Should().Contain
            (
                new[] { null, "A", "A.B1", "A.B1.C", "A.B2.C" }
            );
        }

        [Test]
        public void Obtain_OnlyRootNamespaces()
        {
            typeof(A.B1.C.C1).Assembly.AllNamespaces().OnlyRoots().Should().Contain
            (
                new[] { null, "A" }
            ).And.NotContain(new[] { "A.B1", "A.B1.C", "A.B2.C" });
        }

        [Test]
        public void Trunk_NamespaceRoot_Of_aType()
        {
            typeof(A.A1).TrunkNamespaceRoot().Should().Be(string.Empty);
            typeof(A.B1.C.C1).TrunkNamespaceRoot().Should().Be("B1.C");
            typeof(NoNamespace).TrunkNamespaceRoot().Should().Be(GlobalNamespace);
        }

        [Test]
        public void SubnamespaceOf()
        {
            "A".IsSubnamespaceOf("A").Should().BeFalse();
            "A".IsSubnamespaceOf("B").Should().BeFalse();
            "A".IsSubnamespaceOf("A.B").Should().BeFalse();
            "A.B".IsSubnamespaceOf("A").Should().BeTrue();
            "A.B".IsSubnamespaceOf("A.B").Should().BeFalse();
            "A.B".IsSubnamespaceOf("A.B.C").Should().BeFalse();
            "A.B.C".IsSubnamespaceOf("A.B").Should().BeTrue();
            "A.B.C".IsSubnamespaceOf("A.B.C").Should().BeFalse();
            "A.B.C".IsSubnamespaceOf("A.B.C.D").Should().BeFalse();
            "A.B.C.D".IsSubnamespaceOf("A.B.C").Should().BeTrue();
            "A.B.C.D".IsSubnamespaceOf("A.B.C.D").Should().BeFalse();
        }

        [Test]
        public void CannotStore_Type_WithOtherNamespace()
        {
            new NamespaceSummary("NO", new[] { typeof(A.B1.C.C1) }).Should().BeEmpty();
        }

        [Test]
        public void Store_Type_WithSameNamespace()
        {
            new NamespaceSummary("A.B1", new[] { typeof(A.B1.C.C1) }).Should().HaveCount(1);
        }

        [Test]
        public void ClassInGlobal()
        {
            new NamespaceSummary(GlobalNamespace, new[] { typeof(NoNamespace) }).Should().HaveCount(1);
        }

        [Test]
        public void ClassInNamespace()
        {
            new NamespaceSummary("A", new[] { typeof(A.A1) }).Classes.Should().Be(1);
            new NamespaceSummary("A.B1", new[] { typeof(A.A1), typeof(A.B1.B11) }).Classes.Should().Be(1);
        }

        [Test]
        public void NamespacesInRoot()
        {
            new NamespaceSummary("A", new[] { typeof(A.A1), typeof(A.B1.B11) }).Namespaces.Should().HaveCount(1);
        }
    }
}