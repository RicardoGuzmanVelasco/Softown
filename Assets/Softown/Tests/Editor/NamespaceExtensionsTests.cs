using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class NamespaceExtensionsTests
    {
        [Test]
        public void Namespace_IsAlwaysInnerOf_GlobalNamespace()
        {
            "A".IsInnerNamespaceOf(Namespace.Global).Should().BeTrue();
            "A.B".IsInnerNamespaceOf(Namespace.Global).Should().BeTrue();
        }
        
        [Test]
        public void GlobalNamespace_IsNotInner_OfAnyNamespace()
        {
            Namespace.Global.IsInnerNamespaceOf("A").Should().BeFalse();
            Namespace.Global.IsInnerNamespaceOf(Namespace.Global).Should().BeFalse();
        }
        
        [Test]
        public void GlobalNamespace_IsRootOfNothing_AndHasNoRoot()
        {
            Namespace.Global.IsRootOf("A.B").Should().BeFalse();
            "A.B".IsRootOf(Namespace.Global).Should().BeFalse();
            Namespace.Global.IsRootOf(Namespace.Global).Should().BeFalse();
        }

        [Test]
        public void Namespace_IsNotRoot_OfSameNamespace()
        {
            "A.B".IsRootOf("A.B").Should().BeFalse();
        }
    }
}