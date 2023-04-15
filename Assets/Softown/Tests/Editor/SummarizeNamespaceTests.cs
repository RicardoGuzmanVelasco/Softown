using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeNamespaceTests
    {
        [Test]
        public void GlobalNamespace_Contains_ClassWithoutNamespace()
        {
            new NamespaceSummary(NamespaceSummary.GlobalNamespace, new[] { typeof(NoNamespace) })
                .AllChildrenClasses
                .Should()
                .Contain(new ClassSummary(typeof(NoNamespace)))
                .And.HaveCount(1);
        }
        
        [Test]
        public void GlobalNamespace_Contains_ClassWithNamespace()
        {
            new NamespaceSummary(NamespaceSummary.GlobalNamespace, new[] { typeof(A.B1.B11) })
                .AllChildrenClasses
                .Should()
                .Contain(new ClassSummary(typeof(A.B1.B11)))
                .And.HaveCount(1);
        }
        
        [Test]
        public void Namespace_DoesNotContain_ClassesInGlobalNamespace()
        {
            new NamespaceSummary("A", new[] { typeof(NoNamespace) })
                .AllChildrenClasses
                .Should().BeEmpty();
        }
        
        [Test]
        public void Namespace_Contains_LeafClassInItsNamespace()
        {
            new NamespaceSummary("A", new[] { typeof(A.A1) })
                .AllChildrenClasses
                .Should()
                .Contain(new ClassSummary(typeof(A.A1)))
                .And.HaveCount(1);
        }

        [Test]
        public void Namespace_DoesNotContain_ClassInOtherNamespace()
        {
            new NamespaceSummary("A.B2", new[] { typeof(A.B1.B11) })
                .AllChildrenClasses
                .Should().BeEmpty();
        }
    }
}