namespace Softown.Tests.TestAPI
{
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

    public class TwoMethods_AndOneProperty
    {
        public int Property_1 { get; set; }
        public void Method_1() { }
        public void Method_2() { }
    }
}