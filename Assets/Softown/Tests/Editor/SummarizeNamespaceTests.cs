using FluentAssertions;
using FluentAssertions.Execution;
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
            using var _ = new AssertionScope();
            "A".IsSubnamespaceOf("A").Should().BeFalse("1");
            "A".IsSubnamespaceOf("B").Should().BeFalse("2");
            "A".IsSubnamespaceOf("A.B").Should().BeFalse("3");
            "A.B".IsSubnamespaceOf("A").Should().BeTrue("4");
            "A.B".IsSubnamespaceOf("A.B").Should().BeFalse("5");
            "A.B".IsSubnamespaceOf("A.B.C").Should().BeFalse("6");
            "A.B.C".IsSubnamespaceOf("A.B").Should().BeTrue("7");
            "A.B.C".IsSubnamespaceOf("A.B.C.D").Should().BeFalse("8");
            "A.B.C.D".IsSubnamespaceOf("A.B.C").Should().BeTrue("9");
            "B1.C".IsSubnamespaceOf("A.B1.C").Should().BeTrue("10");
        }

        [Test]
        public void IsInnerNamespaceOf()
        {
            using var _ = new AssertionScope();
            "A".IsInnerNamespaceOf("A").Should().BeFalse("1");
            "A".IsInnerNamespaceOf("B").Should().BeFalse("2");
            "A".IsInnerNamespaceOf("A.B").Should().BeFalse("3");
            "A.B".IsInnerNamespaceOf("A").Should().BeFalse("4");
            "B".IsInnerNamespaceOf("A.B.C").Should().BeTrue("5");
            "B".IsInnerNamespaceOf("A.B").Should().BeFalse("6");
        }

        [Test]
        public void TrunkUntilDeleteSubnamespace()
        {
            using var _ = new AssertionScope();
            "A".TrunkUntilDeleteSubnamespace("A").Should().Be(string.Empty, "1");
            "A.B.C".TrunkUntilDeleteSubnamespace("B").Should().Be("C", "2");
            "A.B.C".TrunkUntilDeleteSubnamespace("A").Should().Be("A.B.C".TrunkNamespaceRoot(), "3");
            "A.B.C".TrunkUntilDeleteSubnamespace("A.B").Should().Be("C", "4");
            "A.B.C.D".TrunkUntilDeleteSubnamespace("C").Should().Be("D", "5");

        }

        [Test]
        public void Name_DesNotMatch_anyCandidate()
        {
            new NamespaceSummary("NO", new[] { typeof(A.B1.C.C1) }).Should().BeEmpty();
        }

        [Test]
        public void Name_Match_Exactly_theCandidate()
        {
            new NamespaceSummary("A.B1.C", new[] { typeof(A.B1.C.C1) }).Should().HaveCount(1);
        }
        
        [Test]
        public void Name_Match_aSubNamespaceOf_theCandidate()
        {
            using var _ = new AssertionScope();
            new NamespaceSummary("B1.C", new[] { typeof(A.B1.C.C1) }).Should().HaveCount(1);
            new NamespaceSummary("B1.C", new[] { typeof(A.B1.C.C1) }).Classes.Should().Be(1);
        }

        [Test]
        public void Name_IsInnerNamespace()
        {
            new NamespaceSummary("B1", new[] { typeof(A.B1.C.C1) }).Should().HaveCount(1);
            new NamespaceSummary("B1", new[] { typeof(A.B1.C.C1) }).Classes.Should().Be(0);
            new NamespaceSummary("B1", new[] { typeof(A.B1.C.C1) }).NamespacesChildrenOfThisNamespace.Should().HaveCount(1);
        }

        [Test]
        public void ClassInGlobal()
        {
            new NamespaceSummary(GlobalNamespace, new[] { typeof(NoNamespace) }).Should().HaveCount(1);
        }

        [Test]
        public void Name_IsRootOf_theCandidate()
        {
            new NamespaceSummary("A", new[] { typeof(A.B1.B11) })
                .NamespacesChildrenOfThisNamespace
                .Should().HaveCount(1);

            new NamespaceSummary("A", new[] { typeof(A.B1.B11) })
                .Should().Contain(new ClassSummary(typeof(A.B1.B11)));
        }

        [Test]
        public void NewMethod()
        {
            new NamespaceSummary("A.B1", new[] { typeof(A.A1), typeof(A.B1.B11) }).Classes.Should().Be(1);
        }

        [Test]
        public void OneSubnamespace_withaClass_ButAlso_Directly_aChildClass()
        {
            new NamespaceSummary("A", new[] { typeof(A.A1), typeof(A.B1.B11) }).NamespacesChildrenOfThisNamespace.Should().HaveCount(1);
        }
    }
}