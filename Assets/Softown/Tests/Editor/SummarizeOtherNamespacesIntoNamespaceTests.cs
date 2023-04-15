using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeOtherNamespacesIntoNamespaceTests
    {
        [Test]
        public void LeafNamespace_HasNo_ChildrenNamespace()
        {
            var sut = new NamespaceSummary("A.B2.C", typeof(A.B2.C.C1).Assembly.DefinedTypes);
            sut.AllChildrenNamespaces.Should().BeEmpty();
        }
    }
}