using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public static class Extensions
    {
        public static ISet<string> AllNamespaces(this Assembly assembly)
        {
            return new HashSet<string>(assembly.GetTypes().Select(t => t.Namespace));
        }

        public static ISet<string> OnlyRoots(this IEnumerable<string> namespaces)
        {
            var global = namespaces.Where(n => n is null);
            var others = namespaces.Where(n => n is not null).Select(n => n.Split('.').First());
            return new HashSet<string>(global.Concat(others));
        }

        public static string TrunkNamespaceRoot(this Type t)
        {
            if(t.Namespace is null)
                return NamespaceSummary.GlobalNamespace;

            if(!t.Namespace.Contains('.'))
                return string.Empty;

            return t.Namespace[(1+t.Namespace.IndexOf('.'))..];
        }
        
        public static bool IsSubnamespaceOf(this string ns, string root)
        {
            if(ns is null)
                return root is null;
            if(root is null)
                return false;

            return ns != root && ns.StartsWith(root);
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