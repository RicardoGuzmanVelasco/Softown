using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

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
            ).And.NotContain(new[]{"A.B1", "A.B1.C", "A.B2.C"});
        }
    }
}