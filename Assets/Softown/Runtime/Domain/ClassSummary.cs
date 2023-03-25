using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Assertions;
using static System.Reflection.BindingFlags;

namespace Softown.Runtime.Domain
{
    public readonly struct ClassSummary
    {
        public int PublicMethods { get; }
        public int Properties { get; }
        public string Name { get; }

        public ClassSummary([NotNull] Type type)
        {
            Properties = type.GetProperties(Public | Instance | DeclaredOnly).Length;
            PublicMethods = type.GetMethods(Public | Instance | DeclaredOnly).Length;
            Name = type.Name;
            PublicMethods -= Properties * 2;

            Assert.IsTrue(Properties >= 0);
            Assert.IsTrue(PublicMethods >= 0);
        }

        public static ClassSummary Empty => new();
    }
}