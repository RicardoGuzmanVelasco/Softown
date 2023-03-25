using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class SummarizeClassTests
    {
        [Test]
        public void Obtain_MethodsAmount_FromClass()
        {
            new ClassSummary(typeof(TwoMethods)).PublicMethods.Should().Be(2);
            new ClassSummary(typeof(ThreeMethods)).PublicMethods.Should().Be(3);
        }

        [Test]
        public void Obtain_MethodsAmount_Ignores_Properties()
        {
            new ClassSummary(typeof(TwoProperties)).PublicMethods.Should().Be(0);
        }

        [Test]
        public void Obtain_Properties_FromClass()
        {
            new ClassSummary(typeof(TwoProperties)).Properties.Should().Be(2);
            new ClassSummary(typeof(ThreeProperties)).Properties.Should().Be(3);
        }

        public class TwoProperties
        {
            public int Property_1 { get; set; }
            public int Property_2 { get; set; }
        }
        
        public class ThreeProperties
        {
            public int Property_1 { get; set; }
            public int Property_2 { get; set; }
            public int Property_3 { get; set; }
        }
        
        public class TwoMethods
        {
            public void Method_1() { }
            public void Method_2() { }
        }
        
        public class ThreeMethods
        {
            public void Method_1() { }
            public void Method_2() { }
            public void Method_3() { }
        }
    }
}