using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public static class AssemblyExtensions
    {
        public static ISet<string> Namespaces(this Assembly assembly)
        {
            return new HashSet<string>(assembly.GetTypes().Select(t => t.Namespace));
        }

        public static ISet<string> OnlyRoots(this IEnumerable<string> namespaces)
        {
            return new HashSet<string>(namespaces.Where(n => n is null || !n.Contains('.')));
        }
    }
}

namespace A.B1.C
{
    public class C1 { }
}

namespace A.B2.C
{
    public class C1 { }
}

namespace A.B1
{
    public class B11 { }
}

namespace A
{
    public class A1 { }
}

public class NoNamespace { }