﻿using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeClassesIntoNamespaceTests
    {
        [Test]
        public void GlobalNamespace_Contains_ClassWithoutNamespace()
        {
            new NamespaceSummary(Namespace.Global, new[] { typeof(NoNamespace) })
                .AllChildrenClasses
                .Should()
                .Contain(new ClassSummary(typeof(NoNamespace)))
                .And.HaveCount(1);
        }
        
        [Test]
        public void GlobalNamespace_Contains_ClassWithNamespace()
        {
            new NamespaceSummary(Namespace.Global, new[] { typeof(A.B1.B11) })
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
        public void Namespace_Contains_ClassesIncludedInItsNamespace()
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

        [Test]
        public void GlobalNamespace_Leafs_AreClassesWithoutNamespace()
        {
            new NamespaceSummary(Namespace.Global, new[] { typeof(NoNamespace), typeof(A.A1) })
                .OnlyLeafClasses
                .Should()
                .Contain(new ClassSummary(typeof(NoNamespace)))
                .And.HaveCount(1);
        }
        
        [Test]
        public void Namespace_Leafs_AreClassesExactlyInThisNamespace()
        {
            new NamespaceSummary("A", new[] { typeof(A.A1), typeof(A.B1.B11) })
                .OnlyLeafClasses
                .Should()
                .Contain(new ClassSummary(typeof(A.A1)))
                .And.HaveCount(1);
        }
        
        [Test]
        public void NamespaceInnerOfOther_Leafs_AreClassesExactlyInThisNamespace()
        {
            new NamespaceSummary("A.B1", new[] { typeof(A.A1), typeof(A.B1.B11), typeof(A.B1.C.C1) })
                .OnlyLeafClasses
                .Should()
                .Contain(new ClassSummary(typeof(A.B1.B11)))
                .And.HaveCount(1);
        }

        [Test]
        public void LeafNamespace_AllClassesAreLeafs()
        {
            var sut = new NamespaceSummary("A.B2.C", new[]{typeof(A.B2.C.C1), typeof(A.B2.C.C2)});
            sut.OnlyLeafClasses.Should().BeEquivalentTo(sut.AllChildrenClasses);
        }
    }
}