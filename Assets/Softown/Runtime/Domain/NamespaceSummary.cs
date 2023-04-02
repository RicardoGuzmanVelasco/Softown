using System;
using System.Collections;
using System.Collections.Generic;

namespace Softown.Runtime.Domain
{
    public readonly struct NamespaceSummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        IReadOnlyCollection<ClassSummary> ClassSummaries { get; }
        public int Classes => ClassSummaries.Count;

        public NamespaceSummary(string name, IEnumerable<Type> types)
        {
            //Obtain namespaces of assembly
            Name = name;
            ClassSummaries = new List<ClassSummary>();
        }

        public NamespaceSummary(IEnumerable<ClassSummary> classes)
        {
            ClassSummaries = new List<ClassSummary>(classes);
            Name = "Placeholder";
        }

        public static object Empty => new();
        public IEnumerator<ClassSummary> GetEnumerator() => ClassSummaries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}