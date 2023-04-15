using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using static Softown.Runtime.Domain.NamespaceSummary;

namespace Softown.Tests.Editor
{
    public class NamespaceExtensionsTests
    {
        [Test]
        public void Namespace_IsAlwaysInnerOf_GlobalNamespace()
        {
            "A".IsInnerNamespaceOf(GlobalNamespace).Should().BeTrue();
            "A.B".IsInnerNamespaceOf(GlobalNamespace).Should().BeTrue();
        }
        
        [Test]
        public void GlobalNamespace_IsNotInner_OfAnyNamespace()
        {
            GlobalNamespace.IsInnerNamespaceOf("A").Should().BeFalse();
            GlobalNamespace.IsInnerNamespaceOf(GlobalNamespace).Should().BeFalse();
        }
        
        [Test]
        public void GlobalNamespace_IsRootOfNothing_AndHasNoRoot()
        {
            GlobalNamespace.IsRootOf("A.B").Should().BeFalse();
            "A.B".IsRootOf(GlobalNamespace).Should().BeFalse();
            GlobalNamespace.IsRootOf(GlobalNamespace).Should().BeFalse();
        }

        [Test]
        public void Namespace_IsNotRoot_OfSameNamespace()
        {
            "A.B".IsRootOf("A.B").Should().BeFalse();
        }
    }
}