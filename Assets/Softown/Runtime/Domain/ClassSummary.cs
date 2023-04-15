using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Assertions;
using static System.Reflection.BindingFlags;

namespace Softown.Runtime.Domain
{
    public readonly struct ClassSummary
    {
        internal readonly Namespace fullNamespace;
        public int PublicMethods { get; }
        public int Properties { get; }
        public string Name { get; }
        
        public string Namespace => fullNamespace.ToString();

        public ClassSummary([NotNull] Type type)
        {
            Properties = type.GetProperties(Public | Instance | DeclaredOnly).Length;
            PublicMethods = type.GetMethods(Public | Instance | DeclaredOnly).Length;
            Name = type.Name;

            fullNamespace = new(type.Namespace);

            PublicMethods -= Properties * 2;
            PublicMethods = Math.Max(0, PublicMethods);

            Assert.IsTrue(Properties >= 0);
            Assert.IsTrue(PublicMethods >= 0);
        }

        public static ClassSummary Empty => new();

        public override string ToString() => $"{Name} ({PublicMethods}m,{Properties}p)";
    }
}