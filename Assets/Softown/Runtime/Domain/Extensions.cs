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
            return t.Namespace.TrunkNamespaceRoot();
        }
        public static string TrunkNamespaceRoot(this string ns)
        {
            if(ns is null)
                return NamespaceSummary.GlobalNamespace;

            if(!ns.Contains('.'))
                return string.Empty;

            return ns[(1+ns.IndexOf('.'))..];
        }

        public static bool IsInSubnamespaceOf(this Type t, string root)
        {
            return t.Namespace.IsSubnamespaceOf(root);
        }
        public static bool IsSubnamespaceOf(this string ns, string root)
        {
            if(ns is null)
                return root is null;
            if(root is null)
                return false;

            if(root == string.Empty || ns == string.Empty)
                return false;

            if(ns == root)
                return false;
            
            return ns.StartsWith(root) ||
                   root.TrunkNamespaceRoot().Equals(ns) ||
                   ns.IsSubnamespaceOf(root.TrunkNamespaceRoot());
        }
        
        public static bool IsInnerNamespaceOf(this string ns, string root)
        {
            if(root.StartsWith(ns) || ns.StartsWith(root))
                return false;
            
            if(root.EndsWith(ns))
                return false;

            if(root.Contains("." + ns + "."))
                return true;

            return ns.IsSubnamespaceOf(root);
        }

        public static string TrunkUntilDeleteSubnamespace(this string ns, string subns)
        {
            if(ns is null)
                return null;
            if(subns is null)
                return ns;

            if(subns == string.Empty || ns == string.Empty)
                return ns;

            if(ns == subns)
                return string.Empty;
            
            if(ns.StartsWith(subns))
                return ns[(1+subns.Length)..];

            return ns.IndexOf("." + subns + ".") is var i && i > 0
                ? ns[(2+i+subns.Length)..]
                : ns;
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