﻿using System;
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